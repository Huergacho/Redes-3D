using System;
using Photon.Pun;
using UnityEngine;

//public class LifeController: MonoBehaviourPun
public class MP_LifeController: SP_LifeController
{
    [PunRPC]
    public override void TakeDamage(float damage)
    {

        if (photonView.IsMine)
        {
            base.TakeDamage(damage);
        }
        else
        {
            photonView.RPC("TakeDamage",photonView.Owner,damage);
        }
    }
}