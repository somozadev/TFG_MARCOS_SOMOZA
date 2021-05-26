using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", order = 1)]
[Serializable]
public class Item : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private int cuantity;
    [SerializeField] private int price;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private ItemType type;
    // [SerializeField] private ItemAction action;
    [SerializeField] private Sprite itemSprite;

    public int Id { get { return id; } }
    public int Cuantity { get { return cuantity; } set { cuantity = value; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public int Price { get { return price; } set { price = value; } }
    // public ItemAction Action { get { return action; } set { action = value; } }
    public ItemType Type { get { return type; } }


    public Item(int id, int cuantity, bool isUnlocked, ItemType type)//, ItemAction action)
    {
        this.id = id;
        this.cuantity = cuantity;
        this.isUnlocked = isUnlocked;
        this.type = type;
        // this.action = action;
    }

    public void InteractAction()
    {
        switch (type)
        {
            case ItemType.CHEST:
                break;
            case ItemType.DOOR:
                OpenDoor();
                break;
        }
        Debug.Log("Interacted");
    }








    #region INTERACT_METHODS
    private void OpenChest()
    {

    }
    private void OpenDoor()
    {
        GameManager.Instance.player.playerInteractor.InteractedObject.GetComponent<Animator>().SetTrigger("Open");
        //start coroutine to change stance
    }
    #endregion



    #region ITEM_METHODS

    public void DoubleShot()
    {
        GameManager.Instance.player.extraStats.NumberOfShots = 2;
    }
    public void AddRange(float range)
    {
        GameManager.Instance.player.playerStats.Range += range;
    }
    public void AddWings()
    {

        GameManager.Instance.player.playerMovement.Levitate();
    }


    #endregion
}



public enum ItemType
{
    SOULCOIN,
    KEY,
    HEALTH,
    DMG,
    XP,
    CHEST,
    DOOR,
    ITEM,
    //BOOSSITEM 
}

public enum ItemAction
{
    PICK,
    INTERACT,
    BUY,
}