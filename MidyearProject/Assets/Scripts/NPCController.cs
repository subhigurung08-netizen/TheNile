

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

    public float detectionDistance = 20f;

    public LayerMask ground;
    private int enchantmentTime;

    // private List<Vector2Int> PlayerActionsX;
    // private List<Vector2Int> PlayerActionsZ;
   
    // Start is called before the first frame update
 
    void Start()
    {
        // PlayerActionsX = new List<Vector2Int>();
        // PlayerActionsZ = new List<Vector2Int>();
        
    }

    

    void Update()
    {
        
        if(player !=null && target != null)
        {
        
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            Ray ray = new Ray(transform.position, directionToPlayer.normalized);
            RaycastHit hit;
            bool playerInSight = false;
            if(transform.localScale == new Vector3(.2f, 3f, 2f))
            {
                enchantmentTime++;
                // switch(directionToPlayer.normalized.x)
                // {
                //     case -1:
                //         PlayerActionsX.Add(new Vector2Int(-1, 0));
                //         break;

                //     case 1:
                //         PlayerActionsX.Add(new Vector2Int(1,0));
                //         break;


                // }

                // switch(directionToPlayer.normalized.z)
                // {
                //     case -1:
                //         PlayerActionsZ.Add(new Vector2Int(0, -1));
                //         break;

                //     case 1:
                //         PlayerActionsZ.Add(new Vector2Int(0, 1));
                //         break;


                // }

                // if(directionToPlayer.x > 0)
                // {
                //     PlayerActions.Add(directionToPlayer);
                // }
                if(enchantmentTime>200)
                {
                    Capture();
                }
            
            }
            if(player!=null && target != null)
            {
                if (Physics.Raycast(ray, out hit, detectionDistance))
                {
                    if (hit.transform == player)
                    {
                        // playerInSight = true;
                    
                        if(hit.distance < 2f)
                        {
                            Enchantment();
                        }
                        Chase(directionToPlayer);
                    }
                }
                
                else
                {
                    Roam(directionToPlayer);
                }
                Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.red);
            }
        }
    }

        
    
    

        void Roam(Vector3 dir)
        {
        
            if(!isCaptured)
            {
            Debug.Log("State: Roam");
            
            transform.localScale = new Vector3(2f, 2f, 2f);
            Vector3 move2 = new Vector3(Random.Range(-100, 100) * 1f, 0f, Random.Range(-100, 100) * 1f);
            transform.position += move2.normalized;
            // Vector3 dir = player.transform.position - transform.position;
            if(dir.magnitude< detectionDistance)
            {
                Chase(dir);
            }
            }

            
        }
        
        void Chase(Vector3 dir)
        {
            if(!isCaptured)
            {
            Debug.Log("State: Chase");
            transform.localScale = new Vector3(3f, 2f, 2f);
            // Vector3 dir = player.transform.position - transform.position;
            dir.y = 0f;
            if(dir.magnitude < 10f)
            {
                Enchantment();
            }
            Vector3 move = new Vector3(dir.x, dir.y, dir.z);
            transform.position += move.normalized ;
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
        
        if(transform.localScale.x == 3f)
        {
            Vector3 scaleChange = new Vector3(.2f, 3f, 2f);
            transform.localScale = scaleChange;
        }
        // }
        
        }
      


    }



    void Capture()
    {
        isCaptured = true;
        Debug.Log("State: Capture");
        
        // StartCoroutine(LoadCapture());
        Vector3 scaleChange = new Vector3(.2f, 4f, 2f);
        transform.localScale = scaleChange;
        Destroy(target, 1f);
    } 
    


}