using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(CharacterModel),typeof(Weapon),typeof(MP_LifeController))]
public class CharacterController : SP_CharacterController
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
