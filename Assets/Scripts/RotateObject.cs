using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // 회전 속도 (초당 30도)

    void Update()
    {
        // 매 프레임마다 Y 축 회전값을 증가시킴
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}