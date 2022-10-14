using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(MP_CharacterModel),typeof(MP_Weapon),typeof(MP_LifeController))]
public class MP_CharacterController : SP_CharacterController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        // if (PhotonNetwork.IsMasterClient)
        // {
        //     PlayerSaver.Instance.AddNewPlayer((MP_CharacterModel)Model);
        // }
        // else
        // {
        //     photonView.RPC(nameof(PlayerSaver.Instance.ExternalAddPlayer),PhotonNetwork.MasterClient,(MP_CharacterModel)Model);
        // }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (photonView.IsMine)
        {
            base.Update();
        }
    }

    protected override void Die()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public override void AddPoints()
    {
        if (photonView.IsMine)
        {
            base.AddPoints();
        }
    }
}
