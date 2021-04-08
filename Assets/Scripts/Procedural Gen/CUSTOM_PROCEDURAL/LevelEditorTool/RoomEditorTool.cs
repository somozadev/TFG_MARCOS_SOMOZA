using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem;

namespace EditorTool
{
    public class RoomEditorTool : MonoBehaviour
    {

        //UI
        public Button wallButton;
        public Button floorButton;


        public EditorResources resources;

        public Material highLitedMat;

        public GridBase grid;

        public List<GameObject> inSceneGameObjects = new List<GameObject>();
        public List<GameObject> inSceneWalls = new List<GameObject>();
        public List<GameObject> inSceneStackObjects = new List<GameObject>();

        Vector3 mousePos;
        Vector3 worldPos;


        //OBJECTS
        bool hasObj;
        GameObject objToPlace;
        GameObject cloneObj;
        LevelObject objProperties;
        bool deleteObj;
        //STACK OBJECTS
        bool placeStackObj;
        GameObject stackObjToPlace;
        GameObject stackCloneObj;
        LevelObject stackObjProperties;
        bool deleteStackObj;
        //WALLS
        bool placeWallObj;
        GameObject wallObjToPlace;
        GameObject wallCloneObj;
        LevelObject wallProperties;
        bool deleteWallObj;
        //FLOORS    
        bool placeFloorObj;
        GameObject floorObjToPlace;
        GameObject floorCloneObj;
        bool deleteFloor;

        private void Update()
        {
            PlaceObject();
            DeleteObjects();
            // PlaceStackedObject();
            // DeleteStackedObject();
            PlaceWall();
            DeleteWalls();
            PlaceFloor();
            DeleteFloors();
            if (Mouse.current.middleButton.wasPressedThisFrame)
                CloseAll();
        }

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
            Node current = grid.grid[x,y];
            return current.floorObj.transform;
        }

