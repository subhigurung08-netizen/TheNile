using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawned;
    public int max=10;
    public int count;
    public LayerMask groundLayer;
   
    void Start()
    {
       count =0;
    }

    void Update()
    {

        if(count<max){
            // int randomIndex = Random.Range(0, spawned.Length);
            Vector3 positionSpawned = new Vector3(Random.Range(0,1000), Random.Range(5,20), Random.Range(0,1000));
            Instantiate(spawned, positionSpawned, Quaternion.identity);
            count++;
        }
}
}
