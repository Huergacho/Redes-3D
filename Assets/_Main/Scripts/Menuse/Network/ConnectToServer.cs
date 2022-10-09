using System;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_InputField userInput;
    public TMP_Text buttonText;
    public Button connectButton;
    public Sprite _onPointer;
    private Sprite _defaultSprite;

    private void Awake()
    {
        _defaultSprite = connectButton.image.sprite;
    }

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

    public void OnMouseEnter()
    {
        connectButton.image.sprite = _onPointer;
    }

    public void OnMouseExit()
    {
        connectButton.image.sprite = _defaultSprite;
    }
}
