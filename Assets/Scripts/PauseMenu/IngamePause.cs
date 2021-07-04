using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class IngamePause : MonoBehaviour
{
    [SerializeField] Animator animator;
    int currentResolutionIndex = 0;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        GetResolutions();
    }

    public void Pause() { animator.SetTrigger("Pause"); Time.timeScale = 0; }
    public void UnPause() { animator.SetTrigger("UnPause"); Time.timeScale = 1; }
    public void Video() { animator.SetTrigger("Video"); }


    public void Continue() => UnPause();

    public void Settings() => animator.SetTrigger("Config");
    public void BackToConfig() => animator.SetTrigger("BackToConfig");
    public void BackToPause() => animator.SetTrigger("BackToPause");

    public void Quit() => GameManager.Instance.ExitGame();



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
