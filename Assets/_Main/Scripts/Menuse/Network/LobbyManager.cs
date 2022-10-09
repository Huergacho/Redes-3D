using System;
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
    private List<RoomItem> roomItemsList;
    public TMP_InputField roomInputField;

    #endregion
    
    public float timeBetweenUpdates=1.5f;
    private float nextUpdateTime; 

    public Transform contentObject;

    public GameObject playButton; 
    
    #region Players Variables

    public List<PlayerItem> playerItemsList;
    public PlayerItem playerItemPref;
    public Transform playerItemParent;
    

    #endregion
    
    void Start()
    {
        
        roomItemsList = new List<RoomItem>();
        playerItemsList = new List<PlayerItem>();
        
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
        nextUpdateTime = 0f;
        
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        if (roomInputField.text.Length>=1 )
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){ MaxPlayers = 2,BroadcastPropsChangeToAll = true});
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

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
        
    }

    void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }   
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItem, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
            
        }
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
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

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom==null) return;
        
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPref, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            playerItemsList.Add(newPlayerItem);
        }
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
