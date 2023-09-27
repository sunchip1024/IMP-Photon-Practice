using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Private Variables

    private bool isTestPanelActivated;


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

    [Tooltip("캐릭터 선택용 패널")]
    [SerializeField]
    private GameObject CharacterSelectPanel;

    [Tooltip("팀 선택용 패널")]
    [SerializeField]
    private GameObject TeamSelectPanel;

    [Tooltip("로비 캔버스")]
    [SerializeField]
    private GameObject Canvas;

    [Tooltip("포럼 캔버스")]
    [SerializeField]
    private GameObject ForumCanvas;

    [Tooltip("테스트 패널")]
    [SerializeField]
    private GameObject TestPanel;

    [Tooltip("이미지 인덱스 입력필드")]
    [SerializeField]
    private InputField ImageIndex;

    #endregion

    #region Public Variables


    #endregion

    #region Public Methods

    public void ShowLobbyCanvas()
    {
        Canvas.SetActive(true);
    }
    public void HideLobbyCanvas()
    {
        Canvas.SetActive(false);
    }
    public void ShowControlPanel()
    {
        ControlPanel.SetActive(true);
    }

    public void ShowSimplePanel()
    {
        SimplePanel.SetActive(true);
    }

    public void ShowForumCanvas()
    {
        ForumCanvas.SetActive(true);
    }
    public void ClearPanels()
    {
        SimplePanel.SetActive(false);
        WebsitePanel.SetActive(false);
        CharacterSelectPanel.SetActive(false);
        TeamSelectPanel.SetActive(false);
    }
    public void StartGame()
    {
        ControlPanel.SetActive(false);
    }

    public void EmitChangeImage()
    {
        int index = int.Parse(ImageIndex.text);
        Debug.Log(index);
        ImageManager.instance.ChangeTeam(index);
    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isTestPanelActivated = !isTestPanelActivated; // 가시성 상태를 토글
            TestPanel.SetActive(isTestPanelActivated); // 패널의 가시성을 설정된 상태로 변경
        }
    }
}
