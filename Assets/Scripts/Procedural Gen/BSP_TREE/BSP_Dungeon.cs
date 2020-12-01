using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP_Dungeon : MonoBehaviour
{
    [SerializeField] int dungeonWidth, dungeonHeight;
    [SerializeField] int roomWidth, roomHeight;
    [SerializeField] int corridorWidth;
    [SerializeField] int maxIterations;

    [SerializeField] GameObject wallV,wallH;

    private void Genereate()
    {
        DungeonGenerator generator = new DungeonGenerator(dungeonWidth,dungeonHeight);
        var rooms = generator.CalculateRooms(roomWidth,roomHeight,maxIterations);
    } 
}
