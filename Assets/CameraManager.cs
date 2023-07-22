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


    void Update()
    {
        FollowTarget();
    }

    public void FollowTarget()
    {
        Vector3 targetPos = localPlayerTarget.position + localPlayerTarget.forward * offset.z +
                                                                        localPlayerTarget.up * offset.y
                                                                            + localPlayerTarget.right * offset.x;

        Quaternion newRotation = Quaternion.LookRotation(localPlayerTarget.position - targetPos, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, damping * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPos, damping * Time.deltaTime);
    }

    [ContextMenu("Á¤º¸")]
    void Info()
    {
        Debug.Log(enabled);
    }
}
