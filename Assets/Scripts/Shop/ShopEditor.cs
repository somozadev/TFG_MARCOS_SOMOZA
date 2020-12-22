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
        if (GUILayout.Button("Add Empty Item",GUILayout.Height(70)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
           
            ShopItem emptyShopItem = ScriptableObject.CreateInstance<ShopItem>();
            emptyShopItem.name = emptyShopItem.ItemName;
            shop.shopItems.Add(emptyShopItem);
            AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
            AssetDatabase.SaveAssets();
        }
        if (GUILayout.Button("Add Random Item",GUILayout.Height(70)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
            
            string path = GetRandomItem(); 
            ShopItem auxRandomShopItem = (ShopItem)AssetDatabase.LoadAssetAtPath(path,(typeof(ShopItem))) as ShopItem;
            ShopItem randomShopItem = Object.Instantiate(auxRandomShopItem);
            randomShopItem.name = randomShopItem.ItemName;
            shop.shopItems.Add(randomShopItem);
            AssetDatabase.AddObjectToAsset(randomShopItem, shop);
            AssetDatabase.SaveAssets();
           
        }


    }
    private string[] GetAllItems()
    {
        string[] allItems = AssetDatabase.FindAssets("t:ShopItem",new[] {itemsAssetDatabasePath});
        return allItems;
    }
    private string GetRandomItem()
    {
        int index = Random.Range(0,GetAllItems().Length);
        return AssetDatabase.GUIDToAssetPath(GetAllItems()[index]);
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