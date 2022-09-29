using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Instantiator : MonoBehaviour
{
    public Transform spawn;
    [SerializeField] private string characterName;
    private void Start()
    {
        PhotonNetwork.Instantiate(characterName, spawn.position, Quaternion.identity);
    }
}
