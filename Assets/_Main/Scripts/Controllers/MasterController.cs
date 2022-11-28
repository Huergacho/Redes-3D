using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Serialization;

public class MasterController : MonoBehaviourPun
{
    [SerializeField] private MP_RoundManager roundManager;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            InstatiateMethods();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void InstatiateMethods()
    {
        PhotonNetwork.Instantiate(roundManager.gameObject.name, Vector3.zero, Quaternion.identity);
    }


}