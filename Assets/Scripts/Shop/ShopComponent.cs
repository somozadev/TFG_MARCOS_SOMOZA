﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopComponent : MonoBehaviour
{
    [SerializeField] Shop shop;

    public List<Transform> itemSpots;


    private void Awake()
    {
        foreach (ShopSlot slot in GetComponentsInChildren<ShopSlot>())
            itemSpots.Add(slot.transform);
        InitializeShop();

        foreach (Transform slot in itemSpots)
        {
            slot.GetComponent<ShopSlot>().RotateStuff();
        }
    }



    public void InitializeShop()
    {
        int i = 0;
        foreach (ShopItem item in shop.shopItems)
        {   
            item.Item.Action = ItemAction.BUY;
            GameObject aux = Instantiate(item.Prefab, itemSpots[i].position, Quaternion.identity, itemSpots[i].transform);
            aux.GetComponent<SceneItem>().item = item.Item;
            aux.GetComponent<SceneItem>().item.Price = item.Price;
            aux.GetComponent<SceneItem>().Initialize();
            itemSpots[shop.shopItems.IndexOf(item)].GetComponentInChildren<TMP_Text>().text = item.Price.ToString();
            i++;
            itemSpots[i].GetComponent<ShopSlot>().itemToSell = aux;
        }
    }


}
