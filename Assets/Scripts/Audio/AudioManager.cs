using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Reference: https://www.youtube.com/watch?v=6OT43pvUyfY
public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;


    private void Awake() {
        foreach(Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
        }
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

        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = sound.output;

        sound.source.Play();
    }

    public void playOneAtATime(string name) {
        Sound sound = findSound(name);

        if (sound == null) {
            return;
        }

        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = sound.output;

        if (!sound.source.isPlaying) {
            sound.source.Play();      
        }  
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

        if (sound == null) {
            return;
        }

        sound.source.volume = volume;
        sound.source.clip = sound.clip;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = sound.output;

        sound.source.Play();
    }

     public void playOneAtATime(string name, float volume) {
        Sound sound = findSound(name);

        if (sound == null) {
            return;
        }

        sound.source.clip = sound.clip;
        sound.source.volume = volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = sound.output;

        if (!sound.source.isPlaying) {
            sound.source.Play();      
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
