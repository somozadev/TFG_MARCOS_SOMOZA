using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace EditorTool
{
    public class GridBase : MonoBehaviour
    {

        [SerializeField] TMP_InputField xInput;
        [SerializeField] TMP_InputField zInput;


        public List<GameObject> gridStuff;
        public GameObject prefab;
        public int x;
        public int z;
        public float offset;

        public Node[,] grid;

        private GameObject targetToDestroy;

        public void CreateGridButton()
        {
            x = System.Convert.ToInt32(xInput.text);
            z = System.Convert.ToInt32(zInput.text);

            if (gridStuff.Count <= 0)
            {
                if (targetToDestroy != null)
                    Destroy(targetToDestroy);
                CreateGrid();
                CreateMouseCollision();
                GetComponent<RoomEditorTool>().enabled = true;
                Camera.main.GetComponent<ToolCameraMovement>().enabled = true;
                GetComponent<RoomEditorUIHelper>().GridPanelObj.GetComponent<Animator>().SetTrigger("Close");
                GetComponent<RoomEditorUIHelper>().openClosGrid = true;
                GetComponent<RoomEditorUIHelper>().AssetsPanelObj.SetActive(true);
                GetComponent<RoomEditorUIHelper>().AssetButton.gameObject.SetActive(true);
                GetComponent<RoomEditorUIHelper>().DropItemPanelObj.SetActive(true);
                GetComponent<RoomEditorUIHelper>().DropItemButton.gameObject.SetActive(true);

            }
            else
            {
                GetComponent<RoomEditorTool>().enabled = false;
                Camera.main.GetComponent<ToolCameraMovement>().enabled = false;
                GetComponent<RoomEditorUIHelper>().GridPanel();
                GetComponent<RoomEditorUIHelper>().AssetsPanelObj.SetActive(false);
                GetComponent<RoomEditorUIHelper>().AssetButton.gameObject.SetActive(false);
                DeleteLastGrid();
                CreateGridButton();
            }
            // GetComponent<RoomEditorUIHelper>().GridButton.gameObject.SetActive(false);

        }
        public void DeleteLastGrid()
        {
            List<GameObject> aux = gridStuff;
            foreach (GameObject g in gridStuff)
            {
                Destroy(aux[aux.IndexOf(g)]);
            }
            gridStuff.Clear();

            List<GameObject> aux1 = GetComponent<RoomEditorTool>().inSceneGameObjects;
            foreach (GameObject g in GetComponent<RoomEditorTool>().inSceneGameObjects)
            {
                Destroy(aux1[aux1.IndexOf(g)]);
            }
            GetComponent<RoomEditorTool>().inSceneGameObjects.Clear();

            List<GameObject> aux2 = GetComponent<RoomEditorTool>().inSceneWalls;
            foreach (GameObject g in GetComponent<RoomEditorTool>().inSceneWalls)
            {
                Destroy(aux2[aux2.IndexOf(g)]);
            }
            GetComponent<RoomEditorTool>().inSceneWalls.Clear();




        }
        public void SetX(InputField inputField)
        {
            x = System.Convert.ToInt32(inputField.text);
        }
        public void SetZ(InputField inputField)
        {
            z = System.Convert.ToInt32(inputField.text);
        }

        public void CreateGrid()
        {
            grid = new Node[x, z];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    float posX = i * offset;
                    float posZ = j * offset;

                    GameObject aux = Instantiate(prefab, new Vector3(posX, 0, posZ), Quaternion.identity) as GameObject;
                    aux.transform.parent = transform.GetChild(0).transform.GetChild(0).transform;

                    NodeObject nodeObj = aux.GetComponent<NodeObject>();
                    nodeObj.x = i;
                    nodeObj.z = j;

                    Node node = new Node();
                    node.floorObj = aux;
                    node.x = i;
                    node.z = j;

                    grid[i, j] = node;
                    gridStuff.Add(aux);

                }
            }
        }

        public void CreateMouseCollision()
        {
            GameObject aux = new GameObject();
            aux.AddComponent<BoxCollider>();
            aux.GetComponent<BoxCollider>().size = new Vector3(x * offset, 0.1f, z * offset);
            aux.transform.position = new Vector3(((x * offset) / 2) - 2, 0, ((z * offset) / 2) - 2);
            gridStuff.Add(aux);
            aux.transform.SetParent(transform.GetChild(0));

            GameObject targeter = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            targeter.transform.position = aux.transform.position;
            targeter.transform.rotation = Quaternion.identity;
            targeter.transform.SetParent(transform);
            targeter.GetComponent<MeshRenderer>().material = GetComponent<RoomEditorTool>().highLitedMat;
            targetToDestroy = targeter;
            Camera.main.GetComponent<ToolCameraMovement>().target = targeter.transform;
            Camera.main.GetComponent<ToolCameraMovement>().startPos = targeter.transform.position;
        }

        public Node NodeFromWorldPos(Vector3 worldPos)
        {
            float worldX = worldPos.x;
            float worldZ = worldPos.z;
            worldX /= offset;
            worldZ /= offset;

            int innerx = Mathf.RoundToInt(worldX);
            int innerz = Mathf.RoundToInt(worldZ);

            if (innerx > x)
                innerx = x;
            if (innerz > z)
                innerz = z;
            if (innerx < 0)
                innerx = 0;
            if (innerz < 0)
                innerz = 0;

            if (grid[innerx, innerz] != null)

                return grid[innerx, innerz];

            else
                return null;

        }

    }
}
