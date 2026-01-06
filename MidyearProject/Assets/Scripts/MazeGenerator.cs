using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;  

public class MazeGenerator : MonoBehaviour
{
   
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int baseWidth;
    [SerializeField] private int baseHeight;
    [SerializeField] private int floorNumber;
    [SerializeField] private float wallHeight; 
    [SerializeField] private float cellSize; 
    [SerializeField] private List<GameObject> walls;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject floor;
    [SerializeField] private  bool [,] visited;
    // [SerializeField] private Stack<int> visitedX = new Stack<int>();
    // [SerializeField] private Stack<int> visitedZ = new Stack<int>();
    // [SerializeField] private GameObject goal;
    [SerializeField] private int entryStairsX;
    [SerializeField] private int entryStairsZ;

    [SerializeField] private int exitStairsX;
    [SerializeField] private int exitStairsZ;



    // Start is called before the first frame update
    void Start()
    {
        // visited = new bool[width, height];
        // walls = new List<GameObject>();
        GenerateMaze();
        
    }

    // Update is called once per frame
    void Update()
    {
    //    if(Input.GetKeyDown(KeyCode.Space)) {
    //         ClearMaze();
    //         GenerateMaze();
    //    }
    }

    void GenerateMaze()
    {
        visited = new bool[width, height];
        walls = new List<GameObject>();
        CreateGrid();
    }

    void CreateGrid()
    {
        for(int i=0; i<width; i++)
        {
            for(int j=0; j<height; j++)
            {
                CreateWall(i, j);
                
            }
        }
        StartCoroutine(Generate(entryStairsX, entryStairsZ));
        // if(floorNumber == 0)
        // {
        //     GetComponent<NavMeshSurface>().BuildNavMesh();
        // }
    }
    
    void CreateWall(int row, int col)
    {
        if(col!=0)
        {
            Vector3 positionNS = new Vector3(cellSize*  (baseWidth - width)/2 + row*cellSize + cellSize/2, 0, cellSize * (baseHeight - height)/2 + col*cellSize);
            GameObject cube1 = Instantiate(wall, positionNS, Quaternion.identity);
            // cube1.transform.position = positionNS;
            Vector3 sizeChange1 = new Vector3(.2f, wallHeight, cellSize);
            cube1.transform.localScale = sizeChange1;
            Vector3 changePosition1 = new Vector3(cube1.transform.position.x, cube1.transform.position.y+ wallHeight * floorNumber + wallHeight/2, cube1.transform.position.z);
            cube1.transform.position = changePosition1;
            walls.Add(cube1);
        }

        if(row!=0)
        {
            // Vector3 positionWE = new Vector3(row * cellSize, 0, col*cellSize + cellSize/2);
            Vector3 positionWE = new Vector3(cellSize * (baseWidth - width)/2 + row * cellSize, 0, cellSize * (baseHeight - height)/2 + col*cellSize + cellSize/2);
            GameObject cube2 = Instantiate(wall, positionWE, Quaternion.identity);
            // GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // Vector3 positionWE = new Vector3(row, 0, col + .5f);
            // cube2.transform.position = positionWE;
            // Vector3 sizeChange2 = new Vector3(1f, wallHeight, .2f);
            Vector3 sizeChange2 = new Vector3(cellSize, wallHeight, .2f);
            cube2.transform.localScale = sizeChange2;
            Vector3 changePosition2 = new Vector3(cube2.transform.position.x, cube2.transform.position.y + wallHeight * floorNumber + wallHeight/2, cube2.transform.position.z);
            cube2.transform.position = changePosition2;
            walls.Add(cube2);
        }
        // if(row == entryStairsX && col == entryStairsZ && floorNumber!=0)
        // {
        //     Vector3 positionFloor = new Vector3(cellSize * (baseWidth - width)/2, 0, cellSize * (baseHeight - height)/2 + col*cellSize);
        //     GameObject cube3 = Instantiate(floor, positionFloor, Quaternion.identity);
        //     Vector3 sizeChange3 = new Vector3(cellSize, .2f, cellSize);
        //     cube3.transform.localScale = sizeChange3;
        //     Vector3 changePosition3 = new Vector3(cube3.transform.position.x, cube3.transform.position.y + wallHeight * floorNumber - wallHeight/2, cube3.transform.position.z);
        //     cube3.transform.position = changePosition3;
        // }

        // else 
        if((row != entryStairsX || col != entryStairsZ) && floorNumber!=0)
        {
            Vector3 positionFloor = new Vector3(cellSize * (baseWidth - width)/2 + row * cellSize, 0, cellSize * (baseHeight - height)/2 + col*cellSize);
            GameObject cube3 = Instantiate(floor, positionFloor, Quaternion.identity);
            Vector3 sizeChange3 = new Vector3(cellSize, .2f, cellSize);
            cube3.transform.localScale = sizeChange3;
            Vector3 changePosition3 = new Vector3(cube3.transform.position.x, cube3.transform.position.y + wallHeight * floorNumber, cube3.transform.position.z);
            cube3.transform.position = changePosition3;

        }
    }

