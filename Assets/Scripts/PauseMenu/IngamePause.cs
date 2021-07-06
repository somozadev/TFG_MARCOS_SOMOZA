using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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


    private void Start()
    {
        GetResolutions();
    }

    public void Pause()
    {
        if (paused)
            return;
        animator.SetTrigger("Pause");
        resume.Select();
        GameManager.Instance.defaultEventSystem.gameObject.SetActive(true);
        GameManager.Instance.playerEventSystem.gameObject.SetActive(false);
        paused = true;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        if (!paused)
            return;
        animator.SetTrigger("UnPause");
        GameManager.Instance.defaultEventSystem.gameObject.SetActive(false);
        GameManager.Instance.playerEventSystem.gameObject.SetActive(true);
        paused = false;
        Time.timeScale = 1;
    }
    public void Video() { animator.SetTrigger("Video"); resolution.Select(); }


    public void Continue() => UnPause();

    public void Settings() { animator.SetTrigger("Config"); video.Select(); }
    public void BackToConfig() { animator.SetTrigger("BackToConfig"); video.Select(); }
    public void BackToPause() { animator.SetTrigger("BackToPause"); resume.Select(); }

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