        #region OBJECTS 
        public void PassGameObjectToPlace(string objectId)
        {
            if (cloneObj != null)
                Destroy(cloneObj);
            CloseAll();
            hasObj = true;
            cloneObj = null;
            objToPlace = resources.GetObjectResource(objectId).prefab;
        }
        void PlaceObject()
        {
            if (hasObj)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                worldPos = current.floorObj.transform.position;
                if (cloneObj == null)
                {
                    cloneObj = Instantiate(objToPlace, worldPos, Quaternion.identity) as GameObject;
                    objProperties = cloneObj.GetComponent<LevelObject>();
                }
                else
                {
                    cloneObj.transform.position = worldPos + objProperties.posOffset;
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        if (current.placedObj != null)
                        {
                            Destroy(current.placedObj.gameObject);
                            current.placedObj = null;
                        }
                        GameObject actualObjPlaced = Instantiate(objToPlace, worldPos, cloneObj.transform.rotation);
                        LevelObject placedProp = actualObjPlaced.GetComponent<LevelObject>();
                        placedProp.x = current.x;
                        placedProp.z = current.z;
                        placedProp.transform.position = worldPos + placedProp.posOffset;
                        placedProp.transform.rotation = cloneObj.transform.rotation;
                        current.placedObj = placedProp.gameObject;

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
            GameObject remove = inSceneGameObjects.Last<GameObject>();
            inSceneGameObjects.Remove(remove);
            Destroy(remove);
            CloseAll();
            deleteObj = true;
        }
        void DeleteObjects()
        {
            if (deleteObj)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (current.placedObj != null)
                    {
                        Destroy(current.placedObj.gameObject);
                        inSceneGameObjects.Remove(current.placedObj);
                    }
                    current.placedObj = null;
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
                //UPDATES CURRENT NODE 
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
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
                LineRenderer lr = null;
                //VISUALIZER INSTANTIATION IF NEEDED
                if (wallCloneObj == null)
                {
                    rotCount = 0;
                    wallCloneObj = Instantiate(wallObjToPlace, worldPos, Quaternion.Euler(0, -180, 0)) as GameObject;

                    GameObject line = new GameObject("Line");
                    lr = line.AddComponent<LineRenderer>();
                    line.transform.SetParent(wallCloneObj.transform);
                    lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                    lr.startColor = Color.green;
                    lr.endColor = Color.blue;
                    lr.SetWidth(0.5f, 0.5f);
                    lr.SetPosition(0, line.transform.position);
                    lr.SetPosition(1, wallCloneObj.transform.forward);
                    lr.useWorldSpace = false;

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
                        if (lr != null)
                            lr.transform.rotation = wallCloneObj.transform.rotation;



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

            else
            {
                highLitedMat.color = new Color32(255, 255, 0, 16);
                highLitedMat.SetColor("_EmissionColor", Color.yellow);
            }

        }
        #endregion
        #region FLOORS
        public void PassFloorToPlace(string objId)
        {
            deleteFloor = false;
            if (floorCloneObj != null)
                Destroy(floorCloneObj);
            CloseAll();
            floorCloneObj = null;
            placeFloorObj = true;
            floorObjToPlace = resources.GetObjectFloor(objId).prefab;

        }
        private void PlaceFloor()
        {
            if (placeFloorObj)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
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
                            floor.UnShadeFloor();
                        }

                    }
                }
            }
        }
        public void DeleteFloor()
        {
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

                if (current.floorObj.GetComponent<Floor>().floor != null)
                {
                    current.floorObj.GetComponent<Floor>().ShadeFloor();
                    highLitedMat.color = new Color32(255, 0, 0, 16);
                    highLitedMat.SetColor("_EmissionColor", Color.red);
                }
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
        #region  STACK_OBJECTS

        public void PassStackObjectToPlace(string objId)
        {
            if (stackCloneObj != null)
                Destroy(stackCloneObj);
            CloseAll();
            stackCloneObj = null;
            placeStackObj = true;
            stackObjToPlace = resources.GetStackedObjectResource(objId).prefab;
        }

        void PlaceStackedObject()
        {
            if (placeStackObj)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                worldPos = current.floorObj.transform.position;
                if (stackCloneObj == null)
                {
                    stackCloneObj = Instantiate(stackObjToPlace, worldPos, Quaternion.identity) as GameObject;
                    stackObjProperties = cloneObj.GetComponent<LevelObject>();
                }
                else
                {
                    stackCloneObj.transform.position = worldPos;
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        GameObject actualObjPlaced = Instantiate(stackObjToPlace, worldPos, stackCloneObj.transform.rotation);
                        LevelObject placedProp = actualObjPlaced.GetComponent<LevelObject>();
                        placedProp.x = current.x;
                        placedProp.z = current.z;
                        current.stackedObj.Add(placedProp.gameObject);
                    }
                    if (Mouse.current.rightButton.wasPressedThisFrame)
                        objProperties.ChangeRotation();
                }
            }
            else
            {
                if (stackCloneObj != null)
                    Destroy(stackCloneObj);
            }

        }
        public void DeleteStackedObject()
        {
            CloseAll();
            deleteStackObj = true;
        }
        void DeleteStackedObjects()
        {
            if (deleteStackObj)
            {
                UpdateMousePosition();
                Node current = grid.NodeFromWorldPos(mousePos);
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    if (current.stackedObj.Count > 0)
                    {
                        foreach (GameObject stackedO in current.stackedObj)
                        {
                            current.stackedObj.Remove(stackedO);
                            Destroy(stackedO);
                        }
                    }
                    current.stackedObj.Clear();
                }
            }
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

            wallButton.GetComponentInChildren<TMP_Text>().text = resources.levelWalls[wallRotativeCounter].id;
            wallButton.GetComponent<Image>().sprite = resources.levelWalls[wallRotativeCounter].sprite;
            //Change visual Image instead of cutre txt... save cool image in resources file as well

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

            floorButton.GetComponentInChildren<TMP_Text>().text = resources.levelFloors[floorRotativeCounter].id;
            floorButton.GetComponent<Image>().sprite = resources.levelFloors[floorRotativeCounter].sprite;
            //Change visual Image instead of cutre txt... save cool image in resources file as well

            floorButton.onClick.RemoveAllListeners();
            floorButton.onClick.AddListener(() => PassFloorToPlace(resources.levelFloors[floorRotativeCounter].id));
        }



        #endregion

        void CloseAll()
        {
            hasObj = false;
            deleteObj = false;
            deleteStackObj = false;

            placeWallObj = false;
            deleteWallObj = false;

            placeFloorObj = false;
            deleteFloor = false;

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
            if (stackCloneObj != null)
            {
                Destroy(stackCloneObj);
                stackCloneObj = null;
            }
            // deleteWall = false;
            foreach (Node n in grid.grid)
                n.floorObj.GetComponent<Floor>().UnShadeAll();

        }

    }


    #region ORGANIZATIVE_CLASSES

    [System.Serializable]
    public class Node
    {
        public int x;
        public int z;
        public GameObject floorObj;
        public GameObject placedObj;
        public List<GameObject> stackedObj;



        public List<GameObject> walls = new List<GameObject>();



        public Material defaultMat;

        public void Select(Material selected)
        {
            Debug.Log("CURRENT");
            defaultMat = floorObj.GetComponent<Renderer>().material;
            floorObj.GetComponent<Renderer>().material = selected;
        }
        public void DeSelect()
        {
            if (this.floorObj != null)
                this.floorObj.GetComponent<Renderer>().material = defaultMat;
        }


    }

    [System.Serializable]
    public class EditorResources
    {
        public List<ObjectResource> levelObjects = new List<ObjectResource>();
        public List<StackedObjectResource> levelStackingObjects = new List<StackedObjectResource>();

        public List<ObjectWall> levelWalls = new List<ObjectWall>();
        public List<ObjectFloor> levelFloors = new List<ObjectFloor>();


        public ObjectResource GetObjectResource(string objectId) { return levelObjects.First(x => x.id == objectId); }
        public StackedObjectResource GetStackedObjectResource(string objectId) { return levelStackingObjects.First(x => x.id == objectId); }
        public ObjectWall GetObjectWall(string objectId) { return levelWalls.First(x => x.id == objectId); }
        public ObjectFloor GetObjectFloor(string objectId) { return levelFloors.First(x => x.id == objectId); }

    }

    [System.Serializable]
    public class ObjectResource
    {
        public string id;
        public GameObject prefab;
        public Sprite sprite;
    }
    [System.Serializable]
    public class StackedObjectResource
    {
        public string id;
        public GameObject prefab;
        public Sprite sprite;
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
    #endregion
}