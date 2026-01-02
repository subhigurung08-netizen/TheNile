using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform player;
   public float turnSpeed = 4.0f;
   public float offsetY;

   public float offsetZ;

   

	private Vector3 offset;

	void Start () {
		if(player!=null)
        {
            offset = new Vector3(player.position.x, player.position.y + offsetY, player.position.z  + offsetZ);
        }
    }

	void LateUpdate()
	{
        if(player!=null)
        {
		    offset = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
		    transform.position = player.position + offset; 
		    transform.LookAt(player.position);
        }
    }
}

