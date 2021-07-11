using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneThemeMusicSelector : MonoBehaviour
{
    public SCENES sceneName;
    private void Start()
    {
        CheckTheme();

    }
    public SCENES SetScene { set { sceneName = value; } }
    public void CheckTheme()
    {
        switch (sceneName)
        {
            case SCENES.ESSENTIALS:
            case SCENES.SaveFileScene:
            case SCENES.MainScene:
            case SCENES.MenuScene:

                if (!GameManager.Instance.soundManager.isPlaying("MainTheme"))
                {
                    GameManager.Instance.soundManager.Play("MainTheme");
                    GameManager.Instance.soundManager.SetCurrentTheme("MainTheme");
                    GameManager.Instance.soundManager.PauseAllOthers("MainTheme");
                }
                break;
            case SCENES.CurrentLevelScene:
                if (GameManager.Instance.stageController == null)
                {
                    if (!GameManager.Instance.soundManager.isPlaying("StageOneTheme"))
                    {
                        GameManager.Instance.soundManager.SetCurrentTheme("StageOneTheme");
                        GameManager.Instance.soundManager.Play("StageOneTheme");
                        GameManager.Instance.soundManager.PauseAllOthers("StageOneTheme");
                    }
                }
                else
                {
                    switch (GameManager.Instance.stageController.GetActualStage)
                    {
                        case 1:
                            if (!GameManager.Instance.soundManager.isPlaying("StageOneTheme"))
                            {
                                GameManager.Instance.soundManager.SetCurrentTheme("StageOneTheme");
                                GameManager.Instance.soundManager.Play("StageOneTheme");
                                GameManager.Instance.soundManager.PauseAllOthers("StageOneTheme");
                            }
                            break;
                        case 2:
                            if (!GameManager.Instance.soundManager.isPlaying("StageTwoTheme"))
                            {
                                GameManager.Instance.soundManager.SetCurrentTheme("StageTwoTheme");
                                GameManager.Instance.soundManager.Play("StageTwoTheme");
                                GameManager.Instance.soundManager.PauseAllOthers("StageTwoTheme");
                            }
                            break;
                        case 3:
                            if (!GameManager.Instance.soundManager.isPlaying("StageThreeTheme"))
                            {
                                GameManager.Instance.soundManager.SetCurrentTheme("StageThreeTheme");
                                GameManager.Instance.soundManager.Play("StageThreeTheme");
                                GameManager.Instance.soundManager.PauseAllOthers("StageThreeTheme");
                            }
                            break;
                        case 4:
                            if (!GameManager.Instance.soundManager.isPlaying("StageFourTheme"))
                            {
                                GameManager.Instance.soundManager.SetCurrentTheme("StageFourTheme");
                                GameManager.Instance.soundManager.Play("StageFourTheme");
                                GameManager.Instance.soundManager.PauseAllOthers("StageFourTheme");
                            }
                            break;
                        case 5:
                            if (!GameManager.Instance.soundManager.isPlaying("StageFiveTheme"))
                            {
                                GameManager.Instance.soundManager.SetCurrentTheme("StageFiveTheme");
                                GameManager.Instance.soundManager.Play("StageFiveTheme");
                                GameManager.Instance.soundManager.PauseAllOthers("StageFiveTheme");
                            }
                            break;
                    }
                }
                break;

        }
    }

}

public enum SCENES
{
    ESSENTIALS,
    MainScene,
    CurrentLevelScene,
    SaveFileScene,
    MenuScene
}