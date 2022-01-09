using UnityEngine;
using System;
using UnityEngine.UI;
//Reference: https://www.youtube.com/watch?v=6OT43pvUyfY
public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    [SerializeField] Slider slider;
    [SerializeField] Toggle mute;

    bool isMute;

    private void Awake() {
        foreach(Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
        }
        
        processSettings();
    }

    private void processSettings() {

        float volume = PlayerPrefs.GetFloat("volume", 1);
        bool isMute = PlayerPrefs.GetInt("isMute", 0) == 0 ? false : true;

        if (slider != null)
        {

            slider.onValueChanged.AddListener(HandlerSliderValueChanged);
            mute.onValueChanged.AddListener(HandlerMute);

            slider.value = volume;
            mute.isOn = isMute;
        }else {

            HandlerMute(isMute);
            HandlerSliderValueChanged(volume);
        }
    }

    public void assignSliderAndMute(Slider slider, Toggle toggle) {
        this.slider = slider;
        mute = toggle;

        processSettings();
    }


    private void HandlerMute(bool isMute)
    {
        this.isMute = isMute;
        if (isMute)
        {
            AudioListener.volume = 0;
        }
        else AudioListener.volume = PlayerPrefs.GetFloat("volume");
        PlayerPrefs.SetInt("isMute", isMute ? 1 : 0);
    }

    private void HandlerSliderValueChanged(float value)
    {
        if (isMute) {
            AudioListener.volume = 0;
        } else {
            AudioListener.volume = value;
        }
        PlayerPrefs.SetFloat("volume", value);
    }

    private Sound findSound(String name) {
        if (sounds != null) {
            return Array.Find(sounds, sound => sound.name == name);
        } else {
            return null;
        }
    }

    public void play(string name) {
        Sound sound = findSound(name);

        if (sound == null) {
            return;
        }

        playSound(sound, sound.volume, true);
    }

    public void playOneAtATime(string name) {
        Sound sound = findSound(name);

        if (sound == null) {
            return;
        }

        playSound(sound, sound.volume, false);
    }

    public void stop(string name) {
        Sound sound = findSound(name);

        if (sound == null) {
            return;
        }

        sound.source.Stop();  
    }

    public void play(string name, float volume) {
        Sound sound = findSound(name);

        playSound(sound, volume, true);
    }

     public void playOneAtATime(string name, float volume) {
        Sound sound = findSound(name);

        playSound(sound, volume, false);
    }

    private void playSound(Sound sound, float volume, Boolean async) {
        if (sound == null) {
            return;
        }

        sound.source.clip = sound.clip;
        sound.source.volume = volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = sound.output;

        if (async) {
            sound.source.Play();   
        } else {
            if (!sound.source.isPlaying) {
                sound.source.Play();      
            } 
        }
    }

    public float getSoundLength(String name) {
        Sound sound = findSound(name);

        if (sound == null) {
            return 0;
        }

        return sound.clip.length;
    }
}
