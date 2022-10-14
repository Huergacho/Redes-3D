﻿using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Serialization;

public class MasterController : MonoBehaviourPun
{
    [SerializeField] private PlayerSaver saver;
    [SerializeField] private MP_GenericPool pool;
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
        Instantiate(saver.gameObject, Vector3.zero, Quaternion.identity);
        Instantiate(pool.gameObject, Vector3.zero, Quaternion.identity);
        Instantiate(roundManager, Vector3.zero, Quaternion.identity);
    }

}