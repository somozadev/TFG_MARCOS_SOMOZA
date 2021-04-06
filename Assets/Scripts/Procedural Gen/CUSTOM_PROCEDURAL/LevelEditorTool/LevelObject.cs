using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EditorTool
{
    public class LevelObject : MonoBehaviour
    {
        public string objectId;
        public int x;
        public int z;
        public GameObject prefab;
        public Vector3 posOffset;
        public Vector3 rotOffset;
        public bool isStackable;
        public bool isWall;
        public int rotationDegrees = 90;


        public void UpdateNode(Node[,] grid)
        {
            Node node = grid[x, z];

            Vector3 worldPos = node.floorObj.transform.position;
            worldPos += posOffset;
            transform.rotation = Quaternion.Euler(rotOffset);
            transform.position = worldPos;
        }

        public void ChangeRotation()
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles += new Vector3(0, rotationDegrees, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }
        public void ChangeRotation(float Degree)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles += new Vector3(0, Degree, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }

        public void Hide()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }


        public SaveableObject GetSaveableObject()
        {
            SaveableObject obj = new SaveableObject();
            obj.id = objectId;
            obj.posX = x;
            obj.posZ = z;

            rotOffset = transform.localEulerAngles;

            obj.rotX = rotOffset.x;
            obj.rotY = rotOffset.y;
            obj.rotZ = rotOffset.z;
            obj.isWallObj = isWall;
            obj.isStackable = isStackable;

            return obj;
        }

    }

    [System.Serializable]
    public class SaveableObject
    {
        public string id;
        public int posX, posZ;
        public float rotX, rotY, rotZ;

        public bool isWallObj, isStackable;

    }
}