using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EditorTool
{
    public class PrefabCreatorCleaner : MonoBehaviour
    {
        [SerializeField] private GameObject walls;
        [SerializeField] private List<GameObject> wallsList;
        [SerializeField] private GameObject objects;
        [SerializeField] private List<GameObject> objectsList;
        [SerializeField] private GameObject floors;
        [SerializeField] private List<GameObject> floorsList;

        public IEnumerator StartClear(GameObject parent)
        {
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
            yield return new WaitForEndOfFrame();

        }

        public void InitRefs()
        {
            foreach (Transform wall in walls.transform)
                if (wall.name != "Objects")
                    wallsList.Add(wall.gameObject);
            foreach (Transform obj in objects.transform)
                if (obj.name != "Floors")
                    objectsList.Add(obj.gameObject);
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

    }
}