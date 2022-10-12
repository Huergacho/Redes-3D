using System;
using UnityEngine;
using Photon.Pun;
public class MP_EnemySpawner : SP_EnemySpawner
{
    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(gameObject);
        }
    }

    public override void InstatiateEnemy()
    {
        var newEnemy = MP_GenericPool.Instance.SpawnFromPool("Goblin", enemySpawnPoints[GetRandomIndex()].position,
            Quaternion.identity);
    }
}