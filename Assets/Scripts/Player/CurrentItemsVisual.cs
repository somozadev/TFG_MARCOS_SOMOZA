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
        switch (item.Id)
        {
            case 2: //Greek glasses
                item.DoubleShot();
                break;
            case 3:// Wings of jisus
                item.AddWings();
                break;
            case 4:// Speed Bow
                item.AddRange(5f);
                break;
            case 15:// Thunder Shot
                item.AddDmg(20f);
                item.AddThunderShot();
                break;
        }
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
