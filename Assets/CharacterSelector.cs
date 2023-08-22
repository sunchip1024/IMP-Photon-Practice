using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public GameObject button1, button2;

    public void ClearButtons()
    {
        button1.GetComponent<Outline>().enabled = false;
        button2.GetComponent<Outline>().enabled = false;
    }

    public void SelectButton(GameObject button)
    {
        ClearButtons();
        button.GetComponent<Outline>().enabled = true;

        if(button == button1)
        {
            NetworkManager.instance.SetPlayerPrefab("PlayerPrefab");
        }
        else
        {
            NetworkManager.instance.SetPlayerPrefab("Cube");
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
