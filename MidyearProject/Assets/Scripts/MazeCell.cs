using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents one cell in the maze grid AND stores A* data.
public class MazeCell
{
    public int x;
    public int y;

    public bool visited = false;
    public bool isWall = false;   // We'll treat some cells as walls if needed

    public Vector3 worldPosition;

    // A* fields:
    public int gCost;      // distance from start
    public int hCost;      // heuristic to goal
    public int fCost;      // g + h
    public MazeCell parent; // previous cell on the best path

    public MazeCell(int x, int y, Vector3 worldPos)
    {
        this.x = x;
        this.y = y;
        this.worldPosition = worldPos;
    }
}

