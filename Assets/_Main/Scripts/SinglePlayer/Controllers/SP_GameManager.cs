using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class SP_GameManager : MonoBehaviourPun
{
    [HideInInspector]public static SP_GameManager instance;
    [SerializeField] private int enemySpawnQuantity;
    [SerializeField]protected SP_EnemySpawner _enemySpawner;
    [SerializeField]protected SP_CharacterModel _character;
    public SP_CharacterModel Character => _character;
    [SerializeField]private float roundChangeCooldown;
    private float currentChangeCooldown;
    private bool canAddRound;
    public int _roundCount { get; private set; }
    protected virtual void Awake()
    {
        instance = this;
        if (Character == null)
        {
            InstanceCharacter();
        }
    }

    protected virtual void Start()
    {
        canAddRound = true;
        currentChangeCooldown = roundChangeCooldown;
    }

    protected virtual void Update()
    {
       CountRounds();
    }

    protected virtual void CountRounds()
    {
        if (canAddRound)
        {
            currentChangeCooldown -= Time.deltaTime;
        }

        if (currentChangeCooldown <= 0 && canAddRound)
        {
            AddRound();
            currentChangeCooldown = roundChangeCooldown;
            canAddRound = false;
        }
    }

    protected virtual void InstanceCharacter()
    {
        Instantiate(Character);
    }
    private void AddRound()
    {
        _roundCount++;
        for (int i = 0; i < enemySpawnQuantity; i++)
        {
             _enemySpawner.InstatiateEnemy();
        }
    }
}