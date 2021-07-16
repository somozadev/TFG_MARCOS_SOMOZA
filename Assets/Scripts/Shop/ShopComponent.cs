using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopComponent : MonoBehaviour
{
    [SerializeField] Shop shop;

    public List<Transform> itemSpots;

    public Shop setShop { set { shop = value; } }

    private void Awake()
    {
        if (GetComponentInParent<Room>() != null)
            GetComponentInParent<Room>().onRoomCompleted += SubsInit;
    }

    private void SubsInit()
    {
        foreach (ShopSlot slot in GetComponentsInChildren<ShopSlot>())
            itemSpots.Add(slot.transform);
        StartCoroutine(InitializeShop());

        foreach (Transform slot in itemSpots)
        {
            slot.GetComponent<ShopSlot>().RotateStuff();
        }

    }

    public IEnumerator InitializeShop()
    {
        yield return new WaitForSeconds(.5f);
        int i = 0;
        foreach (ShopItem item in shop.shopItems)
        {
            GameObject aux = Instantiate(item.Prefab, itemSpots[i].position, Quaternion.identity, itemSpots[i].transform);
            aux.GetComponent<SceneItem>().item = item.Item;
            if (GameManager.Instance.player.extraStats.Sales)
                aux.GetComponent<SceneItem>().item.Price = (int) item.Price / 2;
            else
                aux.GetComponent<SceneItem>().item.Price = item.Price;
            aux.GetComponent<SceneItem>().action = ItemAction.BUY;
            aux.GetComponent<SceneItem>().Initialize();
            if (GameManager.Instance.player.extraStats.Sales)
                itemSpots[shop.shopItems.IndexOf(item)].GetComponentInChildren<TMP_Text>().text = ((int)item.Price / 2).ToString();
            else
                itemSpots[shop.shopItems.IndexOf(item)].GetComponentInChildren<TMP_Text>().text = item.Price.ToString();
            itemSpots[i].GetComponent<ShopSlot>().itemToSell = aux;
            i++;
        }
    }


}
