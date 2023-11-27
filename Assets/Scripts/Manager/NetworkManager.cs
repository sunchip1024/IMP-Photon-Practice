using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text StatusText, RoomText;
    public InputField RoomInput, NickNameInput, TeamInput;
    public GameObject UIManager;
    public static NetworkManager instance;
    public GameObject LocalPlayerPrefab;
    public GameObject LocalPlayer;


    //�����ϰ��� �ϴ� �� �ε��� ����
    public int TeamIndex;

    public PhotonView Photonview;

    public string PlayerPrefabName = "PlayerPrefab";

    public string RoomToMove;
    public Text PhotonStatusText;

    public string nickName;

    void Awake()
    {
        instance = this;

        //Screen.SetResolution(1920, 1080, false);
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString();
        if(!PhotonNetwork.InRoom)
        {
            PhotonStatusText.text =
                $"���濡 ������ �ο� : {PhotonNetwork.CountOfPlayers}\n" +
                $"��� �濡 �ִ� �ο� : {PhotonNetwork.CountOfPlayersInRooms}\n";
            return;
        }
        else
        {
            PhotonStatusText.text =
                $"���� �� �̸� : {PhotonNetwork.CurrentRoom.Name}\n" +
                $"���� �濡 �ִ� �ο� : {PhotonNetwork.CurrentRoom.PlayerCount}\n";

        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ���� �Ϸ�");
        UIManager.GetComponent<UIManager>().ShowSimplePanel();
        PhotonNetwork.JoinLobby();

    }

    public void Join()
    {
        string roomName = $"{TeamIndex}";
        Debug.Log("�ε� �̹���");
        UIManager.GetComponent<UIManager>().ToggleLoading(true);
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 7 }, null);
    }
    
    public void GeneratePlayer(string name)
    {
        
        player newPlayer;
        LocalPlayer = PhotonNetwork.Instantiate(PlayerPrefabName,
                new Vector3(0, 5, 0), Quaternion.identity);
        newPlayer = LocalPlayer.GetComponent<player>();

        newPlayer.playername = name;
        ModelManager.instance.SetHongbo();
        UIManager.GetComponent<UIManager>().ToggleLoading(false);

    }


    public void DisConnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause)
    {
        if (cause == DisconnectCause.MaxCcuReached)
        {
            PopupManager.instance.EmitPopup("�ִ� CCU�� �ʰ��߽��ϴ�!");
            Debug.LogError("�ִ� CCU�� �ʰ��߽��ϴ�!");
            return;
        }
        Debug.LogFormat("���� ����, ���� : {0}", cause);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� �Ϸ�");
        if (RoomToMove != string.Empty)
        {
            JoinOrCreateRoom(RoomToMove);
            RoomToMove = null;
        }
    }

    //�� ����� �� ����
    public void CreateRoom() => PhotonNetwork.CreateRoom(RoomInput.text, new RoomOptions { MaxPlayers = 5 });
    public void JoinRoom(string RoomName) => PhotonNetwork.JoinRoom(RoomName);
    public void JoinOrCreateRoom(string RoomName) => PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions { MaxPlayers = 5 }, null);
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public void LeaveRoom(string RoomName)
    {
        PhotonNetwork.LeaveRoom();
        RoomToMove = RoomName;
        Debug.Log("room to move : " + RoomName);
    }

    public override void OnLeftRoom()
    {
        RoomText.text = "Not in the room";
        Debug.Log(PhotonNetwork.NetworkClientState.ToString());
    }

    public override void OnJoinedRoom()
    {
        Debug.LogFormat("�� ���� �Ϸ� : {0}", PhotonNetwork.CurrentRoom);
        RoomText.text = PhotonNetwork.CurrentRoom.Name;
        UIManager.GetComponent<UIManager>().HideLobbyCanvas();
        UIManager.GetComponent<UIManager>().ShowForumCanvas();
        GeneratePlayer(nickName);
        //try
        //{
        //    SetTeamIndex(int.Parse(TeamInput.text));
        //}
        //catch
        //{
        //    TeamIndex = 0;
        //}

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        PopupManager.instance.EmitPopup($"�� ���� ����, ���� : {message}");
        Debug.LogErrorFormat("�� ����� ����, ���� : {0} : {1}", returnCode, message);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PopupManager.instance.EmitPopup($"�� ���� ����, ���� : {message}");
        Debug.LogErrorFormat("�� ���� ����, ���� : {0} : {1}", returnCode, message);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("�� �������� ����");
    }

    [ContextMenu("����")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("���� �� �̸� : " + PhotonNetwork.CurrentRoom.Name);
            print("���� �� �ο��� : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("���� �� �ִ��ο��� : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "�濡 �ִ� �÷��̾� ��� : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            }
            print(playerStr);
        }
        else
        {
            print("������ �ο� �� : " + PhotonNetwork.CountOfPlayers);
            print("�� ���� : " + PhotonNetwork.CountOfRooms);
            print("��� �濡 �ִ� �ο� �� : " + PhotonNetwork.CountOfPlayersInRooms);
            print("�κ� �ִ���? : " + PhotonNetwork.InLobby);
            print("����ƴ���? : " + PhotonNetwork.IsConnected);
        }
    }

    public void SetPlayerPrefab(string PrefabName)
    {
        PlayerPrefabName = PrefabName;
    }

    [ContextMenu("����")]
    void Status()
    {
        Debug.Log(PhotonNetwork.NetworkClientState);
    }

    public void ChangeTeam(int index)
    {
        Debug.Log($"index {index}�� ������");
        TeamIndex = index;
        UIManager.GetComponent<UIManager>().ToggleLoading(true);
        LeaveRoom(index.ToString());
        ImageManager.instance.ChangeTeam(index);
    }

    public void SetTeamIndex(int index)
    {
        //Debug.Log($"Teamindex : {index}");
        TeamIndex = index;
    }
    
}
