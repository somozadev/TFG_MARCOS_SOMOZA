using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Shop", menuName = "Shop/Shop", order = 1)]
public class Shop : ScriptableObject
{
    public List<ShopItem> shopItems = new List<ShopItem>();
}
