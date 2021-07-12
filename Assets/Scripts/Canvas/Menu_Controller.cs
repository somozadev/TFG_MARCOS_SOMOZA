using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour
{

    [SerializeField] Button continueB, newGameB, configB, achievemB, backB, exitB, videoB;
    [SerializeField] Animator animator;

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
        //SceneController.Instance.LoadScene(SceneName.CurrentLevelScene);//, true);
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene, true);
    }

    public void NewGame()
    {
        DataController.Instance.newRun = true;
        GameManager.Instance.sceneThemeMusicSelector.SetScene = SCENES.CurrentLevelScene;
        GameManager.Instance.sceneThemeMusicSelector.CheckTheme();
        //SceneController.Instance.LoadScene(SceneName.CurrentLevelScene);
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene, true);

    }
    public void Config() { animator.SetTrigger("OpenConfig"); videoB.Select(); }
    public void CloseConfig() { animator.SetTrigger("CloseConfig"); newGameB.Select(); }
    public void Video() { animator.SetTrigger("OpenVideo"); }
    public void Sound() { animator.SetTrigger("OpenSound"); }
    public void Progress()
    {

    }
    public void Back() => SceneController.Instance.LoadAdresseableScene(SceneName.SaveFileScene, true); //SceneController.Instance.LoadScene(SceneName.SaveFileScene);
    public void Exit() => GameManager.Instance.ExitGame();

}
