using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Sound currentTheme;
    [Space(20)]
    	
    [SerializeField] Sound[] sounds;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        foreach (Sound s in sounds)
        {
            s.sound.source = gameObject.AddComponent<AudioSource>();
            s.sound.source.clip = s.sound.clip;
            s.sound.source.volume = s.sound.volume;
            s.sound.source.pitch = s.sound.pitch;
            s.sound.source.loop = s.sound.loop;
            if (s.sound.mixerGroup.Equals(AudioMixerGroup.FX))
                s.sound.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("FX")[0];
            else if (s.sound.mixerGroup.Equals(AudioMixerGroup.MUSIC))
                s.sound.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("MUSIC")[0];
            else if (s.sound.mixerGroup.Equals(AudioMixerGroup.UI))
                s.sound.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("UI")[0];
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
    public void LowerCurrentTheme()
    {
        currentTheme.sound.volume -= 0.04f;
        currentTheme.sound.pitch -= 0.1f;
        foreach (AudioSource aus in GetComponents<AudioSource>())
        {
            if (aus.clip.name == currentTheme.sound.clip.name)
            {
                aus.volume = currentTheme.sound.volume;
                aus.pitch = currentTheme.sound.pitch;
            }
        }
    }
    public void RestoreCurrentTheme()
    {
        currentTheme.sound.volume += 0.04f;
        currentTheme.sound.pitch += 0.1f;
        foreach (AudioSource aus in GetComponents<AudioSource>())
        {
            if (aus.clip.name == currentTheme.sound.clip.name)
            {
                aus.volume = currentTheme.sound.volume;
                aus.pitch = currentTheme.sound.pitch;
            }
        }
    }
    public void SetCurrentTheme(string theme)
    {
        currentTheme = Array.Find(sounds, sound => sound.name == theme);
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log("Playing: " + s.name);
        s.sound.source.Play();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.sound.source.Pause();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        bool result = s.sound.source.isPlaying;
        return result;
    }


    public void PauseAllOthers(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        foreach (Sound sound in sounds)
        {
            if (sound.sound.mixerGroup == AudioMixerGroup.MUSIC)
            {
                if (sound != s)
                {
                    if (isPlaying(sound.name))
                        Pause(sound.name);
                }
            }
        }
    }



}

[System.Serializable]
public class Sound
{
    public string name;
    public SoundConfig sound;
}

[System.Serializable]
public class SoundConfig
{
    public AudioMixerGroup mixerGroup;
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