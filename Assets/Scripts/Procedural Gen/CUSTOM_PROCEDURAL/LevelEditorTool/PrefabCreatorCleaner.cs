using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EditorTool
{
    public class PrefabCreatorCleaner : MonoBehaviour
    {
        [SerializeField] private GameObject objects;
        [SerializeField] private List<GameObject> objectsList;
        [SerializeField] private GameObject drops;
        [SerializeField] private List<GameObject> dropsList;
        [SerializeField] private GameObject walls;
        [SerializeField] private List<GameObject> wallsList;
        [SerializeField] private GameObject floors;
        [SerializeField] private List<GameObject> floorsList;
        [SerializeField] public Transform playerPos;

        private GameObject parent;

        public IEnumerator StartClear(GameObject parent)
        {
            this.parent = parent;
            yield return StartCoroutine(AsyncClear());
            parent.GetComponent<Room>().enabled = true;
            parent.GetComponent<Room>().SetId = DataController.Instance.GenerateId();
            transform.parent.GetComponent<RoomEditorTool>().SavePrefabFinale(parent);

        }

        private IEnumerator AsyncClear()
        {
            SetupCollisionWalls();
            InitRefs();
            ClearFloors();
            ClearWalls();
            ClearObjects();
            ClearDrops();
            yield return new WaitForEndOfFrame();

        }

        public void InitRefs()
        {
            foreach (Transform obj in objects.transform)
                if (obj.name != "Floors")
                {
                    if (obj.name == "Player_Obj(Clone)")
                        playerPos = obj;

                    objectsList.Add(obj.gameObject);
                }
            foreach (Transform drop in drops.transform)
            {
                if (drop.name != "Walls")
                {
                    dropsList.Add(drop.gameObject);
                }
            }
            foreach (Transform wall in walls.transform)
                if (wall.name != "Objects")
                    wallsList.Add(wall.gameObject);
            foreach (Transform floor in floors.transform)
                if (floor.parent == floors.transform)
                    floorsList.Add(floor.gameObject);

        }

        void SetupCollisionWalls()
        {
            Node[,] grid = GetComponentInParent<GridBase>().grid;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int z = 0; z < grid.GetLength(1); z++)
                {
                    if (grid[x, z].floorObj.GetComponent<LevelObject>().objectId != "floor_empty")
                    {
                        if (x - 1 < 0)
                            grid[x, z].floorObj.GetComponent<Floor>().ActivateColliderLeft();
                        else if (grid[x - 1, z].floorObj.GetComponent<LevelObject>().objectId == "floor_empty")
                            grid[x, z].floorObj.GetComponent<Floor>().ActivateColliderLeft();

                        if (z - 1 < 0)
                            grid[x, z].floorObj.GetComponent<Floor>().ActivateColliderBot();
                        else if (grid[x, z - 1].floorObj.GetComponent<LevelObject>().objectId == "floor_empty")
                            grid[x, z].floorObj.GetComponent<Floor>().ActivateColliderBot();
                    }
                }

            }
        }
        void ClearFloors()
        {
            var aux = floorsList;
            foreach (GameObject floor in floorsList)
            {
                if (floor.GetComponent<LevelObject>() != null)
                {
                    if (floor.GetComponent<LevelObject>().objectId == "floor_empty")
                    {
                        Destroy(aux[aux.IndexOf(floor)]);
                    }
                }

            }
            floorsList = aux;
            foreach (GameObject floor in floorsList)
            {
                Destroy(floor.GetComponent<Floor>());
                Destroy(floor.GetComponent<NodeObject>());
                Destroy(floor.GetComponent<LevelObject>());
            }

        }
        void ClearWalls()
        {
            wallsList.Remove(wallsList[0]);
            foreach (GameObject wall in wallsList)
            {
                Destroy(wall.GetComponent<LevelObject>());
            }
        }
        void ClearObjects()
        {
            var aux = objectsList;
            foreach (GameObject obj in objectsList)
            {
                Destroy(obj.GetComponent<LevelObject>());
                if (obj.GetComponent<Rigidbody>() != null)
                    obj.GetComponent<Rigidbody>().isKinematic = false;
            }

        }
        void ClearDrops()
        {
            if (dropsList.Count > 0)
                parent.GetComponent<Room>().SetHasDrop = true;
            var aux = dropsList;
            foreach (GameObject obj in dropsList)
            {
                Destroy(obj.GetComponent<LevelObject>());
                if (obj.GetComponent<ShopComponent>() != null)
                {
                    parent.GetComponent<Room>().SetHasShop = true;

                    foreach (Transform child in obj.transform)
                    {
                        Destroy(child.GetComponent<ShopSlot>().itemToSell.gameObject); //LIMPIA LOS OBJETOS DE LA TIENDA PORQUE SE INSTANCIARAN EN ESCENA CUANDO SE GENEREN EN EL START
                    }
                }

                Collider[] c = obj.GetComponents<Collider>();
                foreach (Collider col in c)
                {
                    col.enabled = false;
                    if(col.GetComponentsInChildren<Collider>().Length > 0)
                    {
                        foreach(Collider cc in col.GetComponentsInChildren<Collider>())
                        {
                            cc.enabled = false;
                        }
                    }
                }

                obj.transform.localScale = Vector3.zero;
            }
        }

    }
}