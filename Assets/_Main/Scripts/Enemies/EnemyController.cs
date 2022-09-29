using System.Collections;
using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(LifeController))]
public class EnemyController : MonoBehaviourPun
{
    private LifeController _lifeController;
    [SerializeField]
    private EnemySO _enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        _lifeController = GetComponent<LifeController>();
        _lifeController.AssignLife(_enemyStats.maxLife);
        _lifeController.onDie += Die;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
