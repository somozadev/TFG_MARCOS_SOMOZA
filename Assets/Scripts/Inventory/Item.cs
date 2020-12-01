using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] private int id;
    [SerializeField] private int cuantity;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private ItemType type;
    [SerializeField] private ItemAction action;

    public Item(int id, int cuantity, bool isUnlocked, ItemType type, ItemAction action)
    {
        this.id = id;
        this.cuantity = cuantity;
        this.isUnlocked = isUnlocked;
        this.type = type;
        this.action = action;
    }

    public void TriggerAction()
    {
        switch (action)
        {
            case ItemAction.PICK:
                CheckItemType();
                break;
            case ItemAction.INTERACT:
                break;
        }
    }

    private void CheckItemType()
    {
        switch (type)
        {
            case ItemType.SOULCOIN:
                CheckIfExistsOnInventory();
                break;
            case ItemType.KEY:
                CheckIfExistsOnInventory();
                break;
            case ItemType.HEALTH:
                break;
            case ItemType.XP:
                break;
            case ItemType.CHEST:
                break;

        }
    }
    
    
    ///<summary> Comprueba si existe un item con el mismo id en el inventario, si sí, añade la canditad de this.
    /// Si no, lo añade como nuevo item./></summary>
    private void CheckIfExistsOnInventory()
    {
        if (GameManager.Instance.player.playerStats.Inventory.Exists(x => x.id == this.id))
            GameManager.Instance.player.playerStats.Inventory.Find(x => x.id == this.id).cuantity += this.cuantity;
        else
            GameManager.Instance.player.playerStats.Inventory.Add(this);
    }

}



public enum ItemType
{
    SOULCOIN,
    KEY,
    HEALTH,
    XP,
    CHEST,
    //BOOSSITEM 
}

public enum ItemAction
{
    PICK,
    INTERACT,
}