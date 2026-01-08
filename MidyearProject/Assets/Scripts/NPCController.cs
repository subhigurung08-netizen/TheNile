using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NPCController : MonoBehaviour
{
    private List<string> states;
    [SerializeField] private float roamSpeed;
    [SerializeField] private float chaseSpeed;
    public Transform player;
    public GameObject target;
    private bool isCaptured = false;
    [SerializeField] private float detectionDistance = 20f;
    [SerializeField] private float minX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxX;
    [SerializeField] private float maxZ;
    [SerializeField] private float positionY;

    [SerializeField] private LayerMask ground;
    [SerializeField] private int enchantmentTime;

    private float countRoam = 0f;
    

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
            
            // float x = transform.position.x;
            // float z = transform.position.z;
            Vector3 directionToPlayer = player.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;
            if(transform.position.x<minX || transform.position.x>maxX)
            {
                // float newX = Mathf.Clamp(transform.position.x, minX, maxX);
                float newX = Random.Range(minX+1, maxX-1);
                transform.position = new Vector3(newX, positionY, transform.position.z);
            }
            if(transform.position.z<minZ || transform.position.z>maxZ)
            {
                // float newZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
                float newZ = Random.Range(minZ+1, maxZ-1);
                transform.position = new Vector3(transform.position.x, positionY, newZ);
            }
            if(Mathf.Abs(player.transform.position.y - transform.position.y) < 1f)
            {
                Ray ray = new Ray(transform.position, directionToPlayer.normalized);
                RaycastHit hit;
                // bool playerInSight = false;

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
                    
                    if (Physics.Raycast(ray, out hit, detectionDistance) && hit.transform == player && Mathf.Abs(player.transform.position.y - transform.position.y) < 1f)
                    {
                        countRoam = 0;
                        // if (hit.transform == player)
                        // {   

                        // playerInSight = true;
                    
                            if(hit.distance < 2f)
                            {
                                Enchantment();
                            }
                            Chase(directionToPlayer);
                        // }
                    }
                
                    else
                    {
                        countRoam++;
                        Roam(directionToPlayer);
                    }
                Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.red);
                }
            }

            else
            {
                countRoam++;
                Roam(directionToPlayer);
            }
        }
    }

        
    
    

        void Roam(Vector3 dir)
        {
            
            if(!isCaptured)
            {
                
                Debug.Log("State: Roam");
            
                transform.localScale = new Vector3(2f, 2f, 2f);
                if(countRoam>100)
                {
                    countRoam=0;
                    Vector3 move2 = new Vector3(Random.Range(-10, 10) * 1f, positionY, Random.Range(-10, 10) * 1f);
                    // transform.position += move2.normalized * roamSpeed * Time.deltaTime;
                    transform.position = new Vector3(move2.normalized.x * roamSpeed * Time.deltaTime, positionY, move2.normalized.z * roamSpeed * Time.deltaTime);
            // Vector3 dir = player.transform.position - transform.position;
                }

                else
                {
                    float addX = roamSpeed * Time.deltaTime;
                    float addZ = roamSpeed * Time.deltaTime;
                    transform.position = new Vector3(transform.position.x + addX, positionY, transform.position.z + addZ);
                }
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
                transform.position += move.normalized * chaseSpeed * Time.deltaTime;
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
        SceneManager.LoadScene(2);
        // Application.Quit();
    } 
    


}