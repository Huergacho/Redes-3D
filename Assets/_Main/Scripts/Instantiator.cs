using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Instantiator : MonoBehaviour
{
    public Transform spawn;
    private void Start()
    {
        PhotonNetwork.Instantiate("Character", spawn.position, Quaternion.identity);
    }
}
