using Photon.Pun;
using UnityEngine.UI;


public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public InputField createRoom;
    public InputField joinRoom;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoom.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameLevel");
    }
}
