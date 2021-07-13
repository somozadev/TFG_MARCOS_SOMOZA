using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
#endif
namespace EditorTool
{
    public class RoomEditorTool : MonoBehaviour
    {
        #region VARIABLES
        //UI
        public Button wallButton;
        public Button floorButton;
        public Button lightingButton;
        public Button objectsButton;
        public Button dropsButton;
        public Button dropsStatModButton;
        public Button dropsAbilityModButton;
        public Button dropsInteractModButton;
        public Button dropsShopConfigButton;
        public Button enemyButton;
        public TMP_InputField inputField;



        public Material highLitedMat;
        public GridBase grid;

        public EditorResources resources;

        public List<GameObject> inSceneGameObjects = new List<GameObject>();
        public List<GameObject> InSceneDropItems = new List<GameObject>();
        public List<GameObject> inSceneWalls = new List<GameObject>();
        public List<GameObject> inSceneEnemies = new List<GameObject>();

        Vector3 mousePos;
        Vector3 worldPos;

        //ENEMY_ITEMS
        bool placeEnemy;
        GameObject enemyToPlace;
        GameObject enemyCloneObj;
        LevelObject enemyObjProperties;
        bool deleteEnemyItem;
        public EnemyStatsEditor stats;

        //DROP_ITEMS
        bool placeDrop;
        GameObject dropToPlace;
        GameObject dropCloneObj;
        LevelObject dropObjProperties;
        bool deleteDropItem;

        //OBJECTS
        bool placeObj;
        GameObject objToPlace;
        GameObject cloneObj;
        LevelObject objProperties;
        bool deleteObj;
        //WALLS
        bool placeWallObj;
        GameObject wallObjToPlace;
        GameObject wallCloneObj;
        LevelObject wallProperties;
        bool deleteWallObj;
        //FLOORS    
        [SerializeField] bool fillFloor;
        bool placeFloorObj;
        GameObject floorObjToPlace;
        GameObject floorCloneObj;
        bool deleteFloor;
        //LIGHTING    
        VolumeProfile lightingObjToPlace;
        public GameObject lightVolumeObj;
        #endregion
        #region START_UPDATE

