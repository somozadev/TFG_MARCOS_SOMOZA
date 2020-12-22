using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Shop))]
public class ShopEditor : Editor
{
    private Shop shop;
    private string itemsAssetDatabasePath = "Assets/Scripts/Shop/ShopItems";

    public override void OnInspectorGUI()
    {
        shop = (Shop)target;
        SerializedObject serializedObject = new SerializedObject(shop);
        serializedObject.Update();
        SerializedProperty shopItems = serializedObject.FindProperty("shopItems");
        Draw(shopItems);

    }

    private void Draw(SerializedProperty list)
    {

        // EditorGUILayout.PropertyField(list);
        if (list.isExpanded)
        {
            EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            ShowElements(list,false);
        }
        GUILayout.Space(20);

        if (GUILayout.Button("Add Random Item",GUILayout.Height(70)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
            
            string path = GetRandomItem(); 
            ShopItem auxRandomShopItem = AssetDatabase.LoadAssetAtPath(path,(typeof(ShopItem))) as ShopItem;
            ShopItem randomShopItem = Object.Instantiate(auxRandomShopItem);
            randomShopItem.name = randomShopItem.ItemName;
            shop.shopItems.Add(randomShopItem);
            AssetDatabase.AddObjectToAsset(randomShopItem, shop);
            AssetDatabase.SaveAssets();
           
        }        
        if (GUILayout.Button("Add Random Consumible",GUILayout.Height(30)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
           
            ShopItem auxShopItem = GetRandomItemByGenre(ItemGenre.CONSUMIBLE);
            ShopItem emptyShopItem = Object.Instantiate(auxShopItem);
            emptyShopItem.name = emptyShopItem.ItemName;
            shop.shopItems.Add(emptyShopItem);
            AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
            AssetDatabase.SaveAssets();
        }
        if (GUILayout.Button("Add Random Weapon",GUILayout.Height(30)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
           
           
            ShopItem auxShopItem = GetRandomItemByGenre(ItemGenre.WEAPON);
            ShopItem emptyShopItem = Object.Instantiate(auxShopItem);
            emptyShopItem.name = emptyShopItem.ItemName;
            shop.shopItems.Add(emptyShopItem);
            AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
            AssetDatabase.SaveAssets();
        }
        if (GUILayout.Button("Add Random Modifier",GUILayout.Height(30)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;

            ShopItem auxShopItem = GetRandomItemByGenre(ItemGenre.STATS_MODIFIER);
            ShopItem emptyShopItem = Object.Instantiate(auxShopItem);
            emptyShopItem.name = emptyShopItem.ItemName;
            shop.shopItems.Add(emptyShopItem);
            AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
            AssetDatabase.SaveAssets();
        }
        if (GUILayout.Button("Add Empty Item",GUILayout.Height(20)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
           
            ShopItem emptyShopItem = ScriptableObject.CreateInstance<ShopItem>();
            emptyShopItem.name = emptyShopItem.ItemName;
            shop.shopItems.Add(emptyShopItem);
            AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
            AssetDatabase.SaveAssets();
        } 
        

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear Shop",GUILayout.Height(20), GUILayout.Width(100)))
        {  
            for (int i = 0; i < list.arraySize; i++)
                Object.DestroyImmediate(list.GetArrayElementAtIndex(i).objectReferenceValue, true);
            
            shop.shopItems.Clear();
            shop.shopItems.TrimExcess();
            AssetDatabase.SaveAssets();
        }

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Export package",GUILayout.Height(20), GUILayout.Width(100)))
        {        
            AssetDatabase.SaveAssets();
            AssetDatabase.ExportPackage("Assets/Scripts/Shop/Shop.asset","Assets/MyPackages/shop.unitypackage",ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);
        }
        EditorGUILayout.EndHorizontal();

    }


    ///
    private string[] GetAllItemsPath()
    {
        string[] allItems = AssetDatabase.FindAssets("t:ShopItem",new[] {itemsAssetDatabasePath});
        return allItems;
    }
    private List <ShopItem> GetAllItems()
    {
        List <ShopItem> shopItems = new List<ShopItem>();
        foreach(string item in GetAllItemsPath())
        {
            ShopItem aux = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(item),(typeof(ShopItem))) as ShopItem;
            shopItems.Add(aux);
        }

        return shopItems;

    }

    private ShopItem GetRandomItemByGenre(ItemGenre genre)
    {   
        List <ShopItem> shopItemsGenre  = new List<ShopItem>(); 

        foreach(ShopItem item in GetAllItems())
        {
            if(item.Genre.Equals(genre))
                shopItemsGenre.Add(item);
        }
        int index = Random.Range(0,shopItemsGenre.Count);
        return shopItemsGenre[index];
    }

    private string GetRandomItem()
    {
        int index = Random.Range(0,GetAllItemsPath().Length);
        return AssetDatabase.GUIDToAssetPath(GetAllItemsPath()[index]);
    }
    private void ShowElements(SerializedProperty list, bool showItemsLabels = true)
    {
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (showItemsLabels)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
            else
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
            }

            if (GUILayout.Button("Delete", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(false)))
            {
                Object.DestroyImmediate(list.GetArrayElementAtIndex(i).objectReferenceValue, true);
                shop.shopItems.RemoveAt(i);
                AssetDatabase.SaveAssets();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}