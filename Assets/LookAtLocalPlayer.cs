using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLocalPlayer : MonoBehaviour
{
    private Transform LocalPlayerTransform; //로컬 플레이어의 transform을 가져옴
    public Vector3 targetPosition;
    public Transform ThisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            GetLocalPlayer();
        }
        catch
        {
            Debug.LogError("로컬 플레이어를 발견하지 못했습니다.");
        }
        
    }

    void GetLocalPlayer()
    {
        LocalPlayerTransform = NetworkManager.instance.LocalPlayer.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LocalPlayerTransform)
        {
            //targetPosition = LocalPlayerTransform.position;
            //targetPosition.y = LocalPlayerTransform.position.y+8;

            targetPosition = LocalPlayerTransform.position;
            targetPosition.y = transform.position.y;


            transform.LookAt(targetPosition);
        }
        else
        {
            GetLocalPlayer();
        }
    }
}
