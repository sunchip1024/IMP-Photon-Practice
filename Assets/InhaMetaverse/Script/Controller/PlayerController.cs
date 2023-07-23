using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


public class PlayerController : MonoBehaviour
{
    public PhotonView PV;

    // 애니메이션
    private Animator animator;

    public enum CameraType { FpCamera, TpCamera };

    public Camera PlayerCamera;

    public Camera tpCamera;
    public Camera fpCamera;
    public Camera uiCamera;

    [HideInInspector] public GameObject tpCamObject;
    [HideInInspector] public GameObject fpCamObject;
    public GameObject uiCamObject;

    [HideInInspector] public Transform tpRoot;
    [HideInInspector] public Transform tpRig;
    [HideInInspector] public Transform fpRoot;
    [HideInInspector] public Transform fpRig;

    [HideInInspector] public Rigidbody rigid;

    // 이동 class 분류시 이용할 script
    //[HideInInspector] public Movement3D movement3d;

    private Vector3 _moveDir;
    private Vector3 _worldMove;
    private Vector3 _rotation;

    /*
     player 상태 옵션
     */
    private bool isCurrentFp;
    public bool isMoving;
    public bool isRunning;
    public bool isGrounded;

    /*
     이동 옵션
     */
    [Range(1f, 10f), Tooltip("이동속도")]
    public float speed = 3f;
    [Range(1f, 3f), Tooltip("달리기 이동속도 증가 계수")]
    public float runningCoef = 1.5f;
    [Range(1f, 10f), Tooltip("점프 강도")]
    public float jumpForce = 5.5f;
    [Tooltip("지면으로 체크할 레이어 설정")]
    public LayerMask groundLayerMask = -1;

    private float _groundCheckRadius;

    /*
     카메라 옵션
     */
    [Tooltip("게임 시작 시 카메라")]
    public CameraType initialCamera;

    [Range(1f, 20f), Tooltip("카메라 상하좌우 회전 속도")]
    public float rotationSpeed = 10f;

    [Range(-90f, 0f), Tooltip("올려다보기 제한 각도")]
    public float lookUpDegree = -60f;

    [Range(0f, 75f), Tooltip("내려다보기 제한 각도")]
    public float lookDownDegree = 75f;

    public string email, socketId;
    public bool isOnline, isLocalPlayer;

    public Transform cameraToTarget;

    public float verticalSpeed = 3.0f;

    public float rotateSpeed = 150f;

    float h;

    float v;

    public void Set()
    {

        isLocalPlayer = PV.IsMine;
        PlayerCamera.enabled = isLocalPlayer;

        GetComponentInChildren<Text>().text = name;

        tpCamObject = tpCamera.gameObject;
        tpRig = tpCamera.transform.parent;
        tpRoot = tpRig.parent;

        fpCamObject = fpCamera.gameObject;
        fpRig = fpCamera.transform.parent;
        fpRoot = fpRig.parent;

        TryGetComponent(out rigid);
        if (rigid != null)
        {
            rigid.constraints = RigidbodyConstraints.FreezeRotation;
        }

        TryGetComponent(out CapsuleCollider cCol);
        _groundCheckRadius = cCol ? cCol.radius : 0.1f;
        animator = GetComponent<Animator>();
    }

    public void Awake()
    {
        isLocalPlayer = PV.IsMine;
    }

    void Start()
    {
        Set();
    }

    // Update is called once per frame
    void Update()
    {
        CameraViewToggle();
        CheckIsGround();
        SetValuesByKeyInput();

        Rotate();      
    }

    private void SetValuesByKeyInput()
    {
        float h = 0f, v = 0f;

        if (Input.GetKey(KeyCode.W)) v += 1.0f;
        if (Input.GetKey(KeyCode.S)) v -= 1.0f;
        if (Input.GetKey(KeyCode.A)) h -= 1.0f;
        if (Input.GetKey(KeyCode.D)) h += 1.0f;

        // Move, Rotate
        SendMoveInfo(h, v);
        if (Input.GetMouseButton(0)) // 0 represents the left mouse button
        {
            _rotation = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));