    private IEnumerator Generate(int x, int z)
    {
        visited[x, z] = true;
        // visitedX.Push(x);
        // visitedZ.Push(z);
        Debug.Log("x: " + x + " z: " + z + " floor: " + floorNumber);

        if(x == exitStairsX+floorNumber && z == exitStairsZ + floorNumber)
        {
            // goal.transform.position = new Vector3(x * cellSize, 0, z * cellSize);
            Debug.Log("sup");
            if(floorNumber == 0)
            {
                GetComponent<NavMeshSurface>().BuildNavMesh();
            }
            yield break;
        }
        List<Vector2Int> neighbor = NeighborCheck(x, z);

        if(neighbor.Count==0)
        {
            // int previousX = visitedX.Pop();
            // int previousZ = visitedZ.Pop();
            
            
            int newX = Random.Range(1, visited.GetLength(0)-1);
            Debug.Log("Random newX generated: " + newX);
            int newZ = Random.Range(1, visited.GetLength(1)-1);
            Debug.Log("Random newZ generated: " + newZ);

            while(visited[newX, newZ] == false || (newX == x && newZ == z))
            {
                newX = Random.Range(1, visited.GetLength(0)-1);
                Debug.Log("Random newX generated: " + newX);
                newZ = Random.Range(1, visited.GetLength(1)-1);
                Debug.Log("Random newZ generated: " + newZ);
            }

                Debug.Log("switch cell");
                StartCoroutine(Generate(newX, newZ));
            
            // StartCoroutine(Generate(previousX, previousZ));
            // goal.transform.position = new Vector3(x * cellSize, 0, z * cellSize);
            // yield break;
        }
        else
        {
            int index = Random.Range(0, neighbor.Count);
            Vector2Int chosen = neighbor[index];
            RemoveWall(x * cellSize, z * cellSize, chosen);
            Debug.Log("wall removed: " + " x: " + x + ", z: " + z);
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(Generate(x + chosen.x, z + chosen.y));
        }

    }

    private List<Vector2Int> NeighborCheck(int x, int z)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        Debug.Log("Checking Neighbors for: " + "x: " + x + ", z: " + z);
        if(z!= height-1 && visited[x, z+1]==false)
        {
            neighbors.Add(new Vector2Int(0, 1));
        }
        
        if(x!=width-1 && visited[x+1, z]==false)
        {
            neighbors.Add(new Vector2Int(1, 0));
        }

        

        if(z!=0 && visited[x, z-1]==false)
        {
            neighbors.Add(new Vector2Int(0, -1));
        }

        if(x!=0 && visited[x-1, z]==false)
        {
            neighbors.Add(new Vector2Int(-1, 0));
        }

        return neighbors;
    }
    
    void RemoveWall(float xvar, float zvar, Vector2Int chosen)
    {
        Vector3 target = Vector3.zero;
        // int midpointX= 0;
        // int midpointZ =0;
        if(chosen.x == 1 && chosen.y == 0)
        {
            // target = new Vector3(xvar + .5f, 0, zvar);
            target = new Vector3(xvar + cellSize/2, wallHeight * floorNumber + wallHeight/2, zvar);
        }

        else if(chosen.x== -1 && chosen.y == 0)
        {
            // target = new Vector3(xvar - .5f, 0, zvar);
            target = new Vector3(xvar - cellSize/2, wallHeight * floorNumber + wallHeight/2, zvar);
        }

        else if(chosen.x == 0 && chosen.y == 1)
        {
            // target = new Vector3(xvar, 0, zvar + .5f);
            target = new Vector3(xvar, wallHeight * floorNumber + wallHeight/2, zvar + cellSize/2);
        }

        else if(chosen.x == 0 && chosen.y == -1)
        {
            // target = new Vector3(xvar, 0, zvar - .5f);
            target = new Vector3(xvar, wallHeight * floorNumber + wallHeight/2, zvar - cellSize/2);
        }
        
        for(int i = 0; i<walls.Count; i++)
        {

            if(walls[i] != null)
            {
                if(Vector3.Distance(walls[i].transform.position, target) < .01f)
                {
                    Destroy(walls[i]);
                    walls[i] = null;
                    break;
                }
            }
        }
            // midpointX = (xvar + xvar+1)/2;
            // Vector3.Distance(walls[xvar*(zvar-1) + zvar], walls[(xvar+1)*(zvar-1) + zvar]);
        
    }

    void ClearMaze()
    {
        for(int i=0; i< walls.Count; i++){
            if(walls[i]!=null)
            {
                Destroy(walls[i]);
            }
        }
        walls.Clear();
    }

    
}
