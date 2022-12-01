using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;


public class MasterController : MonoBehaviourPun
{
    [SerializeField] private MP_RoundManager roundManager;
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private UIWinManager _winManager;
    private Dictionary<string, int> HighScore;
    public static MasterController _instance;
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
        // if (!PhotonNetwork.IsMasterClient)
        // {
        //     Destroy(this);
        //     return;
        // }
        PhotonNetwork.Instantiate(roundManager.gameObject.name, Vector3.zero, Quaternion.identity);

        _timerUI.photonView.RPC("SetStart",RpcTarget.Others);
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

    [PunRPC]
    public void UpdateScores(string name, int score)
    {
        if (!HighScore.ContainsKey(name))
        {
            HighScore.Add(name,score);
        }
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
        print(winner);
        _winManager.WinScreen(winner,HighScore[winner].ToString(),true);
    }


}