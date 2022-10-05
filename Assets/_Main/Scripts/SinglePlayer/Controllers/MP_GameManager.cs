using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class MP_GameManager : SP_GameManager
{
    public static MP_GameManager MPInstance;
    public Transform spawn;
    public List<MP_CharacterModel> characters = new List<MP_CharacterModel>();
    protected override void Awake()
    {
        MPInstance = this;
        InstanceCharacter();
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        if(photonView.IsMine) base.Update();
    }

    /*[PunRPC]
    protected override void CountRounds()
    {
        if (photonView.IsMine)
        {
            base.CountRounds();
            
        }
        else
        {
            photonView.RPC("CountRounds",photonView.Owner);
        }
    }*/


    protected override void InstanceCharacter()
    {
        if (photonView.IsMine)
        {
            var targetObject = PhotonNetwork.Instantiate(Character.name, spawn.position, Quaternion.identity);
            var obj = targetObject.GetComponent<MP_CharacterModel>();
            characters.Add(obj);
        }
    }
    // [PunRPC]
    // protected override void AddRound()
    // {
    //     if (!photonView.IsMine)
    //     {
    //         base.AddRound();
    //     }
    //     else
    //     {
    //         photonView.RPC("AddRound",photonView.Owner);
    //     }
    //     
    // }
}
