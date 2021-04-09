using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEditorUIHelper : MonoBehaviour
{
    [SerializeField] GameObject assetsPanel;
    private bool openCloseAssets = true;
    [SerializeField] GameObject enemiesPanel;
    private bool openCloseEnemies = true;
    [SerializeField] GameObject dropsPannel;
    private bool openCloseDrops = true;


    /*
    * TODO : ADD THE OTHER 3 TYPE OF WALLS AND THE 2 DOORS 
    * TODO : HIDE OTHER BUTTON PANNELS WHEN ONE IS OPEN 
    * TODO : DROPS (COINS, HPBOTTLES, TINTEDROCKS...) EDITOR
    * TODO : DYNAMIC CHANGE GRID SIZE && DYNAMIC EREASE NODE GRID SLOT (MAYBE HIDE MESH AND RECALCULATE COLLIDER??)
    * TODO : EXPORT LEVEL FBX
    */


    public void AssetsPanel()
    {
        if (openCloseAssets)
        {
            assetsPanel.GetComponent<Animator>().SetTrigger("Open");
            openCloseAssets = false;
        }
        else
        {
            assetsPanel.GetComponent<Animator>().SetTrigger("Close");
            openCloseAssets = true;
        }
    }

    public void EnemiesPanel()
    {
        if (openCloseEnemies)
        {
            enemiesPanel.GetComponent<Animator>().SetTrigger("Open");
            openCloseEnemies = false;
        }
        else
        {
            enemiesPanel.GetComponent<Animator>().SetTrigger("Close");
            openCloseEnemies = true;
        }

    }
    public void DropsPanel()
    {
        if (openCloseDrops)
        {
            dropsPannel.GetComponent<Animator>().SetTrigger("Open");
            openCloseDrops = false;
        }
        else
        {
            dropsPannel.GetComponent<Animator>().SetTrigger("Close");
            openCloseDrops = true;
        }

    }
}
