using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedGroundFloor;
    [SerializeField] private GameObject spawnedFirstFloor;
    [SerializeField] private GameObject spawnedSecondFloor;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 GroundFloorPositionSpawned;
    [SerializeField] private Vector3 FirstFloorPositionSpawned;  
    [SerializeField] private Vector3 SecondFloorPositionSpawned;      
    private int groundFloorCount;
    private int firstFloorCount;
    private int secondFloorCount;

   
    void Start()
    {
       firstFloorCount =0;
       secondFloorCount =0;
    }

    void Update()
    {

        if(player.transform.position.y > 1.5 &&  player.transform.position.y < 2.5 && firstFloorCount == 0)
        {
            StartCoroutine(Spawn(0));
        }

        if(player.transform.position.y > 11.5 &&  player.transform.position.y < 12.5 && firstFloorCount==0){
            // int randomIndex = Random.Range(0, spawned.Length);
            firstFloorCount++;
            Vector3 positionSpawned = new Vector3(Random.Range(0,100), Random.Range(5,20), Random.Range(0,100));
            Instantiate(spawnedFirstFloor, FirstFloorPositionSpawned, Quaternion.identity);
         
        }

        if(player.transform.position.y > 21.5 &&  player.transform.position.y < 22.5 && secondFloorCount==0)
        {
            secondFloorCount++;
            Vector3 positionSpawned = new Vector3(Random.Range(0,100), Random.Range(5,20), Random.Range(0,100));
            Instantiate(spawnedSecondFloor, SecondFloorPositionSpawned, Quaternion.identity);
            
        }
    }

    
        
        
    

    IEnumerator Spawn(int floor)
    {
        if(floor==0)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(spawnedGroundFloor, GroundFloorPositionSpawned, Quaternion.identity);
        }
    }
}
