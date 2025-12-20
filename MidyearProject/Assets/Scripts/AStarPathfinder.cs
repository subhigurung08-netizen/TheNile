using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script implements A* on the MazeGenerator3D's grid.
// A* uses:
//   g = cost from START to this cell
//   h = heuristic estimate from this cell to GOAL
//   f = g + h
// At each step, it expands the cell with the lowest f.

public class AStarPathfinder : MonoBehaviour
{
    public MazeGenerator3D mazeGenerator;

    // Directions for 4-way movement
    private static readonly Vector2Int[] Directions = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    public List<MazeCell> FindPath()
    {
        MazeCell[,] grid = mazeGenerator.grid;
        Vector2Int startPos = mazeGenerator.startCell;
        Vector2Int goalPos = mazeGenerator.goalCell;

        MazeCell start = grid[startPos.x, startPos.y];
        MazeCell goal = grid[goalPos.x, goalPos.y];

        // Reset A* data
        foreach (var cell in grid)
        {
            cell.gCost = int.MaxValue;
            cell.hCost = 0;
            cell.fCost = 0;
            cell.parent = null;
        }

        List<MazeCell> openSet = new List<MazeCell>();
        HashSet<MazeCell> closedSet = new HashSet<MazeCell>();

        // Initialize start
        start.gCost = 0;
        start.hCost = Heuristic(start, goal);
        start.fCost = start.gCost + start.hCost;
        openSet.Add(start);

        // A* main loop
        while (openSet.Count > 0)
        {
            // 1. Pick cell with lowest f = g + h
            MazeCell current = GetLowestFCost(openSet);

            // If we reached the goal, build the path
            if (current == goal)
            {
                return ReconstructPath(goal);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            // 2. Check neighbors
            foreach (var dir in Directions)
            {
                int nx = current.x + dir.x;
                int ny = current.y + dir.y;

                if (nx < 0 || nx >= grid.GetLength(0) || ny < 0 || ny >= grid.GetLength(1))
                    continue;

                MazeCell neighbor = grid[nx, ny];

                // Ignore walls and closed cells
                if (!neighbor.isWall && !closedSet.Contains(neighbor))
                {
                   
            
                // Cost from start to neighbor through current (each move cost = 1)
                    int tentativeG = current.gCost + 1;

                // If this path to neighbor is better OR neighbor is not in openSet
                    if (tentativeG < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                    neighbor.gCost = tentativeG;
                    neighbor.hCost = Heuristic(neighbor, goal);
                    neighbor.fCost = neighbor.gCost + neighbor.hCost;
                    neighbor.parent = current;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
                }
            }
        }

        // No path found
        Debug.LogWarning("A*: No path found!");
        return new List<MazeCell>();
    }

    // Manhattan distance heuristic (grid, no diagonals)
    private int Heuristic(MazeCell a, MazeCell b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private MazeCell GetLowestFCost(List<MazeCell> list)
    {
        MazeCell best = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].fCost < best.fCost)
            {
                best = list[i];
            }
        }
        return best;
    }

    private List<MazeCell> ReconstructPath(MazeCell end)
    {
        List<MazeCell> path = new List<MazeCell>();
        MazeCell current = end;

        // Follow parent pointers back to the start
        while (current != null)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }
}
