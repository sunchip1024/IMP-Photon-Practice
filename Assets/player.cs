using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    private float rotX;
    private float rotY;
    [SerializeField] float sensitivity;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * movementSpeed * Time.deltaTime;

        if (!CheckWallCollision(movement))
        {
            transform.Translate(movement);
        }

        Turn();
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

    bool CheckWallCollision(Vector3 movement)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
        {
            if (hit.collider.tag == "Wall")
            {
                // Adjust movement to stop before the wall
                transform.Translate(movement.normalized * (hit.distance - 0.1f));
                return true;
            }
        }
        return false;
    }
}
