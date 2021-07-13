using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour
{
    [Header("Buttons Refs")]
    [SerializeField] Button continueB, newGameB, configB, achievemB, backB, exitB, videoB;
    [Header("Dropdowns Refs")]
    [SerializeField] TMP_Dropdown resolution;


    [Header("Sliders Refs")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider fxSlider;
    [SerializeField] Slider uiSlider;
    [SerializeField] Animator animator;
    private SoundManager soundManager;

    void Start()
    {
        StartCoroutine(WaitForSeed());
        soundManager = GameManager.Instance.soundManager;
        GetResolutions();
    }

    // Waits untill loaded and check if there is a seed in current savefile. 
    private IEnumerator WaitForSeed()
    {
        Debug.Log("Waiting for seed...");
        yield return new WaitUntil(() => DataController.Instance != null);
        Debug.Log(DataController.Instance.currentGameData.seed);
        if (DataController.Instance.currentGameData.seed == "null")
            continueB.interactable = false;
    }

    public void ContinueGame()
    {
        DataController.Instance.newRun = false;
        GameManager.Instance.sceneThemeMusicSelector.SetScene = SCENES.CurrentLevelScene;
        GameManager.Instance.sceneThemeMusicSelector.CheckTheme();
        //SceneController.Instance.LoadScene(SceneName.CurrentLevelScene);//, true);
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene, true);
    }

    public void NewGame()
    {
        GameManager.Instance.dataController.AddAnotherRun();
        DataController.Instance.newRun = true;
        GameManager.Instance.sceneThemeMusicSelector.SetScene = SCENES.CurrentLevelScene;
        GameManager.Instance.sceneThemeMusicSelector.CheckTheme();
        //SceneController.Instance.LoadScene(SceneName.CurrentLevelScene);
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene, true);

    }
    public void Config() { animator.SetTrigger("OpenConfig"); videoB.Select(); }
    public void CloseConfig() { animator.SetTrigger("CloseConfig"); newGameB.Select(); }
    public void Video() { animator.SetTrigger("OpenVideo"); resolution.Select(); }
    public void CloseVideo() { animator.SetTrigger("CloseVideo"); videoB.Select(); }
    public void Sound() { animator.SetTrigger("OpenSound"); }
    public void Progress()
    {

    }
    public void Back() => SceneController.Instance.LoadAdresseableScene(SceneName.SaveFileScene, true); //SceneController.Instance.LoadScene(SceneName.SaveFileScene);
    public void Exit() => GameManager.Instance.ExitGame();


    #region  volumeConfig

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
    #endregion


    #region  videoConfig
    // its duped in IngamePause.... should merge into 1 in the future 
    Resolution[] resolutions;
    int currentResolutionIndex;

    private void GetResolutions()
    {


        resolution.ClearOptions();


        List<string> options = new List<string>();
        currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolution.AddOptions(options);
        resolution.value = currentResolutionIndex;
        resolution.RefreshShownValue();
    }
    public void SetResolution(int index)
    {
        Resolution currRes = resolutions[index];
        Screen.SetResolution(currRes.width, currRes.height, Screen.fullScreen);


    }
    public void SetFullScreen(bool condition)
    {
        Screen.fullScreen = condition;

    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    #endregion



}
