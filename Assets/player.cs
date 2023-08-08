using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class player : MonoBehaviour
{
    public PhotonView PV;


    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    private float rotX;
    private float rotY;
    [SerializeField] float sensitivity;


    public GameObject followCam;

    //局聪皋捞记 贸府
    private Animator animator;
    public bool isMoving;
    public bool isRunning;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        isMoving = horizontalInput != 0 || verticalInput != 0;
        isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * (isRunning ? runSpeed : walkSpeed) * Time.deltaTime;

        //if (!CheckWallCollision(movement))
        //{
        //    transform.Translate(movement);
        //}

        transform.Translate(movement);

        Turn();
        SetAnimation();
    }

    void Turn()
    {
        if (Input.GetMouseButton(0))
        {
            rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;


            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
            transform.rotation = rot;
        }
    }

    void SetAnimation()
    {
        if (isMoving)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    //bool CheckWallCollision(Vector3 movement)
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
    //    {
    //        if (hit.collider.tag == "Wall")
    //        {
    //            // Adjust movement to stop before the wall
    //            transform.Translate(movement.normalized * (hit.distance - 0.1f));
    //            return true;
    //        }
    //    }
    //    return false;
    //}
}
