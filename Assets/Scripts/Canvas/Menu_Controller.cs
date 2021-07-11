using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour
{

    [SerializeField] Button continueB, newGameB, configB, achievemB, backB, exitB;

    void Start()
    {
        StartCoroutine(WaitForSeed());
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
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene, true);
    }

    public void NewGame()
    {
        DataController.Instance.newRun = true;
        GameManager.Instance.sceneThemeMusicSelector.SetScene = SCENES.CurrentLevelScene;
            GameManager.Instance.sceneThemeMusicSelector.CheckTheme();
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene, true);


    }
    public void Config()
    {

    }
    public void Achievements()
    {

    }
    public void Back() => SceneController.Instance.LoadAdresseableScene(SceneName.SaveFileScene, true);
    public void Exit() => GameManager.Instance.ExitGame();

}
