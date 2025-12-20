using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Coordinates the whole mini-game:
// 1. Generates maze
// 2. Runs A*
// 3. Spawns NPC and Goal
// 4. Gives path to NPC
// 5. Visualizes the path

public class MazeGameManager : MonoBehaviour
{
    public MazeGenerator3D mazeGenerator;
    public AStarPathfinder pathfinder;

    public GameObject npcPrefab;
    public GameObject goalPrefab;
    public GameObject pathTilePrefab;

    private GameObject npcInstance;
    private GameObject goalInstance;

    void Start()
    {
        // 1. Generate maze
        mazeGenerator.GenerateMaze();
    }

    void Update()
    {
        // 2. Run A* to get path
        if(Input.GetKeyDown(KeyCode.Space))
        {
        List<MazeCell> path = pathfinder.FindPath();

        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("No path found, cannot start game.");
            return;
        }

        // 3. Spawn NPC at start
        MazeCell startCell = path[0];
        // Vector3 positionStart = new Vector3(startCell.worldPosition.x, .7f, startCell.worldPosition.y);
        npcInstance = Instantiate(npcPrefab, startCell.worldPosition, Quaternion.identity);

        // 4. Spawn Goal at end
        MazeCell goalCell = path[path.Count - 1];
        goalInstance = Instantiate(goalPrefab, goalCell.worldPosition, Quaternion.identity);

        // 5. Give path to NPC movement script
        npcInstance.GetComponent<NPCMovement>().SetPath(path);

        // 6. Visualize the path
        VisualizePath(path);
        }
    }

    void VisualizePath(List<MazeCell> path)
    {
        foreach (MazeCell cell in path)
        {
            
            Vector3 pos = cell.worldPosition + new Vector3(0, 0.01f, 0);
            Instantiate(pathTilePrefab, pos, Quaternion.identity);
        }
    }
}

