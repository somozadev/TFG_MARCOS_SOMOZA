using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinarySpacePartitioner
{

    RoomNode rootNode;
    public RoomNode RootNode => rootNode;

    public BinarySpacePartitioner(int dungeonW, int dungeonH)
    {
        this.rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonW, dungeonH), null, 0);
    }
    public List<RoomNode> NodesCollection(int maxIterations, int minW, int minH)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> returnList = new List<RoomNode>();
        graph.Enqueue(this.rootNode);
        returnList.Add(this.rootNode);
        int iterations = 0;
        while (iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();
            if (currentNode.Width >= minW * 2 || currentNode.Height >= minH * 2)
            {
                SplitTheSpace(currentNode, returnList, minW, minH, graph);
            }
        }
        return returnList;
    }
    //
    private void SplitTheSpace(RoomNode currentNode, List<RoomNode> returnList, int minW, int minH, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(currentNode.BottomLeftCorner, currentNode.TopRightCorner, minW, minH);
        RoomNode node1, node2;
        if (line.Orientation == Orientation.Horizontal)
        {
            node1 = new RoomNode(currentNode.BottomLeftCorner, new Vector2Int(currentNode.TopRightCorner.x, line.Coords.y), currentNode, currentNode.TreeLayerIndex + 1);
            node2 = new RoomNode(new Vector2Int(currentNode.BottomLeftCorner.x, line.Coords.y), currentNode.TopRightCorner, currentNode, currentNode.TreeLayerIndex + 1);
        }
        else
        {
            node1 = new RoomNode(currentNode.BottomLeftCorner, new Vector2Int(line.Coords.x, currentNode.TopRightCorner.y), currentNode, currentNode.TreeLayerIndex + 1);
            node2 = new RoomNode(new Vector2Int(line.Coords.x, currentNode.BottomLeftCorner.y), currentNode.TopRightCorner, currentNode, currentNode.TreeLayerIndex + 1);
        }

        AddNewNodesToCollections(returnList,graph,node1);
        AddNewNodesToCollections(returnList,graph,node2);
    }

    //choose the orientation of the line based on the space size and returns a new line with all values needed
    public Line GetLineDividingSpace(Vector2Int BottomLeftCorner, Vector2Int TopRightCorner, int minW, int minH)
    {
        Orientation orientation;
        bool widthStatus = (TopRightCorner.y - BottomLeftCorner.y) >= 2 * minH;
        bool heightStatus = (TopRightCorner.x - BottomLeftCorner.x) >= 2 * minW;

        if (widthStatus && heightStatus)
            orientation = (Orientation)(Random.Range(0, 2));
        else if (widthStatus)
            orientation = Orientation.Vertical;
        else
            orientation = Orientation.Horizontal;

        return new Line(orientation, GetCoords(orientation, BottomLeftCorner, TopRightCorner, minW, minH));
    }
    //gets the coords for our new line checking our room size, randomly choosing between minW and minH
    private Vector2Int GetCoords(Orientation orientation, Vector2Int BottomLeftCorner, Vector2Int TopRightCorner, int minW, int minH)
    {
        Vector2Int coords = Vector2Int.zero;
        if (orientation == Orientation.Horizontal)
            coords = new Vector2Int(0, Random.Range(BottomLeftCorner.y + minH, TopRightCorner.y - minH));
        else
            coords = new Vector2Int(Random.Range(BottomLeftCorner.x + minW, TopRightCorner.x - minW), 0);

        return coords;
    }
    //adds new nodes to queue and list
    private void AddNewNodesToCollections(List<RoomNode> returnList, Queue<RoomNode> graph, RoomNode node)
    {
        returnList.Add(node);
        graph.Enqueue(node);
    }
}

