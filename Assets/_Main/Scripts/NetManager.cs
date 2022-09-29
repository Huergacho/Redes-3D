using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

class NetManager : MonoBehaviourPunCallbacks
{
    public Button button;
    public Text status;
    public string roomName;
    [SerializeField] private string levelToLoad;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        button.interactable = false;
        status.text = "Conenecting to Master...";

    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        status.text = "Conenecting to Lobby...";

    }
    public override void OnJoinedLobby()
    {
        button.interactable = true;


        status.text = "Connected";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        status.text = "Connection Failed ;(";

    }
    public override void OnLeftLobby()
    {
        status.text = "Lobby Failed ;(";
    }
    public void Connect()
    {
        RoomOptions roomOpt = new RoomOptions();
        roomOpt.MaxPlayers = 4;
        roomOpt.IsOpen = true;
        roomOpt.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOpt, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(levelToLoad);

    }
}
