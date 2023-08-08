using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    [SerializeField] float m_zoomSpeed = 4f;
    [SerializeField] float m_zoomMax = 5f;
    [SerializeField] float m_zoomMin = 16f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraZoom();
    }

    void CameraZoom()
    {
        float t_zoomDirection = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(t_zoomDirection);

        if (transform.position.y <= m_zoomMax) return;
        if (transform.position.y >= m_zoomMin) return;

        transform.position += transform.forward * t_zoomDirection * m_zoomSpeed;
    }

}
