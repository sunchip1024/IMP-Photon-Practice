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

    [Tooltip("간소화된 패널")]
    [SerializeField]
    private GameObject SimplePanel;


    [Tooltip("웹사이트의 로그인 화면을 임시로 구현한 패널")]
    [SerializeField]
    private GameObject WebsitePanel;

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

    public void ShowSimplePanel()
    {
        SimplePanel.SetActive(true);
    }
    public void HideSimplePanel()
    {
        SimplePanel.SetActive(false);
        WebsitePanel.SetActive(false);
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
