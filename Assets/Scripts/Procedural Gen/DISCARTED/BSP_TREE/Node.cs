using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    private List<Node> childrenNodes;
    public List<Node> ChildrenNodes { get { return childrenNodes; } }

    public bool Visited { get; set; }

    public Vector2Int BottomLeftCorner { get; set; } //btmL
    public Vector2Int BottomRightCorner { get; set; } //btmR
    public Vector2Int TopLeftCorner { get; set; } //tpL
    public Vector2Int TopRightCorner { get; set; } //tpR
//          btmL        btmR        tpL         tpR
//        ////////    ////////    *///////    ///////*
//        ////////    ////////    ////////    ////////
//        ////////    ////////    ////////    ////////
//        *///////    ///////*    ////////    ////////
    public Node Parent { get; set; }
    public int TreeLayerIndex { get; set; }

    public Node(Node parentNode)
    {
        childrenNodes = new List<Node>();
        this.Parent = parentNode;
        if (parentNode != null)
            parentNode.AddChild(this);

    }
    public void AddChild(Node node) => childrenNodes.Add(node);
    public void RemoveChild(Node node) => childrenNodes.Remove(node);

}
