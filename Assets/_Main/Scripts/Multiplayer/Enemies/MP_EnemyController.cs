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
        GetPlayersInScene();
        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(_enemyStats.maxLife);
        _enemyModel = GetComponent<MP_EnemyModel>();

    }

    protected override void Start()
    {
        if (targetModel != null)
        {
            InitializeOBS();
        }
        _enemyModel.AssignStats(_enemyStats);
        _enemyModel.Subscribe(this);
        _mpLifeController.OnDie += OnDieCommand;
        _mpLifeController.OnTakeHit += targetModel.GetComponent<MP_CharacterController>().AddPoints;
        InitDecisionTree();
        InitFsm();
    }
    private void GetPlayersInScene()
    {
    
        MP_CharacterModel[] players = FindObjectsOfType<MP_CharacterModel>();
        int index = Random.Range(0, players.Length);
        targetModel = players[index];
    }
    private void OnDieCommand()
    {
        // Ver si disparamos por model o view por animación
        PhotonNetwork.Destroy(gameObject);
    }

    public override void OnObjectSpawn()
    {
        _mpLifeController.AssignLife(_enemyStats.maxLife);
    }

    [PunRPC]
    public void AssingPlayerRPC(MP_CharacterModel target)
    {
        target = (MP_CharacterModel)targetModel;
    }
}