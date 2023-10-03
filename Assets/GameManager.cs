using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0; // 에디터에서 VSync를 비활성화합니다.
        Application.targetFrameRate = 60; // 원하는 프레임 속도를 설정합니다.
#endif
        //NetworkManager.instance.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
