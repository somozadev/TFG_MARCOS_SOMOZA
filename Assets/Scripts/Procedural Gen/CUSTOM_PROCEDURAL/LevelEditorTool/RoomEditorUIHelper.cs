﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEditorUIHelper : MonoBehaviour
{
    [SerializeField] GameObject gridPanel;
    [SerializeField] Button gridButton;
    public bool openClosGrid = true;

    [SerializeField] GameObject tutorialPanel;
    [SerializeField] Button tutorialButton;
    public bool openClosTutorial = true;

    [SerializeField] GameObject assetsPanel;
    [SerializeField] Button assetsButton;
    public bool openCloseAssets = true;

    [SerializeField] GameObject cameraTargetPosPanel;
    [SerializeField] Button cameraTargetPosButton;
    public bool openClosecameraTargetPos = true;

    [SerializeField] GameObject enemiesPanel;
    [SerializeField] Button enemiesButton;
    public bool openCloseEnemies = true;

    [SerializeField] GameObject dropsPannel;
    [SerializeField] Button dropsButton;
    public bool openCloseDrops = true;

    [SerializeField] GameObject savePannel;
    [SerializeField] Button saveButton;
    public bool openCloseSave = true;

    public List<Toggle> toggles;
    public Toggle bossToggle;
    public Toggle startToggle;

    public GameObject AssetsPanelObj { get { return assetsPanel; } }
    public GameObject GridPanelObj { get { return gridPanel; } }
    public GameObject DropItemPanelObj { get { return dropsPannel; } }
    public GameObject EnemiesPanelObj { get { return enemiesPanel; } }
    public GameObject SavePanelObj { get { return savePannel; } }
    public Button AssetButton { get { return assetsButton; } }
    public Button DropItemButton { get { return dropsButton; } }
    public Button GridButton { get { return gridButton; } }
    public Button EnemiesButton { get { return enemiesButton; } }
    public Button SavePanelButton { get { return saveButton; } }

    /*
    * TODO : ADD THE OTHER 3 TYPE OF WALLS AND THE 2 DOORS ***********DONE***********
    * TODO : EXPORT LEVEL FBX ***********DONE***********
    * TODO : DYNAMIC CHANGE GRID SIZE ***********DONE***********
    * TODO : HIDE OTHER BUTTON PANNELS WHEN ONE IS OPEN ***********DONE***********
    * TODO : MOVE CAMERA OFFSET WITH ARROWS KEYBOARD AS WELL ***********DONE***********
    * TODO : LIMIT CAMERA TARGET POSITION TO GRID SIZE ***********DONE***********
    * TODO : MAKE TUTORIAL UI AT BEGINNING WITH ALL CONTROLS ***********DONE***********
    * TODO : DYNAMIC EREASE NODE GRID SLOT (MAYBE HIDE MESH AND RECALCULATE COLLIDER??)  ***********DONE***********
    * TODO : DOORS ***********DONE***********
    * TODO : DROPS (COINS, HPBOTTLES, TINTEDROCKS...) EDITOR ***********DONE***********
    * TODO : ENEMIES BY TYPE || ENEMIE POOL SPAWNER  ***********DONE***********
    */

    public void OnOffTxt(Toggle current)
    {
        Debug.Log(current.transform.GetComponentInChildren<TMPro.TMP_Text>(true).gameObject.name);
        if (current.isOn)
            current.transform.GetComponentInChildren<TMPro.TMP_Text>(true).gameObject.SetActive(false);
        else
            current.transform.GetComponentInChildren<TMPro.TMP_Text>(true).gameObject.SetActive(true);
    }
    public int GetCurrentToogle()
    {
        int returner = 0;
        foreach (Toggle t in toggles)
        {
            if (t.isOn)
                returner = System.Convert.ToInt32(t.transform.GetComponentInChildren<TMPro.TMP_Text>(true).text);
        }
        return returner;
    }

    private bool GetBossToggle() { return bossToggle.isOn; }
    private bool GetStartToggle() { return startToggle.isOn; }

    public string GetExtraLabel()
    {
        if (GetBossToggle())
            return "BossRoom";
        else if (GetStartToggle())
            return "StartRoom";
        else
            return "none";
    }



    public void CameraTargetOffsetPanel()
    {
        if (openClosecameraTargetPos)
        {
            cameraTargetPosPanel.GetComponent<Animator>().SetTrigger("Open");
            HideAllOtherButtons(cameraTargetPosButton);
            openClosecameraTargetPos = false;
        }
        else
        {
            cameraTargetPosPanel.GetComponent<Animator>().SetTrigger("Close");
            ShowllOtherButtons();
            openClosecameraTargetPos = true;
        }

    }
    public void TutorialPanel()
    {
        if (openClosTutorial)
        {
            tutorialPanel.GetComponent<Animator>().SetTrigger("Open");
            HideAllOtherButtons(tutorialButton);
            openClosTutorial = false;
        }
        else
        {
            tutorialPanel.GetComponent<Animator>().SetTrigger("Close");
            ShowllOtherButtons();
            openClosTutorial = true;
        }
    }
    public void GridPanel()
    {
        if (openClosGrid)
        {
            gridPanel.GetComponent<Animator>().SetTrigger("Open");
            HideAllOtherButtons(gridButton);
            openClosGrid = false;
        }
        else
        {
            gridPanel.GetComponent<Animator>().SetTrigger("Close");
            ShowllOtherButtons();
            openClosGrid = true;
        }
    }
    public void AssetsPanel()
    {
        if (openCloseAssets)
        {
            assetsPanel.GetComponent<Animator>().SetTrigger("Open");
            HideAllOtherButtons(assetsButton);
            openCloseAssets = false;
        }
        else
        {
            assetsPanel.GetComponent<Animator>().SetTrigger("Close");
            ShowllOtherButtons();
            openCloseAssets = true;
        }
    }
    public void EnemiesPanel()
    {
        if (openCloseEnemies)
        {
            enemiesPanel.GetComponent<Animator>().SetTrigger("Open");
            HideAllOtherButtons(enemiesButton);
            openCloseEnemies = false;
        }
        else
        {
            enemiesPanel.GetComponent<Animator>().SetTrigger("Close");
            ShowllOtherButtons();
            openCloseEnemies = true;
        }

    }
    public void DropsPanel()
    {
        if (openCloseDrops)
        {
            dropsPannel.GetComponent<Animator>().SetTrigger("Open");
            HideAllOtherButtons(dropsButton);
            openCloseDrops = false;
        }
        else
        {
            dropsPannel.GetComponent<Animator>().SetTrigger("Close");
            ShowllOtherButtons();
            openCloseDrops = true;
        }

    }
    public void SavePanel()
    {
        if (openCloseSave)
        {
            savePannel.GetComponent<Animator>().SetTrigger("Open");
            HideAllOtherButtons(saveButton);
            openCloseSave = false;
        }
        else
        {
            savePannel.GetComponent<Animator>().SetTrigger("Close");
            ShowllOtherButtons();
            openCloseSave = true;
        }

    }

    public void HideAllOtherButtons(Button current)
    {
        if (gridButton != current)
            gridButton.gameObject.SetActive(false);
        if (assetsButton != current)
            assetsButton.gameObject.SetActive(false);
        if (tutorialButton != current)
            tutorialButton.gameObject.SetActive(false);
        if (cameraTargetPosButton != current)
            cameraTargetPosButton.gameObject.SetActive(false);
        if (dropsButton != current)
            dropsButton.gameObject.SetActive(false);
        if (enemiesButton != current)
            enemiesButton.gameObject.SetActive(false);
        if (saveButton != current)
            saveButton.gameObject.SetActive(false);
    }
    public void ShowllOtherButtons()
    {
        gridButton.gameObject.SetActive(true);
        assetsButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        cameraTargetPosButton.gameObject.SetActive(true);
        dropsButton.gameObject.SetActive(true);
        enemiesButton.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
    }


}
