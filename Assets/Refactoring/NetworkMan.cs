using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMan : MonoBehaviourPunCallbacks
{
    private AppSettings appSettings;
    private const string APP_VERSION = "v1.0.0";
    private const string APP_ID_PUN = "9c170339-7570-40cc-a5ec-faf6fca096b7";
    private void Awake() {
        appSettings = new AppSettings();
        appSettings.AppVersion = APP_VERSION;
        appSettings.AppIdRealtime = APP_ID_PUN;
        appSettings.UseNameServer = true;
        appSettings.Port = 0;
        appSettings.Protocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
        appSettings.EnableProtocolFallback = false;

        PhotonNetwork.ConnectUsingSettings(appSettings);
    }

    #region Photon Override Method
    public override void OnConnectedToMaster() {

    }

    #endregion
}
