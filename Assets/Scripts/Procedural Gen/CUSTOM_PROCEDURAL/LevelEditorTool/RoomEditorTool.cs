using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

namespace EditorTool
{
    public class RoomEditorTool : MonoBehaviour
    {
        public EditorResources resources;

        public GridBase grid;

        public List<GameObject> inSceneGameObjects = new List<GameObject>();
        public List<GameObject> inSceneWalls = new List<GameObject>();
        public List<GameObject> inSceneStackObjects = new List<GameObject>();


        bool hasObj;
        GameObject objToPlace;
        GameObject cloneObj;
        LevelObject objProperties;
        Vector3 mousePos;
        Vector3 worldPos;
        bool deleteObj;

        Node previousNode;
        Quaternion targetRot;
        Quaternion prevRot;

        bool placeStackObj;
        GameObject stackObjToPlace;
        GameObject stackCloneObj;
        LevelObject stackObjProperties;
        bool deleteStackObj;

        bool createWall;
        public GameObject wallPrefab;
        Node startNodeWall;
        Node endNodeWall;
        bool deleteWall;

        private void Update()
        {
            PlaceObject();
            // DeleteObject();
            // PlaceStackedObject();
            // CreateWall();
            // DeleteStackedObject();
            // DeleteWallObject();
        }

        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit; 
            if(Physics.Raycast(ray, out hit,Mathf.Infinity))
                mousePos = hit.point;
        }

        public void PassGameObjectToPlace(string objectId)
        {
            if(cloneObj!=null)
                Destroy(cloneObj);
            //CloseAll();
            hasObj = true; 
            cloneObj = null; 
            objToPlace = resources.GetObjectResource(objectId).prefab;
        }
        void PlaceObject()
        {

        }

    }

    public class Node
    {
        public int x;
        public int z;
        public GameObject visualizedObj;
        public GameObject placedObj;
        public GameObject wallObj;
        public GameObject stackedObj;
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
    }
    [System.Serializable]
    public class StackedObjectResource
    {
        public string id;
        public GameObject prefab;
    }
    [System.Serializable]
    public class ObjectWall
    {
        public string id;
        public GameObject prefab;
    }
    [System.Serializable]
    public class ObjectFloor
    {
        public string id;
        public GameObject prefab;
    }
}