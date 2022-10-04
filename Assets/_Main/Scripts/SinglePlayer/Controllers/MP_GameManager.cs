using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class MP_GameManager : SP_GameManager
{
    public Transform spawn;

    protected override void Awake()
    {
        instance = this;
        InstanceCharacter();
    }

    protected override void CountRounds()
    {
        if (photonView.IsMine)
        {
        base.CountRounds();
            
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void InstanceCharacter()
    {
        print(_character.name);
        var targetObject = PhotonNetwork.Instantiate(_character.name, spawn.position, Quaternion.identity);
        _character = targetObject.GetComponent<MP_CharacterModel>();
    }
}
