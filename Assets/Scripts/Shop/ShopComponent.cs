using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopComponent : MonoBehaviour
{
    [SerializeField] Shop shop;

    public List<Transform> itemSpots;


    private void Start() 
    {   
        var aa = GetComponentsInChildren<Transform>();
        foreach(Transform a in aa)
        {
            if(!a.Equals(gameObject.transform))
            itemSpots.Add(a);
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
