using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkdistance : MonoBehaviour
{
    public GameObject player;
    public float Dist;
    public bool isReady, isInteracted;
    public Transform pos;
    public Camera PlayerCamera, UICamera;

    public GameObject CalendarCanvas, week, weekPrefab;

    public GameObject UIManager;

    Renderer ObjectColor;


    // Start is called before the first frame update
    void Start()
    {
        ObjectColor = gameObject.GetComponent<Renderer>();
        Debug.Log("check distance on!");
        //player = GameObject.FindWithTag("LocalPlayer");
        pos = GetComponent<Transform>();
        //Debug.Log("camera number " + player.GetComponentsInChildren<Camera>().Length);
        //PlayerCamera = player.GetComponentsInChildren<Camera>()[0];
        //week = Instantiate(weekPrefab);
        isReady = false;
    }

    void CheckDistance()
    {
        player = GameObject.FindWithTag("LocalPlayer");
        Dist = Vector3.Distance(player.transform.position, pos.position);
        if (Dist < 10) isReady = true;
        else isReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        Set();

    }

    void Set()
    {
        if (isReady)
        {
            ObjectColor.material.color = Color.blue;
            Interact();
        }
        else
        {
            ObjectColor.material.color = Color.black;
        }
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracted = !isInteracted;
            Debug.Log("상호작용했음");
        }
    }
}
