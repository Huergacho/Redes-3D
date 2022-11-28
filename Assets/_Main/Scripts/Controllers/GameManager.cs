using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{
    [HideInInspector]public static GameManager Instance;
    [SerializeField]private CharacterModel _character;
    public CharacterModel Character => _character;

    private  void Awake()
    {
        if (photonView.IsMine)
        {
            Instance = this;
            if (Character == null)
            {
                InstanceCharacter();
            }
        }
        else
        {
            Destroy(this);
        }

    }
    private void InstanceCharacter()
    {
        PhotonNetwork.Instantiate(_character.name, transform.position, Quaternion.identity);
    }
}