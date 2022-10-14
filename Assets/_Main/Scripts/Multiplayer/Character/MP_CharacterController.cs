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
        gameObject.SetActive(false);
    }

    public override void AddPoints()
    {
        if (photonView.IsMine)
        {
            base.AddPoints();
        }
    }
}
