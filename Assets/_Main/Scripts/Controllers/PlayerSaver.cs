using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class PlayerSaver : MonoBehaviourPun
{
    public List<MP_CharacterModel> characters = new List<MP_CharacterModel>();
    public static PlayerSaver Instance;
    private void Awake()
    {
        Instance = this;
    }

    // private void Start()
    // {
    //     foreach (var player in PhotonNetwork.PlayerList)
    //     {
    //         GetPlayersInScene();
    //     }
    // }
    //
    // private void GetPlayersInScene()
    // {
    //
    //     MP_CharacterModel player = FindObjectOfType<MP_CharacterModel>();
    //     characters.Add(player);
    // }
}