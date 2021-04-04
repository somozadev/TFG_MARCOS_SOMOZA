using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneratorTool : MonoBehaviour
{

    [SerializeField] GameObject[] floors;
    [SerializeField] GameObject floorsHolder;
    [SerializeField] GameObject[] walls;
    [SerializeField] GameObject wallsHolder;
    [SerializeField] GameObject[,] floorTiles;
    [SerializeField] int height, width;

    void Start()
    {
        SetupFloor();
        SetupWalls();

    }
    public void SetupFloor()
    {

        floorTiles = new GameObject[width, height];
        Vector3 position = new Vector3(0, 0, 0);
        GameObject aux;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                floorTiles[x, y] = null;
                if (Random.Range(1, 11) < 9)
                {
                    aux = Instantiate(floors.PickOne(), position, Quaternion.identity);
                    aux.transform.SetParent(floorsHolder.transform);
                    floorTiles[x, y] = aux;
                }
                position += new Vector3(0, 0, 4);
            }

            position += new Vector3(4, 0, -position.z);
        }
    }
    public void SetupWalls()
    {
        Vector3 position = new Vector3(0, 0, 2);
        for (int x = 0; x < floorTiles.GetLength(0); x++)
        {
            for (int y = 0; y < floorTiles.GetLength(1); y++)
            {
                if (y < floorTiles.GetLength(1)-1)
                {
                    if (floorTiles[x, y + 1] == null)
                        PlaceWall(position);
                    else if (floorTiles[x, y] == null)
                        PlaceWall(position);
                }

                if (x + 1 < floorTiles.GetLength(0))
                {
                    if (floorTiles[x + 1, y] == null && floorTiles[x,y] != null)
                        PlaceWallEnd(floorTiles[x, y].transform.position + new Vector3(2, 0, 0));
                }
                if (y == floorTiles.GetLength(1) - 1 && floorTiles[x, y] != null)
                    PlaceWall(position);
                position += new Vector3(0, 0, 4);

                if (x == floorTiles.GetLength(0) - 1 && floorTiles[x, y] != null)
                    PlaceWallEnd(floorTiles[x, y].transform.position + new Vector3(2, 0, 0));

            }
            position += new Vector3(4, 0, -position.z + 2);
        }
    }

    private void PlaceWall(Vector3 position)
    {
        GameObject aux;
        aux = Instantiate(walls.PickOne(), position, Quaternion.Euler(0, 180, 0));
        aux.transform.SetParent(wallsHolder.transform);
    }
    private void PlaceWallEnd(Vector3 position)
    {
        GameObject aux;
        aux = Instantiate(walls.PickOne(), position, Quaternion.Euler(0, -90, 0));
        aux.transform.SetParent(wallsHolder.transform);
    }
}
