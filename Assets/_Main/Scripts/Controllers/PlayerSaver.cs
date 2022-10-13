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

    private void Start()
    {
        GetPlayersInScene();
    }

    private void GetPlayersInScene()
    {
        foreach (var character in FindObjectsOfType<MP_CharacterModel>())
        {
            characters.Add(character);
        }
        
    }
}