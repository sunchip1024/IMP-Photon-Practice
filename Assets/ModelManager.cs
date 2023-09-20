using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    public static ModelManager instance;
    public GameObject nature, hongbo, meeting;
    public List<GameObject> Portals;
    // Start is called before the first frame update

    public void ClearPortals()
    {
        foreach(GameObject portal in Portals)
        {
            portal.SetActive(false);
        }
    }

    public void SetHongbo()
    {
        ClearPortals();
        //nature.SetActive(true);
        hongbo.SetActive(true);
        Debug.Log(NetworkManager.instance.TeamIndex + "번 이미지로 세팅");
        ImageManager.instance.ChangeImage(NetworkManager.instance.TeamIndex);
    }

    public void SetMeeting()
    {
        ClearPortals();
        nature.SetActive(true);
        meeting.SetActive(true);
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
