using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLACE
{
    HONGBO,
    MEETING
}

public class PortalManager : MonoBehaviour
{
    public PLACE PlaceToMove;

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "LocalPlayer")
        {
            switch (PlaceToMove)
            {
                case PLACE.HONGBO:
                    Debug.Log("ȫ�� �������� �̵�");
                    ModelManager.instance.SetHongbo();
                    //NetworkManager.instance.Join("Hongbo");
                    break;
                case PLACE.MEETING:
                    Debug.Log("���� �������� �̵�");
                    ModelManager.instance.SetMeeting();
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
