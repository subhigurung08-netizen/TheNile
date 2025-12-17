using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCController : MonoBehaviour
{
    private List<string> states;
   
    // Start is called before the first frame update
 
    void Start()
    {
        states = new List<string>();
       
        states.Add("Roam");
        states.Add("Chase");
        states.Add("Enchantment");
        
    }

    public Transform player;
    // public float patrolSpeed = 10f;
    // public float fleeSpeed = 20f;
    public float detectionDistance = 20f;
    // public float loseSightDistance = 20f;
    // public float patrolRadius = 500f;

    // private Vector3 patrolTarget;
    // private bool fleeing = false;
    public LayerMask ground;
    

    void Update()
    {
        if(player!=null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, directionToPlayer.normalized);
            RaycastHit hit;
            bool playerInSight = false;
            if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            if (hit.transform == player)
            {
                playerInSight = true;
            }
        }

        }
    }
}