using Photon.Pun;
using UnityEngine;
using System;

[RequireComponent(typeof(MP_LifeController))]
public class EnemyController : SP_EnemyController
{
    private MP_LifeController _mpLifeController;
    [SerializeField] private EnemySO _enemyStats;

    private EnemyModel _enemyModel;

    
    private void Awake()
    {
        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(_enemyStats.maxLife);
    }

    void Start()
    {
        _mpLifeController.OnDie += OnDieCommand;
        _enemyModel.Subscribe(this);
    }



    private void OnDieCommand()
    {
        // Ver si disparamos por model o view por animación
        PhotonNetwork.Destroy(gameObject);
    }
}