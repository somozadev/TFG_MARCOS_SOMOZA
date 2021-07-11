using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Audio;

public class IngamePause : MonoBehaviour
{
    [SerializeField] Animator animator;
    int currentResolutionIndex = 0;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public bool paused;

    [Header("Buttons Refs")]
    [SerializeField] Button resume;
    [SerializeField] Button video;
    [SerializeField] TMP_Dropdown resolution;
    [Space(20)]
    [Header("Sliders Refs")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider fxSlider;
    [SerializeField] Slider uiSlider;
    [SerializeField] Sprite audioOff;
    [SerializeField] Sprite audioOn;
    [Space(20)]
    [Header("Volume info")]
    [SerializeField] private float masterVol;
    [SerializeField] private float musicVol;
    [SerializeField] private float fxVol;
    [SerializeField] private float uiVol;
    [SerializeField] private bool masterVolMute, musicVolMute, fxVolMute, uiVolMute;
    private AudioMixer audioMixer;
    [SerializeField] bool isOnPauseScreen;
    private void Start()
    {
        audioMixer = GameManager.Instance.soundManager.GetAudioMixer;
        GetResolutions();
        //GetVolumeConfigs();
    }

    public void Pause()
    {
        if (paused)
            return;
        isOnPauseScreen = true;
        GameManager.Instance.soundManager.Play("OpenPause");
        GameManager.Instance.soundManager.LowerCurrentTheme();
        animator.SetTrigger("Pause");
        resume.Select();
        SetFxVolume(0.3f);
        fxSlider.value = 0.3f;
        GameManager.Instance.defaultEventSystem.gameObject.SetActive(true);
        GameManager.Instance.playerEventSystem.gameObject.SetActive(false);
        paused = true;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        if (!paused || !isOnPauseScreen)
            return;

        GameManager.Instance.soundManager.Play("OpenPause");
        GameManager.Instance.soundManager.RestoreCurrentTheme();
        animator.SetTrigger("UnPause");
        SetFxVolume(fxVol);
        GameManager.Instance.defaultEventSystem.gameObject.SetActive(false);
        GameManager.Instance.playerEventSystem.gameObject.SetActive(true);
        paused = false;
        Time.timeScale = 1;
    }
    public void Video() { animator.SetTrigger("Video"); resolution.Select(); isOnPauseScreen = false; }
    public void Audio() { animator.SetTrigger("Audio"); masterSlider.Select(); isOnPauseScreen = false; }


    public void Continue() => UnPause();

    public void Settings() { animator.SetTrigger("Config"); video.Select(); isOnPauseScreen = false; }
    public void BackToConfig() { animator.SetTrigger("BackToConfig"); video.Select(); isOnPauseScreen = false; }
    public void BackToPause() { animator.SetTrigger("BackToPause"); resume.Select(); isOnPauseScreen = true; }
    public void BackToConfigFromAudio() { animator.SetTrigger("BackToConfigFromAudio"); video.Select(); isOnPauseScreen = false; }
    public void Quit() => GameManager.Instance.ExitGame();


    private void MuteGeneral() { masterVol = 20; audioMixer.SetFloat("MasterVolume", -80f); }
    private void UnMuteGeneral() { audioMixer.SetFloat("MasterVolume", masterVol); masterVol = 20; }
    private void MuteMusic() { musicVol = -12; audioMixer.SetFloat("MUSICVolume", -80f); }
    private void UnMuteMusic() { audioMixer.SetFloat("MUSICVolume", musicVol); musicVol = -12; }
    private void MuteFx() { fxVol = -6; audioMixer.SetFloat("FXVolume", -80f); }
    private void UnMuteFx() { audioMixer.SetFloat("FXVolume", fxVol); fxVol = -6; }
    private void MuteUi() { uiVol = -20; audioMixer.SetFloat("UIVolume", -80f); }
    private void UnMuteUi() { audioMixer.SetFloat("UIVolume", uiVol); uiVol = -20; }

    public void SetGeneralMute(bool value) { if (!value) MuteGeneral(); else UnMuteGeneral(); masterVolMute = !value; }
    public void SetMusicMute(bool value) { if (!value) MuteMusic(); else UnMuteMusic(); musicVolMute = !value; }
    public void SetFxMute(bool value) { if (!value) MuteFx(); else UnMuteFx(); fxVolMute = !value; }
    public void SetUIMute(bool value) { if (!value) MuteUi(); else UnMuteUi(); uiVolMute = !value; }
    public void SetMuteSprite(Image sprite) { if (sprite.sprite.Equals(audioOn)) sprite.sprite = audioOff; else sprite.sprite = audioOn; }

    public void SetGeneralVolume(float value) { if (masterVolMute) return; audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 10 + 20); }
    public void SetMusicVolume(float value) { if (musicVolMute) return; audioMixer.SetFloat("MUSICVolume", Mathf.Log10(value) * 10 - 24); }
    public void SetFxVolume(float value) { if (fxVolMute) return; audioMixer.SetFloat("FXVolume", Mathf.Log10(value) * 10 - 12); }
    public void SetUiVolume(float value) { if (uiVolMute) return; audioMixer.SetFloat("UIVolume", Mathf.Log10(value) * 10 - 40); }
    public float GetMasterVolume() { float ret; audioMixer.GetFloat("MasterVolume", out ret); return Mathf.Log10(ret) * 10; }
    public float GetMusicVolume() { float ret; audioMixer.GetFloat("MUSICVolume", out ret); return Mathf.Log10(ret) * 10; }
    public float GetFxVolume() { float ret; audioMixer.GetFloat("FXVolume", out ret); return Mathf.Log10(ret) * 10; }
    public float GetUiVolume() { float ret; audioMixer.GetFloat("UIVolume", out ret); return Mathf.Log10(ret) * 10; }
    private void GetVolumeConfigs()
    {
        masterSlider.value = GetMasterVolume();
        musicSlider.value = GetMusicVolume();
        fxSlider.value = GetFxVolume();
        uiSlider.value = GetUiVolume();
    }

    private void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution()
    {
        Resolution resolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFullScreen(bool condition) => Screen.fullScreen = condition;
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

}
