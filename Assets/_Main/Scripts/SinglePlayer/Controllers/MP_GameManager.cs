using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MP_GameManager : SP_GameManager
{
    public static MP_GameManager MpInstance;
    public Transform spawn;
    public List<MP_CharacterModel> characters = new List<MP_CharacterModel>();
    protected override void Awake()
    {
            MpInstance = this;
            InstanceCharacter();
    }
    protected override void InstanceCharacter()
    {
        if (photonView.IsMine)
        {
            var targetObject = PhotonNetwork.Instantiate(Character.name, spawn.position, Quaternion.identity);
            var obj = targetObject.GetComponent<MP_CharacterModel>();
            characters.Add(obj);
        }
    }
}
