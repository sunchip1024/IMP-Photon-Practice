using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayer : MonoBehaviour
{


    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpPower = 5.0f;
    private float rotX;
    private float rotY;
    [SerializeField] float sensitivity;


    public GameObject followCam;

    //�ִϸ��̼� ó��
    [SerializeField]
    private Animator animator;
    public bool isMoving;
    public bool isRunning;

    public bool isLocalPlayer = false;
    public bool isJumping = false;


    public Rigidbody rigid;

    public GameObject cameras;
    public Camera playerCamera;

    private void Start()
    {
        try
        {
            animator = GetComponent<Animator>();
        }
        catch
        {
            Debug.Log("There is no animator");
        }

        isLocalPlayer = true;
        cameras.SetActive(true);
        gameObject.tag = "LocalPlayer";
        rigid = GetComponent<Rigidbody>();

        CanvasManager.instance.SetCamera(playerCamera);
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        isMoving = horizontalInput != 0 || verticalInput != 0;
        isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput) * (isRunning ? runSpeed : walkSpeed) * Time.deltaTime;


        transform.Translate(movement);
        Jump();
        Turn();
        if (animator) SetAnimation();
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

    void Jump()
    {
        //�����̽� Ű�� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�ٴڿ� ������ ������ ����
            if (!isJumping)
            {
                //print("���� ���� !");
                isJumping = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }

            //���߿� ���ִ� �����̸� �������� ���ϵ��� ����
            else
            {
                //print("���� �Ұ��� !");
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�ٴڿ� ������
        if (collision.gameObject.CompareTag("Ground"))
        {
            //������ ������ ���·� ����
            isJumping = false;
        }
    }

}
