using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Serialization;

public class MasterController : MonoBehaviourPun
{
    [SerializeField]
    private PlayerSaver saver;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(saver.gameObject.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}