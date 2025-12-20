using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generates a simple 2D grid maze in 3D using recursive backtracking.
public class MazeGenerator3D : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 2f;

    public GameObject floorPrefab;
    public GameObject wallPrefab;

    // Public so A* can use it:
    public MazeCell[,] grid;
    public Vector2Int startCell = new Vector2Int(0, 0);
    public Vector2Int goalCell;

    private System.Random rng = new System.Random();

    void Start()
    {
        GenerateMaze();
    }

    public void GenerateMaze()
    {
        grid = new MazeCell[width, height];

        // Create all cells and spawn floor tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = new Vector3(x * cellSize, 0, y * cellSize);
                grid[x, y] = new MazeCell(x, y, worldPos);

                // Floor tile
                Instantiate(floorPrefab, worldPos, Quaternion.identity, transform);
            }
        }

        // DFS to carve passages
        Stack<MazeCell> stack = new Stack<MazeCell>();
        MazeCell current = grid[startCell.x, startCell.y];
        current.visited = true;
        stack.Push(current);

        // Directions: Up, Down, Left, Right
        Vector2Int[] dirs = {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };

        while (stack.Count > 0)
        {
            current = stack.Peek();
            List<MazeCell> unvisitedNeighbors = new List<MazeCell>();

            foreach (var d in dirs)
            {
                int nx = current.x + d.x;
                int ny = current.y + d.y;

                if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                {
                    MazeCell neighbor = grid[nx, ny];
                    if (!neighbor.visited)
                    {
                        unvisitedNeighbors.Add(neighbor);
                    }
                }
            }

            if (unvisitedNeighbors.Count > 0)
            {
                // Pick random neighbor
                MazeCell chosen = unvisitedNeighbors[rng.Next(unvisitedNeighbors.Count)];
                chosen.visited = true;
                stack.Push(chosen);
            }
            else
            {
                stack.Pop();
            }
        }

        // Optional walls: mark some cells as walls randomly (except start and goal)
        goalCell = new Vector2Int(width - 1, height - 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((x == startCell.x && y == startCell.y) ||
                    (x == goalCell.x && y == goalCell.y))
                    continue;

                // Low chance to become a wall
                if (rng.NextDouble() < 0.15)
                {
                    grid[x, y].isWall = true;
                    Vector3 pos = grid[x, y].worldPosition + new Vector3(0, 0.5f, 0);
                    Instantiate(wallPrefab, pos, Quaternion.identity, transform);
                }
            }
        }
    }
}

