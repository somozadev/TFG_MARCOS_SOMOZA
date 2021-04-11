using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StructureHelper
{
    //allows us to extract all children nodes from the parent node provided
    public static List<Node> TravelGraphToStractLowestLeafes(RoomNode parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> returnList = new List<Node>();

        if (parentNode.ChildrenNodes.Count == 0)
        {
            return new List<Node>() { parentNode };
        }
        foreach(Node child in parentNode.ChildrenNodes)
        {
            nodesToCheck.Enqueue(child);
        }
        while(nodesToCheck.Count > 0)
        {
            var currentNode = nodesToCheck.Dequeue();
            if(currentNode.ChildrenNodes.Count == 0)
            {
                returnList.Add(currentNode);
            }
            else
            {
                foreach(Node child in currentNode.ChildrenNodes)
                {
                    nodesToCheck.Enqueue(child);
                }
            }
        }

        return returnList;
    }

    //gives us the new bottomleft point corner, inside the given roomnode
    public static Vector2Int GenerateBottomLeftCorner(Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;

        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;

        return new Vector2Int(Random.Range(minX,(int) (minX+(maxX-minX)*pointModifier)), Random.Range(minY,(int) (minY+(maxY-minY)*pointModifier))); 
    }
    //gives us the new topright point corner, inside the given roomnode
    public static Vector2Int GenerateTopRightCorner(Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;

        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;

        return new Vector2Int(Random.Range((int)(minX + (maxX-minX) * pointModifier),maxX), Random.Range((int) (minY + (maxY-minY) * pointModifier),maxY)); 
    }
}
