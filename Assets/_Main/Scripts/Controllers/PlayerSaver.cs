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
        if(!PhotonNetwork.IsMasterClient)
        {
            Destroy(this);
        }
        else
        {
            Instance = SetInstance();
        }
        
    }

    [PunRPC]
    public void GetInstance(Player client)
    {
        photonView.RPC(nameof(SetInstance),client);
    }

    [PunRPC]
    public PlayerSaver SetInstance()
    {
        if(Instance == null)
            Instance = this;
        return Instance;
    }

    [PunRPC]
    public void AddNewPlayer(MP_CharacterModel data)
    {
        characters.Add(data);
    }
}