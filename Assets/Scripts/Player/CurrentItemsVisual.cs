using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class CurrentItemsVisual : MonoBehaviour
{
    [SerializeField] GameObject iconPrefab;
    void Start()
    {
        InitializeInventory();
    }
    public void AddNewItem(Item item)
    {
        GameObject aux = Instantiate(iconPrefab, Vector3.zero, Quaternion.identity, transform);
        aux.GetComponent<Image>().sprite = item.ItemSprite;
    }
    public void ClearItemsVisuals()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.gameObject != gameObject)
                Destroy(child.gameObject);
        }
    }
    public void InitializeInventory()
    {
        foreach (Item item in GameManager.Instance.player.playerStats.Inventory)
        {
            GameObject aux = Instantiate(iconPrefab, Vector3.zero, Quaternion.identity, transform);
            aux.GetComponent<Image>().sprite = item.ItemSprite;
        }
    }
}
