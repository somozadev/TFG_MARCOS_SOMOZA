using UnityEngine;

[System.Serializable]
public static class ItemPicker 
{

    private static int startId = 100;
    private static int endId = 104; //current last itemId is 103, so 103+1
    public static Item[] items;
    public static SceneItem[] itemsPrefab;

    private static Item[] FillItems
    {
        get
        {
            Object[] objItems = Resources.LoadAll("ItemsScriptable", typeof(Item));
            Item[] itemsI = new Item[objItems.Length];
            for (int i = 0; i < objItems.Length; i++)
            {
                itemsI[i] = (Item)objItems[i];
            }

            return itemsI;
        }
    }
    private static SceneItem[] FillPrefab
    {
        get
        {
            Object[] objItems = Resources.LoadAll("ItemsPrefab", typeof(SceneItem));
            SceneItem[] itemsIp = new SceneItem[objItems.Length];
            for (int i = 0; i < objItems.Length; i++)
            {
                itemsIp[i] = (SceneItem)objItems[i];
            }

            return itemsIp;
        }
    }

    // public static GameObject rndGo;
    // public static Item rndIt;



    public static GameObject RNGuniItemObject
    {
        get
        {
            int rnd = Random.Range(startId, endId);
            Debug.LogError("rnd obj:" + rnd);
            GameObject returner = new GameObject();
            itemsPrefab = FillPrefab;
            foreach (SceneItem sceneItem in itemsPrefab)
            {
                Debug.LogError("Item:" + sceneItem.name);
                if (sceneItem.item != null && sceneItem.item.Id.Equals(rnd))
                {
                    returner = sceneItem.gameObject;
                }
            }
            return returner;
        }
    }
    public static Item RNGItem
    {
        get
        {
            int rnd = Random.Range(startId, endId);
            Debug.LogError("rnd item:" + rnd);
            Item item = null;
            items = FillItems;
            foreach (Item i in items)
            {
                Debug.LogError("Item" + i.name);
                if (i != null && i.Id.Equals(rnd))
                {
                    item = i;
                }
            }
            return item;
        }
    }
}
