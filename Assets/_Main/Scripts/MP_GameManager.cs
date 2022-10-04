using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class MP_GameManager : SP_GameManager
{
    public Transform spawn;
    [SerializeField] private string characterName;


    private void Start()
    {

    }

    protected override void InstanceCharacter()
    {
        var targetObject = PhotonNetwork.Instantiate(characterName, spawn.position, Quaternion.identity);
        _character = targetObject.GetComponent<MP_CharacterModel>();
    }
}
