using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Shop))]
public class ShopEditor : Editor
{
    private Shop shop;
    private string itemsAssetDatabasePath = "Assets/Scripts/Shop/ShopItems";
    int selected;
    public override void OnInspectorGUI()
    {
        shop = (Shop)target;
        SerializedObject serializedObject = new SerializedObject(shop);
        serializedObject.Update();
        SerializedProperty shopItems = serializedObject.FindProperty("shopItems");
        Draw(shopItems);

    }
    ///<summary>
    /// Draws everything in inspector window 
    ///</summary>
    private void Draw(SerializedProperty list)
    {
        if (list.isExpanded)
        {
            EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            ShowElements(list, false);
        }
        GUILayout.Space(20);

        RandomButton(list);
        ConsumibleButton(list);
        WeaponButton(list);
        ModifierButton(list);
        EmptyButton(list);

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.cyan;
        string[] options = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
        selected = EditorGUILayout.Popup("Cuantity", selected, options);
        if (GUILayout.Button("Generate", GUILayout.Height(20), GUILayout.Width(150)))
        {
            switch (selected + 1)
            {
                case 1:
                    Weapon();
                    break;
                case 2:
                    Weapon();
                    Modifier();
                    break;
                case 3:
                    Weapon();
                    Modifier();
                    Consumible();
                    break;
                case 4:
                    Odd();
                    Weapon();
                    Modifier();
                    Consumible();
                    break;
                case 5:
                    Odd();
                    Weapon();
                    Modifier();
                    Consumible();
                    Consumible();
                    break;
                case 6:
                    Odd();
                    Weapon();
                    Modifier();
                    Consumible();
                    Modifier();
                    break;
                case 7:
                    Odd();
                    Weapon();
                    Weapon();
                    Modifier();
                    Consumible();
                    Modifier();
                    break;
                case 8:
                    Odd();
                    Odd();
                    Weapon();
                    Weapon();
                    Modifier();
                    Consumible();
                    Modifier();
                    break;
                case 9:
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    break;
                case 10:
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    Odd();
                    break;
            }

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear Shop", GUILayout.Height(20), GUILayout.Width(100)))
        {
            for (int i = 0; i < list.arraySize; i++)
                Object.DestroyImmediate(list.GetArrayElementAtIndex(i).objectReferenceValue, true);

            shop.shopItems.Clear();
            shop.shopItems.TrimExcess();
            AssetDatabase.SaveAssets();
        }

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Export package", GUILayout.Height(20), GUILayout.Width(100)))
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.ExportPackage("Assets/Scripts/Shop/Shop.asset", "Assets/MyPackages/shop.unitypackage", ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);
        }
        EditorGUILayout.EndHorizontal();

    }
    #region  BUTTONS
    ///<summary>
    /// Generates button on inspector that adds a random item 
    ///</summary>
    private void RandomButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Random Item", GUILayout.Height(70)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
            Odd();
        }
    }
    ///<summary>
    /// Generates button on inspector that adds a item type consumible
    ///</summary>
    private void ConsumibleButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Random Consumible", GUILayout.Height(30)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
            Consumible();
        }
    }
    ///<summary>
    /// Generates button on inspector that adds a item type weapon
    ///</summary>
    private void WeaponButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Random Weapon", GUILayout.Height(30)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
            Weapon();
        }
    }
    ///<summary>
    /// Generates button on inspector that adds a item type stats modifier
    ///</summary>
    private void ModifierButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Random Modifier", GUILayout.Height(30)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
            Modifier();
        }
    }
    ///<summary>
    /// Generates button on inspector that adds a empty item
    ///</summary>
    private void EmptyButton(SerializedProperty list)
    {
        if (GUILayout.Button("Add Empty Item", GUILayout.Height(20)))
        {
            if (!list.isExpanded)
                list.isExpanded = true;
            Empty();
        }
    }
    #endregion

    #region  BUTTONS_METHODS
    private void Odd()
    {
        string path = GetRandomItem();
        ShopItem auxRandomShopItem = AssetDatabase.LoadAssetAtPath(path, (typeof(ShopItem))) as ShopItem;
        ShopItem randomShopItem = Object.Instantiate(auxRandomShopItem);
        randomShopItem.name = randomShopItem.ItemName;
        shop.shopItems.Add(randomShopItem);
        AssetDatabase.AddObjectToAsset(randomShopItem, shop);
        AssetDatabase.SaveAssets();
    }
    private void Consumible()
    {
        ShopItem auxShopItem = GetRandomItemByGenre(ItemGenre.CONSUMIBLE);
        ShopItem emptyShopItem = Object.Instantiate(auxShopItem);
        emptyShopItem.name = emptyShopItem.ItemName;
        shop.shopItems.Add(emptyShopItem);
        AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
        AssetDatabase.SaveAssets();

    }
    private void Weapon()
    {
        ShopItem auxShopItem = GetRandomItemByGenre(ItemGenre.WEAPON);
        ShopItem emptyShopItem = Object.Instantiate(auxShopItem);
        emptyShopItem.name = emptyShopItem.ItemName;
        shop.shopItems.Add(emptyShopItem);
        AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
        AssetDatabase.SaveAssets();
    }
    private void Modifier()
    {
        ShopItem auxShopItem = GetRandomItemByGenre(ItemGenre.STATS_MODIFIER);
        ShopItem emptyShopItem = Object.Instantiate(auxShopItem);
        emptyShopItem.name = emptyShopItem.ItemName;
        shop.shopItems.Add(emptyShopItem);
        AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
        AssetDatabase.SaveAssets();
    }
    private void Empty()
    {
        ShopItem emptyShopItem = ScriptableObject.CreateInstance<ShopItem>();
        emptyShopItem.name = emptyShopItem.ItemName;
        shop.shopItems.Add(emptyShopItem);
        AssetDatabase.AddObjectToAsset(emptyShopItem, shop);
        AssetDatabase.SaveAssets();
    }

    #endregion

    #region  UTILITY_METHODS
    ///<summary>
    /// Returns all paths of every object item type in an array 
    ///</summary>
    private string[] GetAllItemsPath()
    {
        string[] allItems = AssetDatabase.FindAssets("t:ShopItem", new[] { itemsAssetDatabasePath });
        return allItems;
    }
    ///<summary>
    /// Returns list of all items from folder path 
    ///</summary>
    private List<ShopItem> GetAllItems()
    {
        List<ShopItem> shopItems = new List<ShopItem>();
        foreach (string item in GetAllItemsPath())
        {
            ShopItem aux = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(item), (typeof(ShopItem))) as ShopItem;
            shopItems.Add(aux);
        }

        return shopItems;

    }
    ///<summary>
    /// Returns a random item by genre tag from all items list 
    ///</summary>
    private ShopItem GetRandomItemByGenre(ItemGenre genre)
    {
        List<ShopItem> shopItemsGenre = new List<ShopItem>();

        foreach (ShopItem item in GetAllItems())
        {
            if (item.Genre.Equals(genre))
                shopItemsGenre.Add(item);
        }
        int index = Random.Range(0, shopItemsGenre.Count);
        return shopItemsGenre[index];
    }
    ///<summary>
    /// Returns random item path
    ///</summary>
    private string GetRandomItem()
    {
        int index = Random.Range(0, GetAllItemsPath().Length);
        return AssetDatabase.GUIDToAssetPath(GetAllItemsPath()[index]);
    }
    ///<summary>
    /// Shows shop items list in inspector
    ///</summary>
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
    #endregion
}