using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Shop", menuName = "Shop/Shop", order = 1)]
[Serializable]
public class Shop : ScriptableObject
{
    public List<ShopItem> shopItems = new List<ShopItem>();
    
}
