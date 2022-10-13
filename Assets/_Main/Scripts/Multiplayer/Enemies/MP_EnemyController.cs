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
        _mpLifeController.OnTakeHit += targetModel.GetComponent<MP_CharacterController>().AddPoints;
        InitDecisionTree();
        InitFsm();
    }

    protected override void AssignTarget()
    {
        int playerCount = PhotonNetwork.CountOfPlayers;
        int randomPlayerIndex = Random.Range(0, playerCount -1);
        var newPlayer = PlayerSaver.Instance.characters[randomPlayerIndex];
        if (newPlayer != null)
            targetModel = newPlayer;
        else
        {
            print("NO TA PLAYER");
        }
    }


    private void OnDieCommand()
    {
        // Ver si disparamos por model o view por animación
        gameObject.SetActive(false);
    }

    public override void OnObjectSpawn()
    {
        _mpLifeController.AssignLife(_enemyStats.maxLife);
    }
}