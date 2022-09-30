using System.Collections;
using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(MP_LifeController))]
public class EnemyController : MonoBehaviourPun
{
    private MP_LifeController _mpLifeController;
    [SerializeField]
    private EnemySO _enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(_enemyStats.maxLife);
        _mpLifeController.onDie += Die;
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
