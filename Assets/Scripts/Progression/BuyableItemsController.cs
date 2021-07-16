using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyableItemsController : MonoBehaviour
{
    public Sprite noImage;
    [SerializeField]public List<BuyableItem> items;
    [SerializeField] GameObject content;
    public BuyableItem currentSelected;
    [SerializeField] List<BuyableItem> equippedItems;
    public TMP_Text lvl;
    private void Start()
    {


        foreach (BuyableItem item in content.GetComponentsInChildren<BuyableItem>())
        {
            items.Add(item);
            item.controller = this;
            item.Init();
        }
        UpdateText();
    }

    public void UpdateText() => lvl.text = "100";// GameManager.Instance.player.playerStats.Level.ToString();
    public bool HasSpace(BuyableItem asquer)
    {
        int space = 0;
        bool isAlready = false;
        foreach (BuyableItem item in equippedItems)
        {
            if (item.id == "Slot1" || item.id == "Slot2" || item.id == "Slot3")
            {
                space++;
            }
            if (item.id == asquer.id)
                isAlready = true;
        }
        if (space > 0 && !isAlready)
            return true;
        else
            return false;
    }

    public BuyableItem GetFirstFreeSpace()
    {
        BuyableItem returner = null;
        for (int i = 0; i < equippedItems.Count; i++)
        {
            if (equippedItems[i].id == "Slot1" || equippedItems[i].id == "Slot2" || equippedItems[i].id == "Slot3")
            {
                returner = equippedItems[i];
                break;
            }
        }
        return returner;
    }

    public void FillEquipped(BuyableItem item)
    {
        if (HasSpace(item))
        {
            item.Equip();
            GetFirstFreeSpace().Set(item);

        }
    }
}