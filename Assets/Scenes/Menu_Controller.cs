﻿using System.Collections;
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
        Debug.Log("Waiting for princess to be rescued...");
        yield return new WaitUntil(() => DataController.Instance != null);
        Debug.Log(DataController.Instance.currentGameData.seed);
        if (DataController.Instance.currentGameData.seed == "null")
            continueB.interactable = false;
    }
    
    public void ContinueGame()
    {
        DataController.Instance.newRun = false;
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene,true);
    }

    public void NewGame()
    {
        DataController.Instance.newRun = true;
        SceneController.Instance.LoadAdresseableScene(SceneName.CurrentLevelScene,true);
        

    }
    public void Config()
    {

    }
    public void Back() => SceneController.Instance.LoadAdresseableScene(SceneName.SaveFileScene,true);
    public void Exit() => GameManager.Instance.ExitGame();
    
}
