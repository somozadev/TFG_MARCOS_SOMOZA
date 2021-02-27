using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComponent : MonoBehaviour
{
    [SerializeField] Shop shop;

    public List<Transform> itemSpots;


    private void Start() 
    {   
        var spots = GetComponentsInChildren<Transform>();
        foreach(Transform spot in spots)
        {
            if(!spot.Equals(gameObject.transform))
            itemSpots.Add(spot);
        }
        InitializeShop();
    }



    public void InitializeShop()
    {
        int i = 0;
        foreach(ShopItem item in shop.shopItems)
        {
            GameObject aux = Instantiate(item.Prefab,itemSpots[i].position,Quaternion.identity,itemSpots[i].transform);
            aux.GetComponent<ShopSceneItem>().Price.text = item.Price.ToString();
            i++;

            //aux.AddComponent<rotacion> addcomponent<ShopLimiter>
        
        }
    }


}
