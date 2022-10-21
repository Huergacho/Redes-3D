using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class PlayerSaver : MonoBehaviourPun
{
    // la reemplazamos con la de Player Spawner
    public List<MP_CharacterModel> characters = new List<MP_CharacterModel>();
    public static PlayerSaver Instance;
    private void Awake()
    {
        Instance = this;
    }
}