        private void Awake()
        {
            foreach (Shop s in Methods.LoadShops())
            {
                ShopConfig sc = new ShopConfig();
                sc.id = s.name;
                sc.prefab = s;
                resources.shopConfigs.Add(sc);
            }
        }
        private void Start() => inputField.text = transform.GetChild(0).name;
        private void Update()
        {
            PlaceEnemy();
            DeleteEnemies();

            PlaceObject();
            DeleteObjects();

            PlaceDropItems();
            DeleteDropItems();

            PlaceWall();
            DeleteWalls();

            if (!fillFloor)
                PlaceFloor();
            else
                FillFloor();
            DeleteFloors();
            if (Mouse.current.middleButton.wasPressedThisFrame)
                CloseAll();
        }
        #endregion 
        #region USEFULL_METHODS
        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                mousePos = hit.point;
        }
        public Transform GetCenterNode()
        {
            int x = grid.grid.GetLength(0) / 2;
            int y = grid.grid.GetLength(0) / 2;
            Node current = grid.grid[x, y];
            return current.floorObj.transform;
        }
        void CloseAll()
        {
            placeEnemy = false;
            deleteEnemyItem = false;

            placeDrop = false;
            deleteDropItem = false;

            placeObj = false;
            deleteObj = false;

            placeWallObj = false;
            deleteWallObj = false;

            placeFloorObj = false;
            deleteFloor = false;
            if (enemyCloneObj != null)
            {
                Destroy(enemyCloneObj);
                enemyCloneObj = null;
            }
            if (dropCloneObj != null)
            {
                Destroy(dropCloneObj);
                dropCloneObj = null;
            }
            if (cloneObj != null)
            {
                Destroy(cloneObj);
                cloneObj = null;
            }
            if (wallCloneObj != null)
            {
                Destroy(wallCloneObj);
                wallCloneObj = null;
            }
            if (floorCloneObj != null)
            {
                Destroy(floorCloneObj);
                floorCloneObj = null;
            }
            foreach (Node n in grid.grid)
                n.floorObj.GetComponent<Floor>().UnShadeAll();

        }
        #endregion
        #region PREFAB_MAKER
        public void SavePrefab(GameObject parent)
        {
            StartCoroutine(parent.GetComponent<PrefabCreatorCleaner>().StartClear(parent));

        }
        public void SavePrefabFinale(GameObject parent)
        {

#if UNITY_EDITOR
            // parent.GetComponent<Room>().enabled = true;
            // parent.GetComponent<Room>().SetId = DataController.Instance.GenerateId();
            // CREATING PREFAB
            parent.name = inputField.text;
            string localPath = "Assets/Scenes/Generated_Scenes/" + parent.name + ".prefab";
            PrefabUtility.SaveAsPrefabAssetAndConnect(parent, localPath, InteractionMode.UserAction);
            GetComponent<RoomEditorUIHelper>().AssetsPanelObj.GetComponent<Animator>().SetTrigger("Open");
            PrefabUtility.UnpackPrefabInstance(parent, PrefabUnpackMode.Completely, InteractionMode.UserAction);

            // MAKING PREFAB ADRESSEABLE
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            string labelName = "Stage" + (GetComponent<RoomEditorUIHelper>().GetCurrentToogle());
            settings.AddLabel(labelName, false);


            string extraLabelName = GetComponent<RoomEditorUIHelper>().GetExtraLabel();
            if (extraLabelName != "none")
                settings.AddLabel(extraLabelName, false);


            AddressableAssetGroup g = settings.FindGroup("LevelPrefabs");
            var guid = AssetDatabase.AssetPathToGUID(localPath);
            var entry = settings.CreateOrMoveEntry(guid, g);
            entry.labels.Add(labelName);
            if (extraLabelName != "none")
                entry.labels.Add(extraLabelName);

            entry.address = parent.name;
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            AssetDatabase.SaveAssets();


            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

#endif
        }
        #endregion
        #region OBJECTS 
        public void PassGameObjectToPlace(string objectId)
        {
            foreach (GameObject obj in inSceneGameObjects)
            {
                if (obj.GetComponent<LevelObject>().objectId == "startPos_object")
                    if (objectId == "startPos_object")
                        return;
            }

            if (cloneObj != null)
                Destroy(cloneObj);
            CloseAll();
            placeObj = true;
            cloneObj = null;
            objToPlace = resources.GetObjectResource(objectId).prefab;
        }
        void PlaceObject()
        {
            if (placeObj)
            {
                Node current = grid.NodeFromWorldPos(mousePos);
                #region PLAYER_CONDIT
                //just for player obj.... to limit only 1 can be at a time
                int cont = 0;
                foreach (GameObject obj in inSceneGameObjects)
                {
                    if (obj.GetComponent<LevelObject>().objectId == "startPos_object")
                    {
                        cont++;
                        if (cont > 1)
                        {

                            placeObj = false;
                            if (current.placedObj.Count > 0)
                            {
                                List<GameObject> aux = current.placedObj;
                                foreach (GameObject g in current.placedObj)
                                {
                                    inSceneGameObjects.Remove(g);
                                    Destroy(aux[aux.IndexOf(g)]);
                                }
                                current.floorObj.GetComponent<Floor>().UnShadeFloor();
                            }
                            current.placedObj.Clear();
                            return;
                        }
                    }
                }
                #endregion
                highLitedMat.color = new Color32(255, 255, 0, 16);
                highLitedMat.SetColor("_EmissionColor", Color.yellow);
                UpdateMousePosition();
                if (current == null)
                    return;
                Floor floor = current.floorObj.GetComponent<Floor>();
                worldPos = current.floorObj.transform.position;


                //UNLIGHT N LIGHT CURRENT NODE FLOOR
                foreach (Node n in grid.grid)
                {
                    if (n != current)
                    {
                        Floor f = n.floorObj.GetComponent<Floor>();
                        f.UnShadeAll();
                    }
                    else
                        n.floorObj.GetComponent<Floor>().ShadeFloor();
                }


                if (cloneObj == null)
                {
                    cloneObj = Instantiate(objToPlace, worldPos, Quaternion.identity) as GameObject;
                    objProperties = cloneObj.GetComponent<LevelObject>();
                }
                else
                {
                    if (cloneObj.GetComponent<LevelObject>().posOffset == Vector3.zero)
                        cloneObj.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z);
                    else
                        cloneObj.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z) + cloneObj.GetComponent<LevelObject>().posOffset;

                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        GameObject actualObjPlaced = Instantiate(objToPlace, worldPos, cloneObj.transform.rotation);
                        actualObjPlaced.transform.parent = transform.GetChild(0).transform.GetChild(1).transform;
                        LevelObject placedProp = actualObjPlaced.GetComponent<LevelObject>();
                        placedProp.x = current.x;
                        placedProp.z = current.z;
                        if (placedProp.posOffset == Vector3.zero)
                            placedProp.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z);
                        else
                            placedProp.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z) + placedProp.posOffset;

                        placedProp.transform.rotation = cloneObj.transform.rotation;

                        current.placedObj.Add(actualObjPlaced);
                        inSceneGameObjects.Add(actualObjPlaced);

                    }
                    if (Mouse.current.rightButton.wasPressedThisFrame)
                        objProperties.ChangeRotation();
                }
            }
            else
            {
                if (cloneObj != null)
                    Destroy(cloneObj);

            }

        }
        public void DeleteObject()
        {
            highLitedMat.color = new Color32(255, 0, 0, 16);
            highLitedMat.SetColor("_EmissionColor", Color.red);
            if (cloneObj != null)
                Destroy(cloneObj);
            CloseAll();
            deleteObj = true;
        }
        void DeleteObjects()
        {
            if (deleteObj)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (current == null)
                    return;

                current.floorObj.GetComponent<Floor>().ShadeFloor();

                foreach (Node node in grid.grid)
                {
                    if (node != current)
                        node.floorObj.GetComponent<Floor>().UnShadeFloor();
                }



                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (current.placedObj.Count > 0)
                    {
                        List<GameObject> aux = current.placedObj;
                        foreach (GameObject g in current.placedObj)
                        {
                            inSceneGameObjects.Remove(g);
                            Destroy(aux[aux.IndexOf(g)]);
                        }
                        current.floorObj.GetComponent<Floor>().UnShadeFloor();
                    }
                    current.placedObj.Clear();
                }
            }

        }
        #endregion
        #region WALLS
        public void PassWallToPlace(string objId)
        {
            deleteWallObj = false;
            if (wallCloneObj != null)
                Destroy(wallCloneObj);
            CloseAll();
            wallCloneObj = null;
            placeWallObj = true;
            wallObjToPlace = resources.GetObjectWall(objId).prefab;
        }
        int rotCount = 0;
        void PlaceWall()
        {
            if (placeWallObj)
            {
                highLitedMat.color = new Color32(255, 255, 0, 16);
                highLitedMat.SetColor("_EmissionColor", Color.yellow);
                //UPDATES CURRENT NODE 
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (current == null)
                    return;
                //CLEARS OLD NODE VISUALS
                foreach (Node n in grid.grid)
                {
                    if (n != current)
                    {
                        Floor f = n.floorObj.GetComponent<Floor>();
                        f.UnShadeAll();
                    }
                    else if (rotCount == 0 && n.floorObj.GetComponent<Floor>().topWall == null)
                        n.floorObj.GetComponent<Floor>().ShadeTopWall();
                    else if (rotCount == 2 && n.floorObj.GetComponent<Floor>().leftWall == null)
                        n.floorObj.GetComponent<Floor>().ShadeLeftWall();
                    else if (rotCount == 1 && n.floorObj.GetComponent<Floor>().rightWall == null)
                        n.floorObj.GetComponent<Floor>().ShadeRightWall();
                }
                //CURRENT NODE VARIABLES
                Floor floor = current.floorObj.GetComponent<Floor>();
                worldPos = current.floorObj.transform.position;
                //VISUALIZER INSTANTIATION IF NEEDED
                if (wallCloneObj == null)
                {
                    rotCount = 0;
                    wallCloneObj = Instantiate(wallObjToPlace, worldPos, Quaternion.Euler(0, -180, 0)) as GameObject;
                    wallProperties = wallCloneObj.GetComponent<LevelObject>();

                    floor.ShadeTopWall();
                    wallCloneObj.transform.position = worldPos;
                    wallCloneObj.transform.localScale *= 0.8f;

                }
                else
                {
                    //VISUALIZER POS UPDATER
                    wallCloneObj.transform.position = worldPos;
                    //VISUALIZER ROTATION UPDATER
                    if (Mouse.current.rightButton.wasPressedThisFrame)
                    {
                        Debug.Log(rotCount);
                        if (rotCount == 1)
                            wallProperties.ChangeRotation(180);
                        else
                            wallProperties.ChangeRotation();
                        switch (rotCount)
                        {
                            case 0://prender right
                                floor.UnShadeLeftWall();
                                floor.UnShadeTopWall();
                                if (floor.topWall != null)
                                    floor.UnShadeRightWall();
                                else
                                    floor.ShadeRightWall();
                                break;
                            case 1://prender left
                                floor.UnShadeTopWall();
                                floor.UnShadeRightWall();
                                if (floor.rightWall != null)
                                    floor.UnShadeLeftWall();
                                else
                                    floor.ShadeLeftWall();
                                break;
                            case 2://prender top
                                floor.UnShadeLeftWall();
                                floor.UnShadeRightWall();
                                if (floor.leftWall != null)
                                    floor.UnShadeTopWall();
                                else
                                    floor.ShadeTopWall();
                                break;
                        }



                        rotCount++;

                        if (rotCount > 2)
                            rotCount = 0;
                    }
                    //WALL PLACING UPDATER
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        GameObject actualWallPlaced = null;

                        switch (rotCount)
                        {
                            case 0: //top
                                if (floor.topWall != null && floor.topWall.GetComponent<LevelObject>().objectId == wallCloneObj.GetComponent<LevelObject>().objectId)
                                    return;

                                if (floor.topWall == null || floor.topWall.GetComponent<LevelObject>().objectId != wallCloneObj.GetComponent<LevelObject>().objectId)
                                {
                                    if (floor.topWall != null)
                                        Destroy(floor.topWall);
                                    actualWallPlaced = Instantiate(wallObjToPlace, floor.trfTopWall.position, floor.trfTopWall.rotation);
                                    floor.topWall = actualWallPlaced;
                                    floor.UnShadeTopWall();
                                }
                                else
                                    return;
                                break;
                            case 1: //right
                                if (floor.rightWall != null && floor.rightWall.GetComponent<LevelObject>().objectId == wallCloneObj.GetComponent<LevelObject>().objectId)
                                    return;

                                if (floor.rightWall == null || floor.rightWall.GetComponent<LevelObject>().objectId != wallCloneObj.GetComponent<LevelObject>().objectId)
                                {
                                    if (floor.rightWall != null)
                                        Destroy(floor.rightWall);
                                    actualWallPlaced = Instantiate(wallObjToPlace, floor.trfRightWall.position, floor.trfRightWall.rotation);
                                    floor.rightWall = actualWallPlaced;
                                    floor.UnShadeRightWall();
                                }
                                else
                                    return;
                                break;
                            case 2: //left
                                if (floor.leftWall != null && floor.leftWall.GetComponent<LevelObject>().objectId == wallCloneObj.GetComponent<LevelObject>().objectId)
                                    return;

                                if (floor.leftWall == null || floor.leftWall.GetComponent<LevelObject>().objectId != wallCloneObj.GetComponent<LevelObject>().objectId)
                                {
                                    if (floor.leftWall != null)
                                        Destroy(floor.leftWall);
                                    actualWallPlaced = Instantiate(wallObjToPlace, floor.trfLeftWall.position, floor.trfLeftWall.rotation);
                                    floor.leftWall = actualWallPlaced;
                                    floor.UnShadeLeftWall();
                                }
                                else
                                    return;
                                break;
                        }

                        LevelObject placedProp = actualWallPlaced.GetComponent<LevelObject>();
                        actualWallPlaced.transform.parent = transform.GetChild(0).transform.GetChild(2).transform;

                        placedProp.posOffset = wallProperties.posOffset;
                        placedProp.x = current.x;
                        placedProp.z = current.z;
                        inSceneWalls.Add(actualWallPlaced);
                        // CloseAll();
                    }
                }




            }
        }
        public void DeleteWall()
        {
            highLitedMat.color = new Color32(255, 0, 0, 16);
            highLitedMat.SetColor("_EmissionColor", Color.red);
            GameObject remove = wallCloneObj;
            Destroy(remove);
            CloseAll();
            deleteWallObj = true;
        }
        void DeleteWalls()
        {
            if (deleteWallObj)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (current == null)
                    return;

                if (current.floorObj.GetComponent<Floor>().topWall != null)
                {
                    current.floorObj.GetComponent<Floor>().ShadeTopWall();
                    highLitedMat.color = new Color32(255, 0, 0, 16);
                    highLitedMat.SetColor("_EmissionColor", Color.red);
                }
                if (current.floorObj.GetComponent<Floor>().leftWall != null)
                {
                    current.floorObj.GetComponent<Floor>().ShadeLeftWall();
                    highLitedMat.color = new Color32(255, 0, 0, 16);
                    highLitedMat.SetColor("_EmissionColor", Color.red);
                }
                if (current.floorObj.GetComponent<Floor>().rightWall != null)
                {
                    current.floorObj.GetComponent<Floor>().ShadeRightWall();
                    highLitedMat.color = new Color32(255, 0, 0, 16);
                    highLitedMat.SetColor("_EmissionColor", Color.red);
                }

                foreach (Node node in grid.grid)
                {
                    if (node != current)
                    {
                        node.floorObj.GetComponent<Floor>().UnShadeTopWall();
                        node.floorObj.GetComponent<Floor>().UnShadeRightWall();
                        node.floorObj.GetComponent<Floor>().UnShadeLeftWall();
                    }
                }

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (current.floorObj.GetComponent<Floor>().topWall != null)
                    {
                        inSceneWalls.Remove(current.floorObj.GetComponent<Floor>().topWall);
                        Destroy(current.floorObj.GetComponent<Floor>().topWall);
                        current.floorObj.GetComponent<Floor>().UnShadeTopWall();
                    }
                    if (current.floorObj.GetComponent<Floor>().leftWall != null)
                    {
                        inSceneWalls.Remove(current.floorObj.GetComponent<Floor>().leftWall);
                        Destroy(current.floorObj.GetComponent<Floor>().leftWall);
                        current.floorObj.GetComponent<Floor>().UnShadeLeftWall();
                    }
                    if (current.floorObj.GetComponent<Floor>().rightWall != null)
                    {
                        inSceneWalls.Remove(current.floorObj.GetComponent<Floor>().rightWall);
                        Destroy(current.floorObj.GetComponent<Floor>().rightWall);
                        current.floorObj.GetComponent<Floor>().UnShadeRightWall();
                    }
                }
            }

            // else
            // {
            //     highLitedMat.color = new Color32(255, 255, 0, 16);
            //     highLitedMat.SetColor("_EmissionColor", Color.yellow);
            // }

        }
        #endregion
        #region FLOORS
        public void PassFloorToPlace(string objId)
        {
            // Debug.LogError(objId);
            deleteFloor = false;
            if (floorCloneObj != null)
                Destroy(floorCloneObj);
            CloseAll();
            floorCloneObj = null;
            placeFloorObj = true;
            floorObjToPlace = resources.GetObjectFloor(objId).prefab;

        }
        List<GameObject> floorsFill;
        bool areSeeing = true;
        void FillFloor()
        {
            foreach (Node node in grid.grid)
            {
                node.floorObj.GetComponent<Floor>().ShadeFloor();
            }
            if (areSeeing)
            {
                foreach (Node node in grid.grid)
                {
                    floorCloneObj = Instantiate(floorObjToPlace, worldPos, Quaternion.identity) as GameObject;

                    floorCloneObj.transform.position = node.floorObj.transform.position + new Vector3(0, 0.5f, 0);
                    floorCloneObj.transform.localScale *= 0.8f;
                    floorsFill.Add(floorCloneObj);
                }
                areSeeing = false;
            }

            //PLACE (dont destroy node bc should change so many things)
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                foreach (Node node in grid.grid)
                {
                    if (node.floorObj.GetComponent<Floor>().floor.GetComponent<LevelObject>().objectId != floorCloneObj.GetComponent<LevelObject>().objectId)
                    {
                        node.floorObj.GetComponent<Floor>().floor.GetComponent<LevelObject>().objectId = floorObjToPlace.GetComponent<LevelObject>().objectId;
                        node.floorObj.GetComponent<Floor>().floor.GetComponent<MeshFilter>().mesh = floorObjToPlace.GetComponent<MeshFilter>().sharedMesh;
                        node.floorObj.GetComponent<Floor>().floor.GetComponent<MeshRenderer>().materials = floorObjToPlace.GetComponent<MeshRenderer>().sharedMaterials;
                        node.floorObj.GetComponent<Floor>().UnShadeFloor();
                    }
                }
            }


        }
        private void PlaceFloor()
        {
            if (placeFloorObj)
            {
                highLitedMat.color = new Color32(255, 255, 0, 16);
                highLitedMat.SetColor("_EmissionColor", Color.yellow);
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (current == null)
                    return;
                Floor floor = current.floorObj.GetComponent<Floor>();
                foreach (Node n in grid.grid)
                {
                    if (n != current)
                    {
                        Floor f = n.floorObj.GetComponent<Floor>();
                        f.UnShadeAll();
                    }
                    else if (n.floorObj.GetComponent<Floor>().floor.GetComponent<LevelObject>().objectId != floorObjToPlace.GetComponent<LevelObject>().objectId)
                        n.floorObj.GetComponent<Floor>().ShadeFloor();
                }


                if (floorCloneObj == null)
                {
                    floorCloneObj = Instantiate(floorObjToPlace, worldPos, Quaternion.identity) as GameObject;
                    floor.ShadeFloor();
                    floorCloneObj.transform.position = current.floorObj.transform.position + new Vector3(0, 0.5f, 0);
                    floorCloneObj.transform.localScale *= 0.8f;
                }
                else
                {
                    floorCloneObj.transform.position = current.floorObj.transform.position + new Vector3(0, 0.5f, 0);
                    //PLACE (dont destroy node bc should change so many things)
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        if (floor.floor.GetComponent<LevelObject>().objectId != floorCloneObj.GetComponent<LevelObject>().objectId)
                        {
                            floor.floor.GetComponent<LevelObject>().objectId = floorObjToPlace.GetComponent<LevelObject>().objectId;
                            floor.floor.GetComponent<MeshFilter>().mesh = floorObjToPlace.GetComponent<MeshFilter>().sharedMesh;
                            floor.floor.GetComponent<MeshRenderer>().materials = floorObjToPlace.GetComponent<MeshRenderer>().sharedMaterials;
                            floor.UnShadeFloor();
                        }

                    }
                }
            }
        }
        public void DeleteFloor()
        {
            highLitedMat.color = new Color32(255, 0, 0, 16);
            highLitedMat.SetColor("_EmissionColor", Color.red);
            GameObject remove = floorCloneObj;
            Destroy(remove);
            CloseAll();
            deleteFloor = true;
        }
        private void DeleteFloors()
        {
            if (deleteFloor)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (current == null)
                    return;

                foreach (Node node in grid.grid)
                {
                    if (node != current)
                        node.floorObj.GetComponent<Floor>().UnShadeFloor();
                }
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (current.floorObj.GetComponent<Floor>().floor != null)
                    {
                        // inSceneFloors.Remove(current.floorObj.GetComponent<Floor>().floor);
                        current.floorObj.GetComponent<Floor>().floor.GetComponent<MeshFilter>().mesh = resources.levelFloors[0].prefab.GetComponent<MeshFilter>().sharedMesh;
                        current.floorObj.GetComponent<Floor>().floor.GetComponent<LevelObject>().objectId = resources.levelFloors[0].id;
                        current.floorObj.GetComponent<Floor>().UnShadeFloor();
                    }

                }
            }

        }
        #endregion
        #region LIGHTING_OBJECTS
        public void PassLightingToPlace(string objId)
        {
            CloseAll();
            lightingObjToPlace = resources.GetLightingResource(objId).prefab;
            lightVolumeObj.GetComponent<Volume>().profile = lightingObjToPlace;
        }

        #endregion
        #region DROP_ITEMS
        public void PassDropItemToPlace(string objId)
        {
            deleteDropItem = false;
            if (dropCloneObj != null)
                Destroy(dropCloneObj);
            CloseAll();
            dropCloneObj = null;
            placeDrop = true;
            dropToPlace = resources.GetDropItemResource(objId).prefab;
        }
        public void PassDropItemStatModToPlace(string objId)
        {
            deleteDropItem = false;
            if (dropCloneObj != null)
                Destroy(dropCloneObj);
            CloseAll();
            dropCloneObj = null;
            placeDrop = true;
            dropToPlace = resources.GetDropItemStatModResource(objId).prefab;
        }
        public void PassDropItemAbilityModToPlace(string objId)
        {
            deleteDropItem = false;
            if (dropCloneObj != null)
                Destroy(dropCloneObj);
            CloseAll();
            dropCloneObj = null;
            placeDrop = true;
            dropToPlace = resources.GetDropItemAbilityModResource(objId).prefab;
        }
        public void PassDropItemInteractModToPlace(string objId)
        {
            deleteDropItem = false;
            if (dropCloneObj != null)
                Destroy(dropCloneObj);
            CloseAll();
            dropCloneObj = null;
            placeDrop = true;
            dropToPlace = resources.GetDropItemInteractModResource(objId).prefab;
        }
        public void PassShopConfigToSet(string objId)
        {
            Shop newConfig = resources.GetShopConfigResource(objId).prefab;
            resources.dropItemsInteractMod.First(x => x.id == "Item_Shop").prefab.GetComponent<ShopComponent>().setShop = newConfig;
        }

        public void DeleteDropItem()
        {
            highLitedMat.color = new Color32(255, 0, 0, 16);
            highLitedMat.SetColor("_EmissionColor", Color.red);
            GameObject remove = dropCloneObj;
            Destroy(remove);
            CloseAll();
            deleteDropItem = true;
        }
        public void PlaceDropItems()
        {
            if (placeDrop)
            {
                Node current = grid.NodeFromWorldPos(mousePos);

                highLitedMat.color = new Color32(255, 255, 0, 16);
                highLitedMat.SetColor("_EmissionColor", Color.yellow);
                UpdateMousePosition();
                if (current == null)
                    return;
                Floor floor = current.floorObj.GetComponent<Floor>();
                worldPos = current.floorObj.transform.position;


                //UNLIGHT N LIGHT CURRENT NODE FLOOR
                foreach (Node n in grid.grid)
                {
                    if (n != current)
                    {
                        Floor f = n.floorObj.GetComponent<Floor>();
                        f.UnShadeAll();
                    }
                    else
                        n.floorObj.GetComponent<Floor>().ShadeFloor();
                }


                if (dropCloneObj == null)
                {
                    dropCloneObj = Instantiate(dropToPlace, worldPos, Quaternion.identity) as GameObject;
                    dropObjProperties = dropCloneObj.GetComponent<LevelObject>();
                }
                else
                {
                    if (dropCloneObj.GetComponent<LevelObject>().posOffset == Vector3.zero)
                        dropCloneObj.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z);
                    else
                        dropCloneObj.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z) + dropCloneObj.GetComponent<LevelObject>().posOffset;

                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        GameObject actualObjPlaced = Instantiate(dropToPlace, worldPos, dropCloneObj.transform.rotation);
                        actualObjPlaced.transform.parent = transform.GetChild(0).transform.GetChild(3).transform;
                        LevelObject placedProp = actualObjPlaced.GetComponent<LevelObject>();
                        placedProp.x = current.x;
                        placedProp.z = current.z;
                        if (placedProp.posOffset == Vector3.zero)
                            placedProp.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z);
                        else
                            placedProp.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z) + placedProp.posOffset;

                        placedProp.transform.rotation = dropCloneObj.transform.rotation;

                        current.dropItemsObj.Add(actualObjPlaced);
                        InSceneDropItems.Add(actualObjPlaced);

                    }
                    if (Mouse.current.rightButton.wasPressedThisFrame)
                        dropObjProperties.ChangeRotation();
                }
            }
            else
            {
                if (dropCloneObj != null)
                    Destroy(dropCloneObj);

            }



        }
        void DeleteDropItems()
        {
            if (deleteDropItem)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (current == null)
                    return;

                current.floorObj.GetComponent<Floor>().ShadeFloor();

                foreach (Node node in grid.grid)
                {
                    if (node != current)
                        node.floorObj.GetComponent<Floor>().UnShadeFloor();
                }



                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (current.dropItemsObj.Count > 0)
                    {
                        List<GameObject> aux = current.dropItemsObj;
                        foreach (GameObject g in current.dropItemsObj)
                        {
                            InSceneDropItems.Remove(g);
                            Destroy(aux[aux.IndexOf(g)]);
                        }
                        current.floorObj.GetComponent<Floor>().UnShadeFloor();
                    }
                    current.dropItemsObj.Clear();
                }
            }


        }
        #endregion
        #region ENEMIES
        public void PassEnemyToPlace(string enemyId)
        {
            Debug.Log(enemyId);
            // deleteEnemyItem = false;
            if (enemyCloneObj != null)
                Destroy(enemyCloneObj);
            CloseAll();
            enemyCloneObj = null;
            placeEnemy = true;
            enemyToPlace = resources.GetEnemyResource(enemyId).prefab;
        }
        void PlaceEnemy()
        {
            if (placeEnemy)
            {
                Node current = grid.NodeFromWorldPos(mousePos);
                highLitedMat.color = new Color32(255, 255, 0, 16);
                highLitedMat.SetColor("_EmissionColor", Color.yellow);
                UpdateMousePosition();
                if (current == null)
                    return;
                Floor floor = current.floorObj.GetComponent<Floor>();
                worldPos = current.floorObj.transform.position;


                //UNLIGHT N LIGHT CURRENT NODE FLOOR
                foreach (Node n in grid.grid)
                {
                    if (n != current)
                    {
                        Floor f = n.floorObj.GetComponent<Floor>();
                        f.UnShadeAll();
                    }
                    else
                        n.floorObj.GetComponent<Floor>().ShadeFloor();
                }


                if (enemyCloneObj == null)
                {
                    enemyCloneObj = Instantiate(enemyToPlace, worldPos, Quaternion.identity) as GameObject;
                    objProperties = enemyCloneObj.GetComponent<LevelObject>();
                }
                else
                {
                    if (enemyCloneObj.GetComponent<LevelObject>().posOffset == Vector3.zero)
                        enemyCloneObj.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z);
                    else
                        enemyCloneObj.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z) + enemyCloneObj.GetComponent<LevelObject>().posOffset;

                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        GameObject actualObjPlaced = Instantiate(enemyToPlace, worldPos, enemyCloneObj.transform.rotation);
                        actualObjPlaced.transform.parent = transform.GetChild(0).transform.GetChild(4).transform;
                        LevelObject placedProp = actualObjPlaced.GetComponent<LevelObject>();
                        placedProp.x = current.x;
                        placedProp.z = current.z;
                        if (placedProp.posOffset == Vector3.zero)
                            placedProp.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z);
                        else
                            placedProp.transform.position = new Vector3(mousePos.x, worldPos.y, mousePos.z) + placedProp.posOffset;

                        placedProp.transform.rotation = enemyCloneObj.transform.rotation;

                        current.placedEnemies.Add(actualObjPlaced);
                        inSceneEnemies.Add(actualObjPlaced);

                    }
                    if (Mouse.current.rightButton.wasPressedThisFrame)
                        objProperties.ChangeRotation();
                }
            }
            else
            {
                if (enemyCloneObj != null)
                    Destroy(enemyCloneObj);

            }
        }
        void DeleteEnemies()
        {
            if (deleteEnemyItem)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (current == null)
                    return;

                current.floorObj.GetComponent<Floor>().ShadeFloor();

                foreach (Node node in grid.grid)
                {
                    if (node != current)
                        node.floorObj.GetComponent<Floor>().UnShadeFloor();
                }



                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (current.placedEnemies.Count > 0)
                    {
                        List<GameObject> aux = current.placedEnemies;
                        foreach (GameObject g in current.placedEnemies)
                        {
                            inSceneEnemies.Remove(g);
                            Destroy(aux[aux.IndexOf(g)]);
                        }
                        current.floorObj.GetComponent<Floor>().UnShadeFloor();
                    }
                    current.placedEnemies.Clear();
                }
            }

        }
        public void DeleteEnemy()
        {
            highLitedMat.color = new Color32(255, 0, 0, 16);
            highLitedMat.SetColor("_EmissionColor", Color.red);
            GameObject remove = enemyCloneObj;
            Destroy(remove);
            CloseAll();
            deleteEnemyItem = true;
        }

        #endregion


        #region  UI

        int wallRotativeCounter = 0;
        public void RotateWallsResources(bool right)
        {
            if (right)
            {
                wallRotativeCounter++;
                if (wallRotativeCounter >= resources.levelWalls.Count)
                    wallRotativeCounter = 0;
            }
            else
            {
                wallRotativeCounter--;
                if (wallRotativeCounter < 0)
                    wallRotativeCounter = resources.levelWalls.Count - 1;
            }

            // wallButton.GetComponentInChildren<TMP_Text>().text = resources.levelWalls[wallRotativeCounter].id;
            wallButton.GetComponent<Image>().sprite = resources.levelWalls[wallRotativeCounter].sprite;
            wallButton.onClick.RemoveAllListeners();
            wallButton.onClick.AddListener(() => PassWallToPlace(resources.levelWalls[wallRotativeCounter].id));
        }
        int floorRotativeCounter = 0;
        public void RotateFloorsResources(bool right)
        {
            if (right)
            {
                floorRotativeCounter++;
                if (floorRotativeCounter >= resources.levelFloors.Count)
                    floorRotativeCounter = 0;
            }
            else
            {
                floorRotativeCounter--;
                if (floorRotativeCounter < 0)
                    floorRotativeCounter = resources.levelFloors.Count - 1;
            }

            // floorButton.GetComponentInChildren<TMP_Text>().text = resources.levelFloors[floorRotativeCounter].id;
            floorButton.GetComponent<Image>().sprite = resources.levelFloors[floorRotativeCounter].sprite;
            floorButton.onClick.RemoveAllListeners();
            floorButton.onClick.AddListener(() => PassFloorToPlace(resources.levelFloors[floorRotativeCounter].id));
        }
        int lightingRotativeCounter = 0;
        public void RotateLightingResources(bool right)
        {
            if (right)
            {
                lightingRotativeCounter++;
                if (lightingRotativeCounter >= resources.levelLights.Count)
                    lightingRotativeCounter = 0;
            }
            else
            {
                lightingRotativeCounter--;
                if (lightingRotativeCounter < 0)
                    lightingRotativeCounter = resources.levelLights.Count - 1;
            }

            // lightingButton.GetComponentInChildren<TMP_Text>().text = resources.levelLights[lightingRotativeCounter].id;

            if (resources.levelLights[lightingRotativeCounter].prefab.TryGet<ShadowsMidtonesHighlights>(out var shadows))
                lightingButton.GetComponent<Image>().color = new Vector4(shadows.shadows.value.x, shadows.shadows.value.y, shadows.shadows.value.z, 255);
            lightingButton.onClick.RemoveAllListeners();
            lightingButton.onClick.AddListener(() => PassLightingToPlace(resources.levelLights[lightingRotativeCounter].id));

        }
        int objectsRotativeCounter = 0;
        public void RotateObjectsResources(bool right)
        {
            if (right)
            {
                objectsRotativeCounter++;
                if (objectsRotativeCounter >= resources.levelObjects.Count)
                    objectsRotativeCounter = 0;
            }
            else
            {
                objectsRotativeCounter--;
                if (objectsRotativeCounter < 0)
                    objectsRotativeCounter = resources.levelObjects.Count - 1;
            }
            objectsButton.GetComponent<Image>().sprite = resources.levelObjects[objectsRotativeCounter].sprite;
            objectsButton.onClick.RemoveAllListeners();
            objectsButton.onClick.AddListener(() => PassGameObjectToPlace(resources.levelObjects[objectsRotativeCounter].id));

        }
        int dropsRotativeCounter = 0;
        public void RotateDropsResources(bool right)
        {
            if (right)
            {
                dropsRotativeCounter++;
                if (dropsRotativeCounter >= resources.dropItems.Count)
                    dropsRotativeCounter = 0;
            }
            else
            {
                dropsRotativeCounter--;
                if (dropsRotativeCounter < 0)
                    dropsRotativeCounter = resources.dropItems.Count - 1;
            }
            dropsButton.GetComponent<Image>().sprite = resources.dropItems[dropsRotativeCounter].sprite;
            dropsButton.onClick.RemoveAllListeners();
            Debug.Log("NEW:" + resources.dropItems[dropsRotativeCounter].id);
            dropsButton.onClick.AddListener(() => PassDropItemToPlace(resources.dropItems[dropsRotativeCounter].id));

        }
        int dropsStatModRotativeCounter = 0;
        public void RotateDropsStatModResources(bool right)
        {
            if (right)
            {
                dropsStatModRotativeCounter++;
                if (dropsStatModRotativeCounter >= resources.dropItemsStatMod.Count)
                    dropsStatModRotativeCounter = 0;
            }
            else
            {
                dropsStatModRotativeCounter--;
                if (dropsStatModRotativeCounter < 0)
                    dropsStatModRotativeCounter = resources.dropItemsStatMod.Count - 1;
            }
            dropsStatModButton.GetComponent<Image>().sprite = resources.dropItemsStatMod[dropsStatModRotativeCounter].sprite;
            dropsStatModButton.onClick.RemoveAllListeners();
            dropsStatModButton.onClick.AddListener(() => PassDropItemStatModToPlace(resources.dropItemsStatMod[dropsStatModRotativeCounter].id));

        }
        int dropsAbilityModRotativeCounter = 0;
        public void RotateDropsAbilityModResources(bool right)
        {
            if (right)
            {
                dropsAbilityModRotativeCounter++;
                if (dropsAbilityModRotativeCounter >= resources.dropItemsAbilityMod.Count)
                    dropsAbilityModRotativeCounter = 0;
            }
            else
            {
                dropsAbilityModRotativeCounter--;
                if (dropsAbilityModRotativeCounter < 0)
                    dropsAbilityModRotativeCounter = resources.dropItemsAbilityMod.Count - 1;
            }
            dropsAbilityModButton.GetComponent<Image>().sprite = resources.dropItemsAbilityMod[dropsAbilityModRotativeCounter].sprite;
            dropsAbilityModButton.onClick.RemoveAllListeners();
            dropsAbilityModButton.onClick.AddListener(() => PassDropItemAbilityModToPlace(resources.dropItemsAbilityMod[dropsAbilityModRotativeCounter].id));

        }
        int dropsInteractModRotativeCounter = 0;
        public void RotateDropsInteractModResources(bool right)
        {
            if (right)
            {
                dropsInteractModRotativeCounter++;
                if (dropsInteractModRotativeCounter >= resources.dropItemsInteractMod.Count)
                    dropsInteractModRotativeCounter = 0;
            }
            else
            {
                dropsInteractModRotativeCounter--;
                if (dropsInteractModRotativeCounter < 0)
                    dropsInteractModRotativeCounter = resources.dropItemsInteractMod.Count - 1;
            }
            dropsInteractModButton.GetComponent<Image>().sprite = resources.dropItemsInteractMod[dropsInteractModRotativeCounter].sprite;
            dropsInteractModButton.onClick.RemoveAllListeners();
            dropsInteractModButton.onClick.AddListener(() => PassDropItemInteractModToPlace(resources.dropItemsInteractMod[dropsInteractModRotativeCounter].id));

        }
        int shopConfigRotativeCounter = 0;
        public void RotateShopConfigResources(bool right)
        {
            if (right)
            {
                shopConfigRotativeCounter++;
                if (shopConfigRotativeCounter >= resources.shopConfigs.Count)
                    shopConfigRotativeCounter = 0;
            }
            else
            {
                shopConfigRotativeCounter--;
                if (shopConfigRotativeCounter < 0)
                    shopConfigRotativeCounter = resources.shopConfigs.Count - 1;
            }
            dropsShopConfigButton.GetComponentInChildren<TMP_Text>().text = resources.shopConfigs[shopConfigRotativeCounter].id;
            dropsShopConfigButton.onClick.RemoveAllListeners();
            dropsShopConfigButton.onClick.AddListener(() => PassShopConfigToSet(resources.shopConfigs[shopConfigRotativeCounter].id));

        }
        int enemiesRotativeCounter = 0;
        public void RotateEnemiesResources(bool right)
        {
            if (right)
            {
                enemiesRotativeCounter++;
                if (enemiesRotativeCounter >= resources.enemies.Count)
                    enemiesRotativeCounter = 0;
            }
            else
            {
                enemiesRotativeCounter--;
                if (enemiesRotativeCounter < 0)
                    enemiesRotativeCounter = resources.enemies.Count - 1;
            }
            enemyButton.GetComponent<Image>().sprite = resources.enemies[enemiesRotativeCounter].sprite;
            enemyButton.onClick.RemoveAllListeners();
            stats.InitValues(resources.enemies[enemiesRotativeCounter].prefab.GetComponentInChildren<Enemy>());
            enemyButton.onClick.AddListener(() => PassEnemyToPlace(resources.enemies[enemiesRotativeCounter].id));
        }

        #endregion

        public void ChangeColor(TMP_Text text) => text.color = new Color32(0, 180, 0, 255);
        public void ResetColor(TMP_Text text) => text.color = Color.black;
        public void ApplyButtonStats() { stats.SetAll(resources.enemies[enemiesRotativeCounter].prefab); }

    }


    #region ORGANIZATIVE_CLASSES

    [System.Serializable]
    public class Node
    {
        public int x;
        public int z;
        public GameObject floorObj;
        public List<GameObject> placedObj = new List<GameObject>();
        public List<GameObject> dropItemsObj = new List<GameObject>();
        public List<GameObject> placedEnemies = new List<GameObject>();

    }

    [System.Serializable]
    public class EditorResources
    {
        public List<DropItemResource> dropItems = new List<DropItemResource>();
        public List<DropItemResource> dropItemsStatMod = new List<DropItemResource>();
        public List<DropItemResource> dropItemsAbilityMod = new List<DropItemResource>();
        public List<DropItemResource> dropItemsInteractMod = new List<DropItemResource>();
        public List<ShopConfig> shopConfigs = new List<ShopConfig>();
        public List<ObjectResource> levelObjects = new List<ObjectResource>();
        public List<ObjectWall> levelWalls = new List<ObjectWall>();
        public List<ObjectFloor> levelFloors = new List<ObjectFloor>();
        public List<LightingResource> levelLights = new List<LightingResource>();
        public List<EnemyResource> enemies = new List<EnemyResource>();

        public DropItemResource GetDropItemResource(string objectId) { return dropItems.First(x => x.id == objectId); }
        public DropItemResource GetDropItemStatModResource(string objectId) { return dropItemsStatMod.First(x => x.id == objectId); }
        public DropItemResource GetDropItemAbilityModResource(string objectId) { return dropItemsAbilityMod.First(x => x.id == objectId); }
        public DropItemResource GetDropItemInteractModResource(string objectId) { return dropItemsInteractMod.First(x => x.id == objectId); }
        public ShopConfig GetShopConfigResource(string objectId) { return shopConfigs.First(x => x.id == objectId); }
        public ObjectResource GetObjectResource(string objectId) { return levelObjects.First(x => x.id == objectId); }
        public ObjectWall GetObjectWall(string objectId) { return levelWalls.First(x => x.id == objectId); }
        public ObjectFloor GetObjectFloor(string objectId) { return levelFloors.First(x => x.id == objectId); }
        public LightingResource GetLightingResource(string lightingId) { return levelLights.First(x => x.id == lightingId); }
        public EnemyResource GetEnemyResource(string enemyId) { return enemies.First(x => x.id == enemyId); }
    }

    [System.Serializable]
    public class DropItemResource
    {
        public string id;
        public GameObject prefab;
        public Sprite sprite;
    }
    [System.Serializable]
    public class ShopConfig
    {
        public string id;
        public Shop prefab;
    }
    [System.Serializable]
    public class ObjectResource
    {
        public string id;
        public GameObject prefab;
        public Sprite sprite;
    }
    [System.Serializable]
    public class LightingResource
    {
        public string id;
        public VolumeProfile prefab;
    }
    [System.Serializable]
    public class ObjectWall
    {
        public string id;
        public GameObject prefab;
        public Sprite sprite;
    }
    [System.Serializable]
    public class ObjectFloor
    {
        public string id;
        public GameObject prefab;
        public Sprite sprite;
    }
    [System.Serializable]
    public class EnemyResource
    {
        public string id;
        public GameObject prefab;
        public Sprite sprite;
    }
    [System.Serializable]
    public class EnemyStatsEditor
    {
        public TMP_InputField hp;
        public TMP_InputField def;
        public TMP_InputField dmg;
        public TMP_InputField spd;
        public TMP_InputField det_range;
        public TMP_InputField att_range;
        public TMP_InputField att_rate;
        public TMP_InputField shoot_range;

        public float GetHp() { return float.Parse(hp.text); }
        public float GetDef() { return float.Parse(def.text); }
        public float GetDmg() { return float.Parse(dmg.text); }
        public float GetSpd() { return float.Parse(spd.text); }
        public float GetDetRange() { return float.Parse(det_range.text); }
        public float GetAttRange() { return float.Parse(att_range.text); }
        public float GetShootRange() { return float.Parse(shoot_range.text); }
        public float GetAttRate() { return float.Parse(att_rate.text); }

        public void SetHp(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.Hp = (int)GetHp(); enemy.GetComponentInChildren<Enemy>().stats.CurrentHp = (int)GetHp(); }
        public void SetDef(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.Def = GetDef(); }
        public void SetDmg(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.Dmg = GetDmg(); }
        public void SetSpd(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.Spd = GetSpd(); }
        public void SetDetRange(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.PursuitRange = GetDetRange(); }
        public void SetAttRange(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.AttackRange = GetAttRange(); }
        public void SetShootRange(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.ShootingRange = GetShootRange(); }
        public void SetAttRate(GameObject enemy) { enemy.GetComponentInChildren<Enemy>().stats.Attrate = GetAttRate(); }

        public void SetAll(GameObject enemy)
        {
            SetHp(enemy);
            SetDef(enemy);
            SetDmg(enemy);
            SetSpd(enemy);
            SetDetRange(enemy);
            SetAttRange(enemy);
            SetAttRate(enemy);
            SetShootRange(enemy);
        }
        public void InitValues(Enemy enemy)
        {
            hp.text = enemy.stats.Hp.ToString();
            def.text = enemy.stats.Def.ToString();
            dmg.text = enemy.stats.Dmg.ToString();
            spd.text = enemy.stats.Spd.ToString();
            det_range.text = enemy.stats.PursuitRange.ToString();
            att_range.text = enemy.stats.AttackRange.ToString();
            att_rate.text = enemy.stats.Attrate.ToString();
            shoot_range.text = enemy.stats.ShootingRange.ToString();

            hp.GetComponentInChildren<TMP_Text>().color = Color.black;
            def.GetComponentInChildren<TMP_Text>().color = Color.black;
            dmg.GetComponentInChildren<TMP_Text>().color = Color.black;
            spd.GetComponentInChildren<TMP_Text>().color = Color.black;
            det_range.GetComponentInChildren<TMP_Text>().color = Color.black;
            att_range.GetComponentInChildren<TMP_Text>().color = Color.black;
            att_rate.GetComponentInChildren<TMP_Text>().color = Color.black;
            shoot_range.GetComponentInChildren<TMP_Text>().color = Color.black;
        }


    }


    #endregion
}