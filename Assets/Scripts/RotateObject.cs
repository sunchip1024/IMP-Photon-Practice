using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // ȸ�� �ӵ� (�ʴ� 30��)

    void Update()
    {
        // �� �����Ӹ��� Y �� ȸ������ ������Ŵ
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}