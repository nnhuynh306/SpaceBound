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

    public void play(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = sound.output;

        sound.source.Play();
    }

    public void Start() {
        play("Theme");
    }

    public void playOneAtATime(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

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
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        sound.source.Stop();  
    }

    public void play(string name, float volume) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        sound.source.volume = volume;
        sound.source.clip = sound.clip;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = sound.output;

        sound.source.Play();
    }

     public void playOneAtATime(string name, float volume) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

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
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        return sound.clip.length;
    }
}
