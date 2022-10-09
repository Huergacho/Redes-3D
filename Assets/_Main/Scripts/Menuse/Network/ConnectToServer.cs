using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_InputField userInput;
    public TMP_Text buttonText;


    public void OnClickConnect()
    {
        if (userInput.text.Length>=1)
        {
            PhotonNetwork.NickName = userInput.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }


}
