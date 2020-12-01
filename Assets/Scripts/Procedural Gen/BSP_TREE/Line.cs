using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
///Stores the information about the line we create to divide space
///

public class Line
{
    private Orientation orientation;
    private Vector2Int coords;

    public Line(Orientation orientation, Vector2Int coords)
    {
        this.orientation = orientation;
        this.coords = coords;
    }
    public Orientation Orientation { get { return orientation; } set { orientation = value; } }
    public Vector2Int Coords { get { return coords; } set { coords = value; } }

    
}

public enum Orientation
{
    Horizontal = 0,
    Vertical = 1
}