            // rotate the character using _rotation here
        }
        else
        {
            _rotation = Vector2.zero;
        }
        //_rotation = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));

        isMoving = h != 0 || v != 0;
        isRunning = Input.GetKey(KeyCode.LeftShift);



        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Wheel, 카메라 줌 관련 변수
        //_tpCameraWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");
        //_currentWheel = Mathf.Lerp(_currentWheel, _tpCameraWheelInput, CamOption.zoomAccel);
    }

    // 이동 관련 변수 초기화 
    private void SendMoveInfo(float horizontal, float vertical)
    {
        _moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        if (isCurrentFp)
        {
            _worldMove = fpRoot.TransformDirection(_moveDir);
        }
        else
        {
            _worldMove = tpRoot.TransformDirection(_moveDir);
        }

        Move();
    }

    // 토글 정상 작동 (카메라 전환만 확인)
    private void CameraViewToggle()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isCurrentFp = !isCurrentFp;
            /*
                SetActive(true) => 활성화
                SetActive(false) => 비활성화
            */
            fpCamObject.SetActive(isCurrentFp);
            tpCamObject.SetActive(!isCurrentFp);

            // TP -> FP
            if (isCurrentFp)
            {
                fpRoot.localEulerAngles = Vector3.up * tpRoot.localEulerAngles.y;
                fpRig.localEulerAngles = Vector3.right * tpRig.localEulerAngles.x;
            }
            // FP -> TP
            else
            {
                tpRoot.localEulerAngles = Vector3.up * fpRoot.localEulerAngles.y;
                tpRig.localEulerAngles = Vector3.right * fpRig.localEulerAngles.x;
            }
        }
    }

    private void Rotate()
    {
        Transform root, rig;

        // 1인칭
        if (isCurrentFp)
        {
            root = fpRoot;
            rig = fpRig;
        }
        // 3인칭
        else
        {
            root = tpRoot;
            rig = tpRig;
            //RotateFpRoot();
        }

        // 회전 ==========================================================
        float deltaCoef = Time.deltaTime * 50f;

        // 상하 : Rig 회전
        float xRotPrev = rig.localEulerAngles.x;
        float xRotNext = xRotPrev + _rotation.y
            * rotationSpeed * deltaCoef;

        if (xRotNext > 180f)
            xRotNext -= 360f;

        // 좌우 : Root 회전
        float yRotPrev = root.localEulerAngles.y;
        float yRotNext =
            yRotPrev + _rotation.x
            * rotationSpeed * deltaCoef;

        // 상하 회전 가능 여부
        bool xRotatable =
            lookUpDegree < xRotNext &&
            lookDownDegree > xRotNext;

        // Rig 상하 회전 적용
        rig.localEulerAngles = Vector3.right * (xRotatable ? xRotNext : xRotPrev);

        // Root 좌우 회전 적용
        root.localEulerAngles = Vector3.up * yRotNext;
    }

    /// <summary> 3인칭일 경우 Walker 회전 </summary>
    private void RotateFpRoot()
    {
        if (isMoving == false) return;

        Vector3 dir = tpRig.TransformDirection(_moveDir);
        float currentY = fpRoot.localEulerAngles.y;
        float nextY = Quaternion.LookRotation(dir, Vector3.up).eulerAngles.y;

        if (nextY - currentY > 180f) nextY -= 360f;
        else if (currentY - nextY > 180f) nextY += 360f;

        fpRoot.eulerAngles = Vector3.up * Mathf.Lerp(currentY, nextY, 0.1f);
    }

    /*player 이동 관련 함수*/
    private void Move()
    {
        // 실제 이동 벡터 계산
        // 1인칭
        if (isCurrentFp)
        {
            _worldMove = fpRoot.TransformDirection(_moveDir);
        }
        // 3인칭
        else
        {
            _worldMove = tpRig.TransformDirection(_moveDir);
        }

        _worldMove *= (speed) * (isRunning ? runningCoef : 1f);

        // 애니메이션 변수 설정
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

        // Y축 속도는 유지하면서 XZ평면 이동
        rigid.velocity = new Vector3(_worldMove.x, rigid.velocity.y, _worldMove.z);
    }

    private void Jump()
    {
        if (!isGrounded) return;

        // 하강 중 점프 시 속도가 합산되지 않도록 속도 초기화
        rigid.velocity = Vector3.zero;

        rigid.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private void CheckIsGround()
    {
        Vector3 ro = transform.position + Vector3.up;
        Vector3 rd = Vector3.down;
        Ray ray = new Ray(ro, rd);

        const float rayDist = 500f;
        const float threshold = 0.01f;

        bool cast =
            Physics.SphereCast(ray, _groundCheckRadius, out var hit, rayDist, groundLayerMask);

        float _distFromGround = cast ? (hit.distance - 1f + _groundCheckRadius) : float.MaxValue;
        isGrounded = _distFromGround <= _groundCheckRadius + threshold;
    }

    public void Set3DName(string name)
    {
        GetComponentInChildren<Text>().text = name;
    }


    void UpdateStatusToServer()
    {
        //hash table <key, value>
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["local_player_email"] = email;
        data["position"] = transform.position.x + ":" + transform.position.y + ":" + transform.position.z;
        data["rotation"] = transform.rotation.y.ToString();


    }

    public void SetUICamera(bool toggle)
    {
        tpCamObject.SetActive(toggle);
    }
}