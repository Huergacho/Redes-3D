using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
public class PlayerSaver : MonoBehaviourPun
{
    public List<MP_CharacterModel> characters = new List<MP_CharacterModel>();
    public static PlayerSaver Instance;
    private void Awake()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
    }
    

}