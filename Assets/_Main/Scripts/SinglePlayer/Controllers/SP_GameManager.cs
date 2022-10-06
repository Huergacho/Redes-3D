using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class SP_GameManager : MonoBehaviourPun
{
    [HideInInspector]public static SP_GameManager SPInstance;
    [SerializeField]private SP_CharacterModel _character;
    public SP_CharacterModel Character => _character;
    protected virtual void Awake()
    {
        SPInstance = this;
        if (Character == null)
        {
            InstanceCharacter();
        }
    }
    protected virtual void InstanceCharacter()
    {
        Instantiate(Character);
    }
}