using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyableItem : MonoBehaviour
{

    public string id;
    public int price;
    public Sprite image;
    public BuyableState state;
    public BuyableEquipped equipped;
    ColorBlock colors;
    ColorBlock colorsInvalid;

    public bool chosedToEquipped;

    public BuyableItemsController controller; //[HideInInspector]

    private void Awake()
    {
        id = gameObject.name;
        colors = GetComponent<Button>().colors;
        colorsInvalid = GetComponent<Button>().colors;
        colorsInvalid.normalColor = new Color32(255, 174, 174, 255);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(id + "_Equipped") > 0 && transform.parent.gameObject.name != "CurrentBackground")
        {
            equipped = BuyableEquipped.EQUIPPED;
            controller.FillEquipped(this);

            if (!GameManager.Instance.player.rocks.currentItems.Contains(id))
                GameManager.Instance.player.rocks.currentItems.Add(id);
            GetComponent<Button>().interactable = false;
        }
    }
    public void Equip()
    {
        equipped = BuyableEquipped.EQUIPPED;

        if (!GameManager.Instance.player.rocks.currentItems.Contains(id))
            GameManager.Instance.player.rocks.currentItems.Add(id);
        PlayerPrefs.SetInt(id + "_Equipped", 1);
    }

    public void Init()
    {
        CheckState();
        if (equipped.Equals(BuyableEquipped.NOT_EQUIPPED))
        {
            if (state.Equals(BuyableState.BOUGHT))
            {
                price = int.Parse(GetComponentInChildren<TMP_Text>().text);
                GetComponentInChildren<TMP_Text>().text = "BOUGHT";

                price = 0;
            }
            else
                price = int.Parse(GetComponentInChildren<TMP_Text>().text);
        }
        else
        {
        }
        // image = GetComponentInChildren<Image>().sprite;
    }
    public void BuyItem()
    {
        if (state.Equals(BuyableState.NOT_BOUGHT))
        {
            if (PlayerPrefs.GetInt("level", 0) >= price)//if (100 >= price)//
            {
                GetComponent<Button>().colors = colors;
                GameManager.Instance.player.playerStats.LevelSpend(price);
                Debug.LogError("LEVEL BE LIKE WOW>"+PlayerPrefs.GetInt("level", 0));
                price = 0;
                state = BuyableState.BOUGHT;
                Debug.Log("BOUGHT ID: " + id);
                PlayerPrefs.SetInt(id, 1);
                PlayerPrefs.Save();
                controller.UpdateText();
                GetComponentInChildren<TMP_Text>().text = "BOUGHT";
            }
            else
            {
                GetComponent<Button>().colors = colorsInvalid;
            }
        }
        else
        {
            if (controller.currentSelected == null || controller.currentSelected != this)
            {
                BuyableItem aux = null;
                if (controller.currentSelected != null)
                    aux = controller.currentSelected;
                chosedToEquipped = true;
                controller.currentSelected = this;
                if (controller.currentSelected != null && controller.currentSelected != this)
                    aux.chosedToEquipped = false;
            }
            if (controller.HasSpace(this))
            {
                Equip();
                controller.GetFirstFreeSpace().Set(this);
            }
        }
    }
    private void CheckState()
    {
        Debug.Log(gameObject.name + " >" + PlayerPrefs.GetInt(id, 0));
        switch (PlayerPrefs.GetInt(id, 0))
        {
            case 0:
                state = BuyableState.NOT_BOUGHT;
                break;
            case 1:
                state = BuyableState.BOUGHT;
                break;

        }
    }

    public void Set(BuyableItem item)
    {
        this.id = item.id;
        this.price = item.price;
        this.image = item.image;
        this.state = item.state;
        this.equipped = item.equipped;
        this.chosedToEquipped = item.chosedToEquipped;
        this.controller = item.controller;
        this.transform.GetChild(1).GetComponent<Image>().sprite = this.image;
        this.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        this.Init();

    }


    public void RestoreCurrentSlot()
    {
        if (this.equipped.Equals(BuyableEquipped.NOT_EQUIPPED))
            return;
        string auxId = this.id;
        this.id = gameObject.name;
        this.state = BuyableState.NOT_BOUGHT;
        this.equipped = BuyableEquipped.NOT_EQUIPPED;
        GameManager.Instance.player.rocks.currentItems.Remove(auxId);

        PlayerPrefs.SetInt(auxId + "_Equipped", 0);
        PlayerPrefs.Save();
        this.price = 0;
        this.chosedToEquipped = false;

        this.transform.GetChild(1).GetComponent<Image>().sprite = controller.noImage;
        this.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 128);
        foreach (BuyableItem item in controller.items)
        { item.CheckEquipped(auxId); }

    }

    public void CheckEquipped(string auxId)
    {
        // Debug.LogError(this.id + " IS: " + (BuyableEquipped)PlayerPrefs.GetInt(id + "_Equipped", 0));
        // this.equipped = (BuyableEquipped)PlayerPrefs.GetInt(id + "_Equipped", 0);
        if (this.id.Equals(auxId))
            GetComponent<Button>().interactable = true;
    }
}


public enum BuyableState
{
    NOT_BOUGHT,
    BOUGHT
}
public enum BuyableEquipped
{
    NOT_EQUIPPED,
    EQUIPPED
}