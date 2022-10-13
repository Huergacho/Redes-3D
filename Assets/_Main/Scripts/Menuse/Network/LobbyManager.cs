
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject lobbyPanel;

    #region Room Variables

    public GameObject roomPanel;
    public TMP_Text roomName;
    public RoomItem roomItem;
    private List<RoomItem> _roomItemsList;
    public TMP_InputField roomInputField;
    public Transform contentObject;

    #endregion

    [SerializeField] private int maxPlayerNumber = 2;
    public float timeBetweenUpdates = 1.5f;
    private float _nextUpdateTime;


    public GameObject playButton;

    #region Players Variables

    public List<PlayerItem> playerItemsList;
    public PlayerItem playerItemPref;
    public Transform playerItemParent;

    #endregion

    void Start()
    {
        _roomItemsList = new List<RoomItem>();
        playerItemsList = new List<PlayerItem>();

        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        _nextUpdateTime = 0f;

        PhotonNetwork.JoinLobby();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    #region Photon Overrides
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= _nextUpdateTime)
        {
            UpdateRoomList(roomList);
            _nextUpdateTime = Time.time + timeBetweenUpdates;
        }
    }
    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    #endregion

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in _roomItemsList)
        {
            Destroy(item.gameObject);
        }

        _roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItem, contentObject);
            newRoom.SetRoomName(room.Name);
            _roomItemsList.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }

        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null) return;

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPref, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
           
            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            } 
            playerItemsList.Add(newPlayerItem);
        }
    }

    #region Buttons events
    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text,
                new RoomOptions() { MaxPlayers = (byte)maxPlayerNumber, BroadcastPropsChangeToAll = true });
        }
    }
    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("House");
    }
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion
}