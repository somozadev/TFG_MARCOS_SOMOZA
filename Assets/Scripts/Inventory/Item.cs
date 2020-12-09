using System;
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
    public Transform transform;
    //[SerializeField] ParticleSystem particlePick;
    public int Id { get { return id; } }
    public int Cuantity { get { return cuantity; } }
    public ItemAction Action  { get { return action; } }


    public Item(int id, int cuantity, bool isUnlocked, ItemType type, ItemAction action, Transform transform)
    {
        this.id = id;
        this.cuantity = cuantity;
        this.isUnlocked = isUnlocked;
        this.type = type;
        this.action = action;
        this.transform = transform;
    }

    public void InteractAction()
    {
        Debug.Log("Interacted");
    }


    public void PickAction()
    {
        switch (type)
        {
            case ItemType.SOULCOIN:
                AddSoulCoin();
                GameManager.Instance.statsCanvas.AssignCoins();
                break;
            case ItemType.KEY:
                CheckIfExistsOnInventory();
                GameManager.Instance.statsCanvas.AssignKeys();
                break;
            case ItemType.HEALTH:
                CheckIfNeedsHeal();
                break;
            case ItemType.XP:
                AddXp();
                break;
            case ItemType.CHEST:
                break;

        }
    }
    private void CheckItemInteractType()
    {
        switch (type)
        {
            case ItemType.CHEST:
                OpenChest();
                break;

        }
    }





    #region PICK_METHODS
    ///<summary> Comprueba si existe un item con el mismo id en el inventario, si sí, añade la canditad de this.
    /// Si no, lo añade como nuevo item./></summary>
    private void CheckIfExistsOnInventory()
    {
        if (GameManager.Instance.player.playerStats.Inventory.Exists(x => x.id == this.id))
            GameManager.Instance.player.playerStats.Inventory.Find(x => x.id == this.id).cuantity += this.cuantity;
        else
            GameManager.Instance.player.playerStats.Inventory.Add(this);
    }
    ///<summary> Comprueba si la vida actual del jugador es menor que la máxima posible. Si lo es, se añade vida./></summary>
    private void CheckIfNeedsHeal()
    {
        Debug.Log("CurrentHP:" + GameManager.Instance.player.playerStats.CurrentHp);
        Debug.Log("MaxHP:" + GameManager.Instance.player.playerStats.Hp);

        if (GameManager.Instance.player.playerStats.CurrentHp < GameManager.Instance.player.playerStats.Hp)
            GameManager.Instance.player.playerStats.AddHp(this.cuantity); 
    }
    ///<summary> Comprueba si la xp actual del jugador es menor que la máxima posible. Si lo es, se añade xp. Si no, se sube de nivel y se resetea el current xp/></summary>
    private void AddXp()
    {
        if (GameManager.Instance.player.playerStats.CurrentXp >= GameManager.Instance.player.playerStats.Xp)
            GameManager.Instance.player.playerStats.LevelUp();
        else
            GameManager.Instance.player.playerStats.AddXp(this.cuantity);
    }

    private void AddSoulCoin() => GameManager.Instance.player.playerStats.SoulCoins += this.cuantity;

    #endregion

    #region INTERACT_METHODS
    private void OpenChest()
    {

    }
    #endregion

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