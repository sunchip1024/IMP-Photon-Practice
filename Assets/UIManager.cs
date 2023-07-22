using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Private Variables
    [Tooltip("각종 스테이터스 보여주는 패널")]
    [SerializeField]
    private GameObject StatusPanel;



    [Tooltip("조작용 패널")]
    [SerializeField]
    private GameObject ControlPanel;

    [Tooltip("전체 캔버스")]
    [SerializeField]
    private GameObject Canvas;

    #endregion

    #region Public Methods

    public void ShowCanvas()
    {
        Canvas.SetActive(true);
    }
    public void ShowControlPanel()
    {
        ControlPanel.SetActive(true);
    }
    public void StartGame()
    {
        ControlPanel.SetActive(false);
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
