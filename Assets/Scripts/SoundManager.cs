using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [Header("Audio sprites")]
    [Space(20)]
    public Sprite audioOff;
    public Sprite audioOn;
    [Header("Volume info")]
    [Space(20)]
    public float masterVol;
    public float musicVol;
    public float fxVol;
    public float uiVol;
    public bool masterVolMute, musicVolMute, fxVolMute, uiVolMute;
    [SerializeField] Sound currentTheme;
    [Space(20)]
    [Header("Sounds")]
    [Space(20)]
    [SerializeField] Sound[] sounds;


    public AudioMixer GetAudioMixer { get { return audioMixer; } }

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



    public void MuteGeneral() { masterVol = 20; audioMixer.SetFloat("MasterVolume", -80f); }
    public void UnMuteGeneral() { audioMixer.SetFloat("MasterVolume", masterVol); masterVol = 20; }
    public void MuteMusic() { musicVol = -12; audioMixer.SetFloat("MUSICVolume", -80f); }
    public void UnMuteMusic() { audioMixer.SetFloat("MUSICVolume", musicVol); musicVol = -12; }
    public void MuteFx() { fxVol = -6; audioMixer.SetFloat("FXVolume", -80f); }
    public void UnMuteFx() { audioMixer.SetFloat("FXVolume", fxVol); fxVol = -6; }
    public void MuteUi() { uiVol = -20; audioMixer.SetFloat("UIVolume", -80f); }
    public void UnMuteUi() { audioMixer.SetFloat("UIVolume", uiVol); uiVol = -20; }

    public void SetGeneralMute(bool value) { if (!value) MuteGeneral(); else UnMuteGeneral(); masterVolMute = !value; }
    public void SetMusicMute(bool value) { if (!value) MuteMusic(); else UnMuteMusic(); musicVolMute = !value; }
    public void SetFxMute(bool value) { if (!value) MuteFx(); else UnMuteFx(); fxVolMute = !value; }
    public void SetUIMute(bool value) { if (!value) MuteUi(); else UnMuteUi(); uiVolMute = !value; }
    public void SetMuteSprite(Image sprite) { if (sprite.sprite.Equals(audioOn)) sprite.sprite = audioOff; else sprite.sprite = audioOn; }

    public void SetGeneralVolume(float value) { if (masterVolMute) return; audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 10 + 20); }
    public void SetMusicVolume(float value) { if (musicVolMute) return; audioMixer.SetFloat("MUSICVolume", Mathf.Log10(value) * 10 - 24); }
    public void SetFxVolume(float value) { if (fxVolMute) return; audioMixer.SetFloat("FXVolume", Mathf.Log10(value) * 10 - 12); }
    public void SetUiVolume(float value) { if (uiVolMute) return; audioMixer.SetFloat("UIVolume", Mathf.Log10(value) * 10 - 20); }
    public float GetMasterVolume() { float ret; audioMixer.GetFloat("MasterVolume", out ret); return Mathf.Log10(ret) * 10; }
    public float GetMusicVolume() { float ret; audioMixer.GetFloat("MUSICVolume", out ret); return Mathf.Log10(ret) * 10; }
    public float GetFxVolume() { float ret; audioMixer.GetFloat("FXVolume", out ret); return Mathf.Log10(ret) * 10; }
    public float GetUiVolume() { float ret; audioMixer.GetFloat("UIVolume", out ret); return Mathf.Log10(ret) * 10; }






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