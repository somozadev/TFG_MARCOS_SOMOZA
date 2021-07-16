using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class SceneItem : MonoBehaviour
{
    public bool canInteract;
    public Item item;
    public ItemAction action;

    private AnimationWoldSpaceCanvas PopUpCanvas;
    [SerializeField] Collider colliderObject;


    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (item != null)
        {
            canInteract = true;
            // item.Transform = this.transform;
            if (action.Equals(ItemAction.INTERACT))
                PopUpCanvas = GetComponentInChildren<AnimationWoldSpaceCanvas>(true);
            if (action.Equals(ItemAction.BUY))
            {
                if (colliderObject != null)
                {
                    colliderObject.enabled = false;
                    colliderObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            if (item.Type.Equals(ItemType.XP)) { }
        }
        // XpScaler();
    }

    public void Contact()
    {
        if (canInteract)
        {
            if (action.Equals(ItemAction.PICK))
            {
                PickAction();
                Destroy(gameObject);
            }
            else if (action.Equals(ItemAction.INTERACT))
                EnablePopUpInteract();
            else if (action.Equals(ItemAction.BUY))
            {
                Debug.Log("Shall animate item");
                GetComponentInParent<ShopSlot>().SelectedItem();
            }
        }
    }
    public void Use()
    {
        if (canInteract)
        {
            if (action.Equals(ItemAction.INTERACT))
            {
                item.InteractAction();
                DisablePopUpInteract();
                // GameManager.Instance.player.playerInteractor.InteractedObject.gameObject.tag = "Untagged";
            }
            else if (action.Equals(ItemAction.BUY))
            {
                BuyAction();
            }
        }
    }
    public void Leave()
    {
        if (canInteract)
        {
            if (action.Equals(ItemAction.INTERACT))
            {
                DisablePopUpInteract();
            }
            else if (action.Equals(ItemAction.BUY))
            {
                GetComponentInParent<ShopSlot>().DeselectedItem();
                Debug.Log("Shall finish animating item if didnt bought");
                Debug.Log("Shall finish animating item if bought");
            }
        }
    }

    private void EnablePopUpInteract() => PopUpCanvas.Activate();
    private void DisablePopUpInteract() => PopUpCanvas.Deactivate();

    public void PickAction()
    {
        switch (item.Type)
        {
            case ItemType.SOULCOIN:
                AddSoulCoin();
                GameManager.Instance.statsCanvas.AssignCoins();
                GameManager.Instance.soundManager.Play("CoinItem");
                break;
            case ItemType.KEY:
                CheckIfExistsOnInventory();
                GameManager.Instance.statsCanvas.AssignKeys();
                break;
            case ItemType.HEALTH:
                CheckIfNeedsHeal();
                GameManager.Instance.statsCanvas.AssignHp();
                GameManager.Instance.soundManager.Play("HealthItem");
                break;
            case ItemType.DMG:
                Debug.LogError("Added to dmg stats: " + (float)item.Cuantity*0.1f);
                Debug.LogError("Current dmg stat: " + GameManager.Instance.player.playerStats.Dmg);

                GameManager.Instance.player.playerStats.AddDmg(((float)item.Cuantity*0.1f));
                GameManager.Instance.soundManager.Play("HealthItem");
                break;
            case ItemType.XP:
                AddXp();
                GameManager.Instance.statsCanvas.AssignXp();
                GameManager.Instance.soundManager.Play("Xp");
                break;
            case ItemType.ITEM:
                AddItemToInventory(item);
                GameManager.Instance.player.currentItemsVisual.AddNewItem(item);
                switch (item.Id)
                {
                    case 2: //Greek glasses
                        item.DoubleShot();
                        break;
                    case 3:// Wings of jisus
                        item.AddWings();
                        break;
                    case 4:// Speed Bow
                        item.AddRange(5f);
                        break;
                    case 15:// Thunder Shot
                        item.AddDmg(20f);
                        item.AddThunderShot();
                        break;
                }
                break;

        }
    }

    #region PICK_METHODS
    ///<summary> Comprueba si existe un item con el mismo id en el inventario, si sí, añade la canditad de this.
    /// Si no, lo añade como nuevo item./></summary>
    private void CheckIfExistsOnInventory()
    {
        if (GameManager.Instance.player.playerStats.Inventory.Exists(x => x.Id == item.Id))
            GameManager.Instance.player.playerStats.Inventory.Find(x => x.Id == item.Id).Cuantity += item.Cuantity;
        else
            GameManager.Instance.player.playerStats.Inventory.Add(item);
    }
    ///<summary> Comprueba si la vida actual del jugador es menor que la máxima posible. Si lo es, se añade vida./></summary>
    private bool CheckIfNeedsHeal()
    {
        bool canHeal = false;
        Debug.Log("CurrentHP:" + GameManager.Instance.player.playerStats.CurrentHp);
        Debug.Log("MaxHP:" + GameManager.Instance.player.playerStats.Hp);

        if (GameManager.Instance.player.playerStats.CurrentHp < GameManager.Instance.player.playerStats.Hp)
        {
            GameManager.Instance.player.playerStats.AddHp(item.Cuantity);
            canHeal = true;
        }

        return canHeal;
    }
    ///<summary> Comprueba si la xp actual del jugador es menor que la máxima posible. Si lo es, se añade xp. Si no, se sube de nivel y se resetea el current xp/></summary>
    private void AddXp()
    {
        if (GameManager.Instance.player.playerStats.ShouldAddXp(item.Cuantity))
        {
            GameManager.Instance.player.playerStats.LevelUp();
            GameManager.Instance.player.playerStats.AddXp(item.Cuantity);
        }
        else
            GameManager.Instance.player.playerStats.AddXp(item.Cuantity);
    }
    ///<summary> Añade la cantidad del this.item a las stats del inventario</summary>
    private void AddSoulCoin() => GameManager.Instance.player.playerStats.SoulCoins += item.Cuantity;

    #endregion



    public void BuyAction()
    {
        bool ShouldBuy = false;

        if (CanAffordBuy())
        {
            Debug.Log("BUY");
            switch (item.Type)
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
                    AddItemToInventory(item);
                    GameManager.Instance.player.currentItemsVisual.AddNewItem(item);
                    break;

            }
            if (ShouldBuy)
            {
                RetrieveSoulCoins();
                GameObject.Destroy(GameManager.Instance.player.playerInteractor.InteractedObject.transform.parent.gameObject);
                if (item.Type.Equals(ItemType.ITEM))
                {
                    switch (item.Id)
                    {
                        case 2: //Greek glasses
                            item.DoubleShot();
                            break;
                        case 3:// Wings of jisus
                            item.AddWings();
                            break;
                        case 4:// Speed Bow
                            item.AddRange(5f);
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


    #region  BUY_METHODS

    private bool CanAffordBuy()
    {
        bool canBuy = false;
        if (GameManager.Instance.player.playerStats.SoulCoins >= item.Price)
            canBuy = true;

        return canBuy;
    }
    private void RetrieveSoulCoins()
    {
        GameManager.Instance.player.playerStats.SoulCoins -= item.Price;
        GameManager.Instance.statsCanvas.AssignCoins();
    }
    private void AddItemToInventory(Item item) => GameManager.Instance.player.playerStats.Inventory.Add(item);

    #endregion





    // private void XpScaler()
    // {
    //     Vector3 aux = (new Vector3(1, 1, 1) * item.Cuantity) / 100;
    //     if (aux.magnitude >= new Vector3(0.1f, 0.1f, 0.1f).magnitude)
    //         item.Transform.localScale = aux;
    //     else
    //         item.Transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

    //     item.Transform.GetComponent<SphereCollider>().radius = (item.Transform.GetComponent<SphereCollider>().radius * item.Transform.localScale.x) / 0.1f;
    // }

}
