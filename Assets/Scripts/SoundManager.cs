using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Sound[] sounds;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.mixerGroup.Equals(AudioMixerGroup.FX))
                s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Fx")[0];
            else if (s.mixerGroup.Equals(AudioMixerGroup.MUSIC))
                s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        }
    }

    public void AtenuateOnMenu() => audioMixer.FindSnapshot("OnMenu").TransitionTo(.01f);


    public void DesAtenuateOnMenu() => audioMixer.FindSnapshot("OffMenu").TransitionTo(.01f);


    public void MuteFx(bool mute)
    {
        if (mute)
            audioMixer.SetFloat("fxVolume", -80);
        else
            audioMixer.SetFloat("fxVolume", -10);
    }
    public void MuteMusic(bool mute)
    {
        if (mute)
            audioMixer.SetFloat("musicVolume", -80);
        else
            audioMixer.SetFloat("musicVolume", -10);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        bool result = s.source.isPlaying;
        return result;
    }
}


[System.Serializable]
public class Sound
{
    public AudioMixerGroup mixerGroup;
    public string name;
    public bool loop;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public AudioSource source;
}

public enum AudioMixerGroup
{
    FX, MUSIC, UI
}