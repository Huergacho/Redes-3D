using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;


public class MasterController : MonoBehaviourPunCallbacks
{
    [SerializeField] private MP_RoundManager roundManager;
    private TimerUI _timerUI;
    [SerializeField] GameObject LoCanvas;
    private UIWinManager _winManager;
    private Dictionary<string, int> HighScore;
    private static MasterController _instance;
    public static MasterController Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            InstatiateMethods();
        }
    }
    private void InstatiateMethods()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            return; 
        }
        PhotonNetwork.Instantiate(roundManager.gameObject.name, Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate(LoCanvas.name, Vector3.zero, Quaternion.identity);
        
        // _winManager = LoCanvas.GetComponentInChildren<UIWinManager>();
        // _timerUI = LoCanvas.GetComponentInChildren<TimerUI>();
        _timerUI.SetStart();
        HighScore = new Dictionary<string, int>();
        LoadScores();
    }

    void LoadScores()
    {
        var players = FindObjectsOfType<CharacterModel>();
        foreach (var item in players)
        {
            HighScore.Add(item.Name,0);
        }
    }

    public void RPCMaster(string name, params object[] p)
    {
        photonView.RPC(name, PhotonNetwork.MasterClient, p);
    }
    [PunRPC]
    public void UpdateScores(string name, int score)
    {
        HighScore[name] = score;
    }

    private string FindTopScore()
    {
        string winner = " ";
        int max = 0;
        foreach (var item in HighScore)
        {
            if (item.Value>max)
            {
                winner = item.Key;
                max = item.Value;
            }
        }
        return winner;
    }

    [PunRPC]
    public void FinishGame()
    {
        string winner = FindTopScore();
        _winManager.WinScreen(winner,HighScore[winner].ToString(),true);
    }


}