using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Manage Main Camera
/// </summary>

public class CameraManager : MonoBehaviour
{
    public Transform localPlayerTarget;
    public float damping = 5f;
    public Vector3 offset;

    [SerializeField] float m_zoomSpeed = 4f;
    [SerializeField] float m_zoomMax = 5f;
    [SerializeField] float m_zoomMin = 16f;

    private float currentZoom = 0f; // Store the current zoom level
    private Vector2 _rotation = Vector2.zero;

    void Update()
    {
        CameraZoom();
        FollowTarget();
        CameraMove();
    }

    void FollowTarget()
    {
        Vector3 desiredPosition = localPlayerTarget.position + offset - transform.forward * (offset.z + currentZoom);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, m_zoomMax, m_zoomMin);

        Quaternion newRotation = Quaternion.LookRotation(localPlayerTarget.position - desiredPosition, Vector3.up);


        transform.position = Vector3.Lerp(transform.position, desiredPosition, damping * Time.deltaTime);

        // Apply rotation to character here using _rotation

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, damping * Time.deltaTime);
    }

    void CameraZoom()
    {
        float t_zoomDirection = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= t_zoomDirection * m_zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, m_zoomMax, m_zoomMin);
    }

    void CameraMove()
    {
        if(Input.GetMouseButton(0))
        {
            float t_posX = Input.GetAxis("Mouse X");
            float t_posZ = Input.GetAxis("Mouse Y");
            transform.position += new Vector3(t_posX, 0, t_posZ);
        }
    }


}
