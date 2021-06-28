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
        [SerializeField] public Transform enemies;
        [SerializeField] private List<GameObject> enemiesList;
        [SerializeField] public Transform playerPos;
        public GameObject navMesherCollider;
        private GameObject parent;

        public IEnumerator StartClear(GameObject parent)
        {
            this.parent = parent;
            yield return StartCoroutine(AsyncClear());
            parent.GetComponent<Room>().enabled = true;
            parent.GetComponent<Room>().SetId = DataController.Instance.GenerateId();
            transform.parent.GetComponent<RoomEditorTool>().SavePrefabFinale(parent);
        }
        private void Awake()
        {
            try
            {
                navMesherCollider = GetComponentInChildren<UnityEngine.AI.NavMeshSurface>().gameObject;
            }
            catch
            {
                Debug.Log("nope");
            }
            if (navMesherCollider != null)
            {
                Bake();
            }
            Room room = GetComponent<Room>();
            if (room)
            {
                room.SetEnemiesList = enemiesList;
                room.SetEnemiesListDied = new bool[enemiesList.Count];
                if (enemiesList.Count <= 0)
                    room.Complete();
            }
        }

        private IEnumerator AsyncClear()
        {
            SetupCollisionWalls();
            InitRefs();
            ClearFloors();
            ClearWalls();
            ClearObjects();
            ClearDrops();
            ClearEnemies();
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
            foreach (Transform enemy in enemies.transform)
                if (enemy.parent == enemies.transform)
                    enemiesList.Add(enemy.gameObject);

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
                floor.layer = 10;
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
                    if (col.GetComponentsInChildren<Collider>().Length > 0)
                    {
                        foreach (Collider cc in col.GetComponentsInChildren<Collider>())
                        {
                            cc.enabled = false;
                        }
                    }
                }

                obj.transform.localScale = Vector3.zero;
            }
        }
        void ClearEnemies()
        {

            foreach (GameObject obj in enemiesList)
            {
                Destroy(obj.GetComponent<LevelObject>());
                obj.GetComponentInChildren<Rigidbody>().isKinematic = false;
                obj.GetComponentInChildren<UnityEngine.AI.NavMeshAgent>().enabled = true;
                obj.GetComponentInChildren<StateMachine.StateMachine>().enabled = true;
            }


        }

        void Bake()
        {
            navMesherCollider.GetComponent<UnityEngine.AI.NavMeshSurface>().BuildNavMesh();
            foreach (GameObject obj in enemiesList)
            {
                try
                { obj.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true; }
                catch
                { Debug.Log("F"); }

                try { obj.GetComponent<StateMachine.Bat_Enemy.BatStateMachine>().enabled = true; }
                catch { Debug.Log("No bat"); }
                try { obj.GetComponent<StateMachine.Dragon_Enemy.DragonStateMachine>().enabled = true; }
                catch { Debug.Log("No dragon"); }
                try { obj.GetComponent<StateMachine.Slime_Enemy.SlimeStateMachine>().enabled = true; }
                catch { Debug.Log("No slime"); }
                try { obj.GetComponent<StateMachine.Golem_Enemy.GolemStateMachine>().enabled = true; }
                catch { Debug.Log("No golem"); }
                try { obj.GetComponent<StateMachine.Orc_Enemy.OrcStateMachine>().enabled = true; }
                catch { Debug.Log("No orc"); }
                try { obj.GetComponent<StateMachine.Plant_Enemy.PlantStateMachine>().enabled = true; }
                catch { Debug.Log("No plant"); }
                try { obj.GetComponentInChildren<StateMachine.Turtle_Enemy.TurtleStateMachine>().enabled = true; }
                catch { Debug.Log("No turtle"); }
            }
        }

    }
}