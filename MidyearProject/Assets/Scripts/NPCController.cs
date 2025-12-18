using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCController : MonoBehaviour
{
    private List<string> states;
    public float roamSpeed;
    public Transform player;
    public GameObject target;
    private bool isCaptured = false;
    // private bool isEnchanted = false;
    // public float patrolSpeed = 10f;
    // public float fleeSpeed = 20f;
    public float detectionDistance = 20f;
    // public float loseSightDistance = 20f;
    // public float patrolRadius = 500f;

    // private Vector3 patrolTarget;
    // private bool fleeing = false;
    public LayerMask ground;
    private int enchantmentTime;
   
    // Start is called before the first frame update
 
    void Start()
    {

       
    //     states = new List<string>();
       
    //     states.Add("Roam");
    //     states.Add("Chase");
    //     states.Add("Enchantment");
        
    }


    
    

    void Update()
    {
        
        if(player !=null && target != null)
        {
        
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            Ray ray = new Ray(transform.position + Vector3.up * 0.5f, directionToPlayer.normalized);
            RaycastHit hit;
            bool playerInSight = false;
            if(transform.localScale == new Vector3(.2f, 2f, 2f))
            {
                enchantmentTime++;
                if(enchantmentTime>200)
                {
                    Capture();
                }
            
                // else
                // {
                //     enchantmentTime = 0;
                // }
            }
            if(player!=null && target != null)
            {
                if (Physics.Raycast(ray, out hit, detectionDistance))
                {
                    if (hit.transform == player)
                    {
                        // playerInSight = true;
                    
                        Chase();
                    }
                }
                
                else
                {
                    Roam();
                }
                Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.red);
            }
        }
    }

        
    
    

        void Roam()
        {
        
            if(!isCaptured)
            {
            Debug.Log("State: Roam");
            
            transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 move2 = new Vector3(Random.Range(-100, 100) * 1f, 0f, Random.Range(-100, 100) * 1f);
            transform.position += move2.normalized;
            Vector3 dir = player.transform.position - transform.position;
            if(dir.magnitude< detectionDistance){
                Chase();
            }
            // }

            }
        }
        
        void Chase()
        {
            if(!isCaptured)
            {
            Debug.Log("State: Chase");
            transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0f;
            if(dir.magnitude < 2f)
            {
                Enchantment();
            }
            Vector3 move = new Vector3(dir.x, dir.y, dir.z);
            transform.position += move.normalized;
        // {
        //     SetNewPatrolTarget();
        // }
        // else
        // {
    
            
        // }
            // }
            }
        }

    void OnTriggerEnter(Collider col)
    {
        if(!isCaptured)
        {
        // if(player!=null && target != null)
        // {
        if (col.CompareTag("Player"))
        {
            Enchantment();
        }
        // }
        }
    }
    
    void Enchantment()
    {
        
        if(!isCaptured)
        {
      
        Debug.Log("State: Enchantment");
        // if(player!=null && target != null)
        // {
        if(transform.localScale.x == 1)
        {
            Vector3 scaleChange = new Vector3(.2f, 2f, 2f);
            transform.localScale = scaleChange;
        }
        // }
        
        }


    }


    // IEnumerator LoadCapture()
    // {
    //      yield return new WaitForSeconds(2f);
    // }

    void Capture()
    {
        isCaptured = true;
        Debug.Log("State: Capture");
        
        // StartCoroutine(LoadCapture());
        Debug.Log("State: Capture");
        Destroy(target, 2f);
    } 
    


}