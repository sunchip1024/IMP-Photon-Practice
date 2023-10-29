using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class player : MonoBehaviour
{
    public PhotonView PV;

    public GameObject EmoticonManager;


    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public float jumpPower = 5.0f;
    private float rotX;
    private float rotY;
    [SerializeField] float sensitivity;


    public GameObject followCam;

    //애니메이션 처리
    [SerializeField]
    private Animator animator;
    public bool isMoving;
    public bool isRunning;

    public bool isLocalPlayer = false;
    public bool isJumping = false;

    //UI 처리
    public Text PlayerName;
    public string playername;

    public Rigidbody rigid;

    public GameObject cameras;
    public GameObject NameTag;

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

        if (PV.IsMine)
        {
            Debug.Log("로컬 플레이");
            isLocalPlayer = true;
            TestManager.instance._player = this;
        }
        if (isLocalPlayer)
        {
            Debug.Log("로컬이군요!");
            SetName(playername);
            cameras.SetActive(true);
            gameObject.tag = "LocalPlayer";
            NameTag.SetActive(false);
        }
        else
        {
            Debug.Log("로컬이 아니군요!");
        }
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
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
    private void SetName(string name)
    {
        Debug.Log("이름을 " + name + "(으)로 세팅하겠습니다!");
        PV.RPC(nameof(SetNameRPC), RpcTarget.AllBuffered, name);
    }

    [PunRPC]
    public void SetNameRPC(string name)
    {
        PlayerName = GetComponentInChildren<Text>();
        PlayerName.text = name;

        if (isLocalPlayer)
        {
            Debug.Log("Local");
            //PlayerName.enabled = false;
        }
        else
        {
            Debug.Log("Not local");
        }
    }

    void Jump()
    {
        //스페이스 키를 누르면 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //바닥에 있으면 점프를 실행
            if (!isJumping)
            {
                //print("점프 가능 !");
                isJumping = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }

            //공중에 떠있는 상태이면 점프하지 못하도록 리턴
            else
            {
                //print("점프 불가능 !");
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //바닥에 닿으면
        if (collision.gameObject.CompareTag("Ground"))
        {
            //점프가 가능한 상태로 만듦
            isJumping = false;
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

    [ContextMenu("이모티콘 변경")]
    public void SetEmoticon(string emoticon)
    {
        PV.RPC(nameof(SetEmoticonRPC), RpcTarget.AllBuffered, emoticon);
    }


    [PunRPC]
    public void SetEmoticonRPC(string emoticon)
    {
        EmoticonManager.GetComponent<EmoticonManager>().SetEmoticon(emoticon);
    }

    [ContextMenu("회전정지")]
    public void StopMoving()
    {
        rigid.angularVelocity = new Vector3(0, 0, 0);
    }
}
