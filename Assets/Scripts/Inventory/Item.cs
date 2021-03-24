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
    [SerializeField] private ItemAction action;
    [SerializeField] private Sprite itemSprite;
    // [SerializeField] private Transform transform;
    //[SerializeField] ParticleSystem particlePick;
    public int Id { get { return id; } }
    public int Cuantity { get { return cuantity; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public int Price { get { return price; } set { price = value; } }
    public ItemAction Action { get { return action; } set { action = value; } }
    // public Transform Transform { get { return transform; } set { transform = value; } }
    public ItemType Type { get { return type; } }


    public Item(int id, int cuantity, bool isUnlocked, ItemType type, ItemAction action)//, Transform transform)
    {
        this.id = id;
        this.cuantity = cuantity;
        this.isUnlocked = isUnlocked;
        this.type = type;
        this.action = action;
        // this.transform = transform;
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
                GameManager.Instance.statsCanvas.AssignHp();
                break;
            case ItemType.XP:
                AddXp();
                GameManager.Instance.statsCanvas.AssignXp();
                break;

        }
    }

    public void BuyAction()
    {
        bool ShouldBuy = false;

        if (CanAffordBuy())
        {
            Debug.Log("BUY");
            switch (type)
            {
                case ItemType.KEY:
                    ShouldBuy = true;
                    CheckIfExistsOnInventory();
                    GameManager.Instance.statsCanvas.AssignKeys();
                    break;
                case ItemType.HEALTH:
                    if (CheckIfNeedsHeal())
                    {
                        ShouldBuy = true;
                        GameManager.Instance.statsCanvas.AssignHp();
                    }
                    break;
                case ItemType.ITEM:
                    ShouldBuy = true;
                    AddItemToInventory(this);
                    GameManager.Instance.player.currentItemsVisual.AddNewItem(this);
                    break;

            }
            if (ShouldBuy)
            {
                RetrieveSoulCoins();
                GameObject.Destroy(GameManager.Instance.player.playerInteractor.InteractedObject.transform.parent.gameObject);
                if (type.Equals(ItemType.ITEM))
                {
                    switch (id)
                    {
                        case 2: //Greek glasses
                            DoubleShot();
                            break;
                        case 3:// Wings of jisus
                            AddWings();
                            break;
                        case 4:// Speed Bow
                            AddRange(5f);
                            break;
                    }
                }
            }

        }
        else
        {

            GameManager.Instance.player.playerInteractor.InteractedObject.GetComponent<SceneItem>().GetComponentInParent<ShopSlot>().CantAfford();
            Debug.Log("CANT BUY");
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
    private bool CheckIfNeedsHeal()
    {
        bool canHeal = false;
        Debug.Log("CurrentHP:" + GameManager.Instance.player.playerStats.CurrentHp);
        Debug.Log("MaxHP:" + GameManager.Instance.player.playerStats.Hp);

        if (GameManager.Instance.player.playerStats.CurrentHp < GameManager.Instance.player.playerStats.Hp)
        {
            GameManager.Instance.player.playerStats.AddHp(this.cuantity);
            canHeal = true;
        }

        return canHeal;
    }
    ///<summary> Comprueba si la xp actual del jugador es menor que la máxima posible. Si lo es, se añade xp. Si no, se sube de nivel y se resetea el current xp/></summary>
    private void AddXp()
    {
        if (GameManager.Instance.player.playerStats.ShouldAddXp(this.cuantity))
            GameManager.Instance.player.playerStats.LevelUp();
        else
            GameManager.Instance.player.playerStats.AddXp(this.cuantity);
    }
    ///<summary> Añade la cantidad del this. item a las stats del inventario</summary>
    private void AddSoulCoin() => GameManager.Instance.player.playerStats.SoulCoins += this.cuantity;

    #endregion

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

    #region  BUY_METHODS

    private bool CanAffordBuy()
    {
        bool canBuy = false;
        if (GameManager.Instance.player.playerStats.SoulCoins > this.price)
            canBuy = true;

        return canBuy;
    }
    private void RetrieveSoulCoins() => GameManager.Instance.player.playerStats.SoulCoins -= this.Price;
    private void AddItemToInventory(Item item) => GameManager.Instance.player.playerStats.Inventory.Add(item);

    #endregion

    #region ITEM_METHODS

    private void DoubleShot()
    {
        GameManager.Instance.player.extraStats.NumberOfShots = 2;
    }
    private void AddRange(float range)
    {
        GameManager.Instance.player.playerStats.Range+=range;
    }
    private void AddWings()
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