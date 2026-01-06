using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavMeshPathfinding : MonoBehaviour
{
    [SerializeField] private Vector3 goal;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = goal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag=="Player")
        {
            Destroy(player, 1f);
            SceneManager.LoadScene(1);
        }
    }
}
