using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class SP_GameManager : MonoBehaviourPun
{
    [HideInInspector]public static SP_GameManager SPInstance;
    [SerializeField] protected int enemySpawnQuantity;
    [SerializeField]protected SP_EnemySpawner _enemySpawner;
    [SerializeField]private SP_CharacterModel _character;
    public SP_CharacterModel Character => _character;
    [SerializeField]protected float roundChangeCooldown;
    private float _currentChangeCooldown;
    private bool _canAddRound;
    private int RoundCount { get; set; }
    protected virtual void Awake()
    {
        SPInstance = this;
        if (Character == null)
        {
            InstanceCharacter();
        }
    }

    protected virtual void Start()
    {
        _canAddRound = true;
        _currentChangeCooldown = roundChangeCooldown;
    }

    protected virtual void Update()
    {
        if(_canAddRound) CountRounds();
    }

    protected virtual void CountRounds()
    {
        _currentChangeCooldown -= Time.deltaTime;

        if (_currentChangeCooldown <= 0)
        {
            AddRound();
            _canAddRound = false;
            RoundCount++;

            _currentChangeCooldown = roundChangeCooldown;
        }
    }

    protected virtual void InstanceCharacter()
    {
        Instantiate(Character);
    }
    protected virtual void AddRound()
    {
        for (int i = 0; i < enemySpawnQuantity; i++)
        {
             _enemySpawner.InstatiateEnemy();
        }
    }
}