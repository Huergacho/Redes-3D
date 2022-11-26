using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviourPun
{
    [HideInInspector]public static GameManager Instance;
    [SerializeField]private CharacterModel _character;
    public CharacterModel Character => _character;
    protected virtual void Awake()
    {
        Instance = this;
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