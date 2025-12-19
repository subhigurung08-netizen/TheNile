using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float offsetZ;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            Vector3 movement = new Vector3(target.transform.position.x, offsetY, target.transform.position.z + offsetZ);
            transform.position = movement;
        }
    }

}
