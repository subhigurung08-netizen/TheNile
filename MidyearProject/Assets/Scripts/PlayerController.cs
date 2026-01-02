using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public float speed;
    // public Transform cam;

    private Vector3 movementInput;
    private Vector2 mouseInput;

    private float rotationX;


    [SerializeField] private LayerMask floorMask;
    [SerializeField] private Transform transformFeet;
    [SerializeField] private Transform camera; 
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float moveX = Input.GetAxis("Horizontal");
        // float moveZ = Input.GetAxis("Vertical");
        // // Vector3 movement = new Vector3(transform.position.x + moveX * speed, 1f, transform.position.z + moveZ * speed);
        

        // Vector3 forwardCam = cam.forward;
        // Vector3 rightCam = cam.right;
        // forwardCam.y = 1f;
        // forwardCam.Normalize();
        // rightCam.y = 1f;
        // rightCam.Normalize();


        // Vector3 movement = moveX * rightCam + moveZ * forwardCam;
        // movement.y = 1f;
        // rb.AddForce(movement * speed);

        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MoveCamera();
        
    }

    private void MovePlayer()
    {
        Vector3 playerMove = transform.TransformDirection(movementInput) * speed;
        rb.velocity = new Vector3(playerMove.x, rb.velocity.y, playerMove.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(transformFeet.position, .1f, floorMask))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void MoveCamera()
    {
        rotationX -= mouseInput.y * sensitivity;
        transform.Rotate(0f, mouseInput.x * sensitivity, 0f);
        camera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
