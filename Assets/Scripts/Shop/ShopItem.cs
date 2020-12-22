using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item", order = 1)]
[Serializable]
public class ShopItem : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private ItemGenre genre;
    [SerializeField] private Item item;
    public ItemGenre Genre { get { return genre; } set { genre = value; } }
    public string ItemName { get { return itemName; } set { itemName = value; } }
    public Item Item { get { return item; } set { item = value; } }

    private void OnEnable() => NameChecker();

    void NameChecker()
    {
        if (name == "")
        {
            itemName = "default item";
            name = itemName;
        }
        else
            itemName = name;
    }
}


public enum ItemGenre
{
    CONSUMIBLE,
    STATS_MODIFIER,
    WEAPON,
    
}