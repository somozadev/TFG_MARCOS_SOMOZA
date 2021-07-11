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
    [SerializeField] bool isOnPauseScreen;
    private SoundManager soundManager;
    private void Start()
    {
        soundManager = GameManager.Instance.soundManager;
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
        soundManager.SetFxVolume(soundManager.fxVol);
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


    private void MuteGeneral() { soundManager.MuteGeneral(); }
    private void UnMuteGeneral() { soundManager.UnMuteGeneral(); }
    private void MuteMusic() { soundManager.MuteMusic(); }
    private void UnMuteMusic() { soundManager.UnMuteMusic(); }
    private void MuteFx() { soundManager.MuteFx(); }
    private void UnMuteFx() { soundManager.UnMuteFx(); }
    private void MuteUi() { soundManager.MuteUi(); }
    private void UnMuteUi() { soundManager.UnMuteUi(); }

    public void SetGeneralMute(bool value) { soundManager.SetGeneralMute(value); }
    public void SetMusicMute(bool value) { soundManager.SetMusicMute(value); }
    public void SetFxMute(bool value) { soundManager.SetFxMute(value); }
    public void SetUIMute(bool value) { soundManager.SetUIMute(value); }
    public void SetMuteSprite(Image sprite) { if (sprite.sprite.Equals(soundManager.audioOn)) sprite.sprite = soundManager.audioOff; else sprite.sprite = soundManager.audioOn; }

    public void SetGeneralVolume(float value) { soundManager.SetGeneralVolume(value); }
    public void SetMusicVolume(float value) { soundManager.SetMusicVolume(value); }
    public void SetFxVolume(float value) { soundManager.SetFxVolume(value); }
    public void SetUiVolume(float value) { soundManager.SetUiVolume(value); }

    private void GetVolumeConfigs()
    {
        masterSlider.value = soundManager.GetMasterVolume();
        musicSlider.value = soundManager.GetMusicVolume();
        fxSlider.value = soundManager.GetFxVolume();
        uiSlider.value = soundManager.GetUiVolume();
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
