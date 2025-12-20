using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public float wallSize; 
    public  bool [,] visited;
    public List<GameObject> walls;
    public GameObject goal;
   

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
       if(Input.GetKeyDown(KeyCode.Space)) {
            ClearMaze();
            GenerateMaze();
       }
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
        StartCoroutine(Generate(0 , 0));
    }
    
    void CreateWall(int row, int col)
    {
        if(col!=0)
        {
            GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Vector3 positionNS = new Vector3(row+ .5f, 0, col);
            cube1.transform.position = positionNS;
            Vector3 sizeChange1 = new Vector3(.2f, wallSize, 1f);
            cube1.transform.localScale = sizeChange1;
            walls.Add(cube1);
        }

        if(row!=0)
        {
        GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 positionWE = new Vector3(row, 0, col + .5f);
        cube2.transform.position = positionWE;
        Vector3 sizeChange2 = new Vector3(1f, wallSize, .2f);
        cube2.transform.localScale = sizeChange2;
        walls.Add(cube2);
        }
    }

    private IEnumerator Generate(int x, int z)
    {
        visited[x, z] = true;
        List<Vector2Int> neighbor = NeighborCheck(x, z);
        if(neighbor.Count==0){
            goal.transform.position = new Vector3(x, 0, z);
            yield break;
        }
        int index = Random.Range(0, neighbor.Count);
        Vector2Int chosen = neighbor[index];
        RemoveWall(x, z, chosen);
        yield return new WaitForSeconds(0.05f);
        
        StartCoroutine(Generate(x + chosen.x, z + chosen.y));
        

    }

    private List<Vector2Int> NeighborCheck(int x, int z)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
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
    
    void RemoveWall(int xvar, int zvar, Vector2Int chosen)
    {
        Vector3 target = Vector3.zero;
        // int midpointX= 0;
        // int midpointZ =0;
        if(chosen.x == 1 && chosen.y == 0)
        {
            target = new Vector3(xvar + .5f, 0, zvar);
        }

        else if(chosen.x== -1 && chosen.y == 0)
        {
            target = new Vector3(xvar - .5f, 0, zvar);
        }

        else if(chosen.x == 0 && chosen.y == 1)
        {
            target = new Vector3(xvar, 0, zvar + .5f);
        }

        else if(chosen.x == 0 && chosen.y == -1)
        {
            target = new Vector3(xvar, 0, zvar - .5f);
        }
        
        for(int i = 0; i<walls.Count; i++){
            if(walls[i] != null){
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
