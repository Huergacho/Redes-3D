using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MP_GameManager : SP_GameManager
{
    public static MP_GameManager MpInstance;
    public Transform spawn;
    // protected override void Awake()
    // {
    //     if (PhotonNetwork.IsMasterClient)
    //     {
    //         MpInstance = this;
    //         InstanceCharacter();
    //     }
    //     else
    //     {
    //         photonView.RPC("InstanceCharacter",photonView.Owner);
    //     }
    // }

    protected void Start()
    {

    }
// [PunRPC]
//     protected override void InstanceCharacter()
//     {
//         var targetObject = PhotonNetwork.Instantiate(Character.name, spawn.position, Quaternion.identity);
//             var obj = targetObject.GetComponent<MP_CharacterModel>();
//             PlayerSaver.Instance.characters.Add(obj);
//     }
}
