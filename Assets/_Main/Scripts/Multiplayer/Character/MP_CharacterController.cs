using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(MP_CharacterModel),typeof(MP_Weapon),typeof(MP_LifeController))]
public class MP_CharacterController : SP_CharacterController
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this); 
        }
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
        PhotonNetwork.Destroy(gameObject);
    }
}
