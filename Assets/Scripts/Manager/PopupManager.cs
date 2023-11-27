using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject UIManager;

    public static PopupManager instance;
    public GameObject PopupPanel;
    public Text PopupMessage;

    private void Awake()
    {
        instance = this;
    }

    public void EmitPopup(string message)
    {
        UIManager.GetComponent<UIManager>().ToggleLoading(false);
        PopupPanel.SetActive(true);
        PopupMessage.text = message;
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
