using UnityEngine;
using TMPro;


public class RoomItem : MonoBehaviour
{
    [SerializeField]private TMP_Text roomName;
    private LobbyManager _lobby;



    private void Start()
    {
        _lobby = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string roomNameParam)
    {
        roomName.text = roomNameParam;
    }

    public void OnClickItem()
    {
        _lobby.JoinRoom(roomName.text);
    }


    
}
