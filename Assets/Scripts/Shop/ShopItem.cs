using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item", order = 1)]
public class ShopItem : ScriptableObject
{

    [SerializeField] private string itemName;
    [SerializeField] private Item item;
    public string ItemName { get { return itemName; } set { itemName = value; } }
    public Item Item { get { return item; } set { item = value; } }

    private void OnEnable() => itemName = "Default item";

}
