using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;

    public Transform objectTofollow;
    public float followSpeed = 10f;
    public float sensitivity = 400f;
    public float clampAngle = 70f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;

    [SerializeField] float m_zoomSpeed = 4f;
    [SerializeField] float m_zoomMax = 2f;
    [SerializeField] float m_zoomMin = 16f;
    [SerializeField] float currentZoom = 0f;

    //currentZoom과 maxDistance의 비율
    [SerializeField] float ScrollSensitivty;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //rotX = transform.localRotation.eulerAngles.x;
        //rotY = transform.localRotation.eulerAngles.y;

        //Debug.Log(realCamera.localPosition);
        //dirNormalized = realCamera.localPosition.normalized;
        //finalDistance = realCamera.localPosition.magnitude;

        //currentZoom = 5f;

        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void Set()
    {

        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //Debug.Log(realCamera.localPosition);
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude;

        currentZoom = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (objectTofollow == null) return;
        if(Input.GetMouseButton(0))
        {
            rotX -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
            transform.rotation = rot;
        }

        CameraZoom();

    }

    private void LateUpdate()
    {
        if (objectTofollow == null) return;
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);
        //finalDistance = maxDistance;

        RaycastHit hit;
        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        //Debug.Log($"{realCamera.localPosition}, {dirNormalized * finalDistance}");
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
        //realCamera.localPosition = dirNormalized * finalDistance;
    }

    void CameraZoom()
    {
        float t_zoomDirection = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= t_zoomDirection * m_zoomSpeed;
        if (currentZoom < m_zoomMax) Debug.Log("1인칭 모드");
        currentZoom = Mathf.Clamp(currentZoom, m_zoomMax, m_zoomMin);
        maxDistance = currentZoom * ScrollSensitivty;
    }

}
