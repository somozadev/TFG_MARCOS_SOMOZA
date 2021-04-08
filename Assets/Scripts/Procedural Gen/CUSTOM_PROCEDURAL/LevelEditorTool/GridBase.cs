using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EditorTool
{
    public class GridBase : MonoBehaviour
    {
        public List<GameObject> gridStuff;
        public GameObject prefab;
        public int x;
        public int z;
        public float offset;

        public Node[,] grid;

        private void Start()
        {
            CreateGrid();
            CreateMouseCollision();
        }

        public void DeleteLastGrid()
        {
            foreach (GameObject o in gridStuff)
            {
                Destroy(o);
            }
            gridStuff.Clear();
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
                    aux.transform.parent = transform.GetChild(0).transform;

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
            Camera.main.GetComponent<ToolCameraMovement>().target = aux.transform; 
        }

        public Node NodeFromWorldPos(Vector3 worldPos)
        {


            //MakeNodeGlowHere?? // nor item ?

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
