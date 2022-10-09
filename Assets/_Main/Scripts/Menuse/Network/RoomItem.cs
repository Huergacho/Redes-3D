using Photon.Pun;
using UnityEngine;
using TMPro;


public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    private LobbyManager manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string roomNameParam)
    {
        roomName.text = roomNameParam;
    }

    public void OnClickItem()
    {
        manager.JoinRoom(roomName.text);
    }


    
}
