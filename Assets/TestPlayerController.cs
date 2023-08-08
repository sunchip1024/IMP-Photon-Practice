using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    Animator animator;
    CharacterController _controller;

    public float speed = 2f;
    public float runSpeed = 8f;
    public float finalSpeed;
    public bool isRunning;
    public float smoothness;

    //public bool toggleCameraRotation;
    // Start is called before the first frame update
    void Start()
    {
        _controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMoveMent();
    }

    private void LateUpdate()
    {
        //Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
    }

    void InputMoveMent()
    {
        finalSpeed = (isRunning) ? runSpeed : speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");
        _controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);
    }

}
