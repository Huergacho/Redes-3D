using Photon.Pun;
using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MP_LifeController))]
public class MP_EnemyController : SP_EnemyController
{
    private MP_LifeController _mpLifeController;
    private MP_EnemyView _enemyView;

    protected override void Awake()
    {
   
        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(_enemyStats.maxLife);
        _enemyModel = GetComponent<MP_EnemyModel>();
        _enemyView = GetComponent<MP_EnemyView>();
    }

    protected override void Start()
    {
        GetPlayersInScene();
        if (targetModel != null)
        {
            InitializeOBS();
        }
        _enemyModel.AssignStats(_enemyStats);
        _enemyModel.Subscribe(this);
        _enemyView.Subscribe(this);
        _mpLifeController.OnDie += OnDieCommand;
        InitDecisionTree();
        InitFsm();
    }
    private void GetPlayersInScene()
    {
        List<MP_CharacterModel> players = new List<MP_CharacterModel>();
        foreach (var posPayer in FindObjectsOfType<MP_CharacterModel>())
        {
            if (posPayer.isActiveAndEnabled)
            {
                players.Add(posPayer);
            }
        }

        //MP_CharacterModel[] players = new MP_CharacterModel[];
        int index = Random.Range(0, players.Count-1);
        targetModel = players[index];
        if (!targetModel) this.enabled = false;
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

    protected override void Update()
    {
        if (!targetModel.gameObject.activeSelf) {GetPlayersInScene();}
        _fsm.UpdateState();
    }

  
    [PunRPC]
    public void AssingPlayerRPC(MP_CharacterModel target)
    {
        target = (MP_CharacterModel)targetModel;
    }
}