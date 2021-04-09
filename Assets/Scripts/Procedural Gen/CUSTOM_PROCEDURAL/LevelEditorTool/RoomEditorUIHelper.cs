using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEditorUIHelper : MonoBehaviour
{
    [SerializeField] GameObject gridPanel;
    [SerializeField] Button gridButton;
    public bool openClosGrid = true;
    
    [SerializeField] GameObject assetsPanel;
    [SerializeField] Button assetsButton;
    public bool openCloseAssets = true;
    
    [SerializeField] GameObject cameraTargetPosPanel;
    // [SerializeField] Button cameraTargetPosButton;
    public bool openClosecameraTargetPos = true;

    [SerializeField] GameObject enemiesPanel;
    [SerializeField] Button enemiesButton;
    public bool openCloseEnemies = true;

    [SerializeField] GameObject dropsPannel;
    [SerializeField] Button dropsButton;
    public bool openCloseDrops = true;

    public GameObject AssetsPanelObj { get { return assetsPanel; } }
    public GameObject GridPanelObj { get { return gridPanel; } }
    public Button AssetButton { get { return assetsButton; } }
    public Button GridButton { get { return gridButton; } }

    /*
    * TODO : ADD THE OTHER 3 TYPE OF WALLS AND THE 2 DOORS ***********DONE***********
    * TODO : EXPORT LEVEL FBX ***********DONE***********
    * TODO : DYNAMIC CHANGE GRID SIZE ***********DONE***********
    * TODO : DYNAMIC EREASE NODE GRID SLOT (MAYBE HIDE MESH AND RECALCULATE COLLIDER??)
    * TODO : HIDE OTHER BUTTON PANNELS WHEN ONE IS OPEN 
    * TODO : DROPS (COINS, HPBOTTLES, TINTEDROCKS...) EDITOR
    */

    public void CameraTargetOffsetPanel()
    {
        if (openClosecameraTargetPos)
        {
            cameraTargetPosPanel.GetComponent<Animator>().SetTrigger("Open");
            openClosecameraTargetPos = false;
        }
        else
        {
            cameraTargetPosPanel.GetComponent<Animator>().SetTrigger("Close");
            openClosecameraTargetPos = true;
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
        // if(enemiesButton != current)
        //     enemiesButton.gameObject.SetActive(false);
        // if(dropsButton != current)
        //     dropsButton.gameObject.SetActive(false);
    }
    public void ShowllOtherButtons()
    {
        gridButton.gameObject.SetActive(true);
        assetsButton.gameObject.SetActive(true);
        // enemiesButton.gameObject.SetActive(true);
        // dropsButton.gameObject.SetActive(true);
    }


}
