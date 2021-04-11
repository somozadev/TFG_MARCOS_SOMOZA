using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private int dungeonW, dungeonH;
    List<RoomNode> allSpaceNodes = new List<RoomNode>();

    public DungeonGenerator(int dungeonW, int dungeonH)
    {
        this.dungeonH = dungeonH;
        this.dungeonW = dungeonW;
    }

    public List<Node> CalculateRooms(int maxIterations, int minRoomW, int minRoomH)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonW, dungeonH);
        allSpaceNodes = bsp.NodesCollection(maxIterations, minRoomW, minRoomH);
        List<Node> roomSpaces  = StructureHelper.TravelGraphToStractLowestLeafes(bsp.RootNode);
        RoomGenerator roomGenerator = new RoomGenerator(maxIterations, minRoomW, minRoomH);
        List<RoomNode> roomList = roomGenerator.GenerateRoomsInSpaces(roomSpaces);
        return new List<Node>(allSpaceNodes);
    }
}
