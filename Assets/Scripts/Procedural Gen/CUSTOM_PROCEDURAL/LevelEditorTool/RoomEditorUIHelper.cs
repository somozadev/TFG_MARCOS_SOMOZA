using System.Collections;
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

    public List<Toggle> toggles;

    public GameObject AssetsPanelObj { get { return assetsPanel; } }
    public GameObject GridPanelObj { get { return gridPanel; } }
    public Button AssetButton { get { return assetsButton; } }
    public Button GridButton { get { return gridButton; } }

    /*
    * TODO : ADD THE OTHER 3 TYPE OF WALLS AND THE 2 DOORS ***********DONE***********
    * TODO : EXPORT LEVEL FBX ***********DONE***********
    * TODO : DYNAMIC CHANGE GRID SIZE ***********DONE***********
    * TODO : HIDE OTHER BUTTON PANNELS WHEN ONE IS OPEN ***********DONE***********
    * TODO : MOVE CAMERA OFFSET WITH ARROWS KEYBOARD AS WELL ***********DONE***********
    * TODO : LIMIT CAMERA TARGET POSITION TO GRID SIZE ***********DONE***********
    * TODO : MAKE TUTORIAL UI AT BEGINNING WITH ALL CONTROLS ***********DONE***********
    * TODO : DYNAMIC EREASE NODE GRID SLOT (MAYBE HIDE MESH AND RECALCULATE COLLIDER??)  ***********DONE***********
    * TODO : DOORS

    * TODO : DROPS (COINS, HPBOTTLES, TINTEDROCKS...) EDITOR
    * TODO : ENEMIES BY TYPE || ENEMIE POOL SPAWNER
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
        foreach(Toggle t in toggles)
        {
            if(t.isOn)
                returner = System.Convert.ToInt32(t.transform.GetComponentInChildren<TMPro.TMP_Text>(true).text);
        }
        return returner;
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
        // if(enemiesButton != current)
        //     enemiesButton.gameObject.SetActive(false);
        // if(dropsButton != current)
        //     dropsButton.gameObject.SetActive(false);
    }
    public void ShowllOtherButtons()
    {
        gridButton.gameObject.SetActive(true);
        assetsButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(true);
        cameraTargetPosButton.gameObject.SetActive(true);
        // enemiesButton.gameObject.SetActive(true);
        // dropsButton.gameObject.SetActive(true);
    }


}
