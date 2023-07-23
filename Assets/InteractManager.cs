using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance;
    public PlayerManager player;
    public float Dist;
    public bool isReady, isInteracted;
    public Transform team1, team2;
    public Camera PlayerCamera, UICamera;

    public GameObject CalendarCanvas, week, weekPrefab;

    public GameObject UIManager;

    Renderer ObjectColor;

    public List<Transform> ObjectList;

    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        ObjectColor = gameObject.GetComponent<Renderer>();
        team1.GetComponent<Renderer>().material.color = Color.black;
        team2.GetComponent<Renderer>().material.color = Color.black;
        //player = GameObject.FindWithTag("LocalPlayer");
        //pos = GetComponent<Transform>();
        //Debug.Log("camera number " + player.GetComponentsInChildren<Camera>().Length);
        //PlayerCamera = player.GetComponentsInChildren<Camera>()[0];
        //week = Instantiate(weekPrefab);
        isReady = false;
    }

    void CheckDistance(Transform pos)
    {
        Dist = Vector3.Distance(player.transform.position, pos.position);

        if (Dist < 4) isReady = true;
        else isReady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            foreach(Transform obj in ObjectList)
            {
                CheckDistance(obj);
                Paint(obj);
            }
            //CheckDistance(team1);
            //CheckDistance(team2);
            
        }
        

    }

    void Paint(Transform obj)
    {
        if (isReady)
        {
            obj.GetComponent<Renderer>().material.color = Color.green;
            Interact();
        }
        else
        {
            obj.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracted = !isInteracted;
            NetworkManager.instance.LeaveRoom();
        }
    }
}
