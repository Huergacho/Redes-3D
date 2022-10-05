using Photon.Pun;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MP_LifeController))]
public class MP_EnemyController : SP_EnemyController
{
    private MP_LifeController _mpLifeController;


    private void Awake()
    {
        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(_enemyStats.maxLife);
        _enemyModel = GetComponent<MP_EnemyModel>();

    }

    protected override void Start()
    {
        AssignTarget();
        if (targetModel != null)
        {
            InitializeOBS();
        }
        _enemyModel.AssignStats(_enemyStats);
        _enemyModel.Subscribe(this);
        _mpLifeController.OnDie += OnDieCommand;
        InitDecisionTree();
        InitFsm();
    }

    protected override void AssignTarget()
    {
        int playerCount = PhotonNetwork.CountOfPlayers;
        int randomPlayerIndex = Random.Range(0, playerCount);
        targetModel = MP_GameManager.MPInstance.characters[randomPlayerIndex];
    }


    private void OnDieCommand()
    {
        // Ver si disparamos por model o view por animación
        gameObject.SetActive(false);
    }

    
}