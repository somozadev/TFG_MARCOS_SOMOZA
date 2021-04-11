using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    private int maxIterations;
    private int minRoomW;
    private int minRoomH;
    
    public RoomGenerator(int maxIterations, int minRoomW, int minRoomH)
    {
        this.maxIterations = maxIterations;
        this.minRoomH = minRoomH;
        this.minRoomW = minRoomW;
    }

    public List<RoomNode> GenerateRoomsInSpaces(List<Node> roomSpaces)
    {
        List<RoomNode> returnList = new List<RoomNode>();
        foreach (Node roomSpace in roomSpaces)
        {
            Vector2Int newBottomLeftCorner = StructureHelper.GenerateBottomLeftCorner(roomSpace.BottomLeftCorner,roomSpace.TopRightCorner,0.1f,1);
            Vector2Int newTopRightCorner = StructureHelper.GenerateTopRightCorner(roomSpace.BottomLeftCorner,roomSpace.TopRightCorner,0.9f,1);

            roomSpace.BottomLeftCorner = newBottomLeftCorner;
            roomSpace.TopRightCorner = newTopRightCorner;
        }
        return null;
    }

}
