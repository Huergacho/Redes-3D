﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor;


public class MasterController : MonoBehaviourPun
{
    //[SerializeField] private MP_RoundManager roundManager;
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
            if (PhotonNetwork.IsMasterClient)
            {
                InstatiateMethods();
            }
            else
            {
                photonView.RPC(nameof(InstatiateMethods),photonView.Owner);
            }
            CreateHighScore();
        }
    }
    [PunRPC]
    private void InstatiateMethods()
    {
        _timerUI.SetStart(_timerUI.TimeLeft);
        _timerUI.photonView.RPC("SetStart", RpcTarget.OthersBuffered, _timerUI.TimeLeft);
    }

    private void CreateHighScore()
    {
        HighScore = new Dictionary<string, int>();
    }

    // void LoadScores()
    // {
    //     var players = FindObjectsOfType<CharacterModel>();
    //     foreach (var item in players)
    //     {
    //         HighScore.Add(item.Name,0);
    //     }<<
    // }

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
        string winner = "MARIO ES MEJOR QUE CRASH";
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
        // roundManager.photonView.RPC("DisableSpawner",RpcTarget.All);
        // var modelos = FindObjectsOfType<MP_CharacterView>();
        // foreach (var item in modelos)
        // {
        //     item.gameObject.SetActive(false); 
        // }

    }


}