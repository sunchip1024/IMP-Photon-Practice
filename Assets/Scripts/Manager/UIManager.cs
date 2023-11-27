using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Private Variables

    private bool isTestPanelActivated;


    [Tooltip("���� �������ͽ� �����ִ� �г�")]
    [SerializeField]
    private GameObject StatusPanel;


    [Tooltip("���ۿ� �г�")]
    [SerializeField]
    private GameObject ControlPanel;

    [Tooltip("����ȭ�� �г�")]
    [SerializeField]
    private GameObject SimplePanel;


    [Tooltip("������Ʈ�� �α��� ȭ���� �ӽ÷� ������ �г�")]
    [SerializeField]
    private GameObject WebsitePanel;

    //[Tooltip("ĳ���� ���ÿ� �г�")]
    //[SerializeField]
    //private GameObject CharacterSelectPanel;

    //[Tooltip("�� ���ÿ� �г�")]
    //[SerializeField]
    //private GameObject TeamSelectPanel;

    [Tooltip("�κ� ĵ����")]
    [SerializeField]
    private GameObject Canvas;

    [Tooltip("���� ĵ����")]
    [SerializeField]
    private GameObject ForumCanvas;

    [Tooltip("�׽�Ʈ �г�")]
    [SerializeField]
    private GameObject TestPanel;

    [Tooltip("�̹��� �ε��� �Է��ʵ�")]
    [SerializeField]
    private InputField ImageIndex;


    #endregion

    #region Public Variables
    public static UIManager Instance;

    [Tooltip("�ε� �̹���")]
    public Image LoadingImage;

    [Tooltip("������ �ؽ�Ʈ")]
    public Text FrameText;

    [Header("�̹��� ���� �߰��� �ӽ� �г�")]
    public GameObject NicknamePanel;

    [Tooltip("�� ���ÿ� �г�")]
    public GameObject TeamSelectPanel;

    [Tooltip("ĳ���� ���ÿ� �г�")]
    public GameObject AvatarSelectPanel;

    [Tooltip("���ο� �г�")]
    public GameObject ZoomInPanel;

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
        NicknamePanel.SetActive(false);
        SimplePanel.SetActive(false);
        WebsitePanel.SetActive(false);
        AvatarSelectPanel.SetActive(false);
        TeamSelectPanel.SetActive(false);
    }
    public void StartGame()
    {
        ControlPanel.SetActive(false);
    }

    public void EmitChangeTeam()
    {
        int index = int.Parse(ImageIndex.text);
        NetworkManager.instance.ChangeTeam(index);
    }

    public void ToggleLoading(bool toggle)
    {
        LoadingImage.enabled = toggle;
    }

    public int GetCurrentFrame()
    {
        return Time.frameCount;
    }

    public void NextPanel(int index)
    {
        ClearPanels();
        switch (index)
        {
            case 0:
                TeamSelectPanel.SetActive(true);
                break;
            case 1:
                AvatarSelectPanel.SetActive(true);
                break;
        }
    }

    public void ShowZoomInCanvas()
    {
        ZoomInPanel.SetActive(true);
    }

    public void HideZoomInCanvas()
    {
        ZoomInPanel.SetActive(false);
    }

    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        Instance= this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isTestPanelActivated = !isTestPanelActivated; // ���ü� ���¸� ���
            TestPanel.SetActive(isTestPanelActivated); // �г��� ���ü��� ������ ���·� ����
        }
        FrameText.text = $"Frame : { 1/Time.deltaTime}";
    }
}
