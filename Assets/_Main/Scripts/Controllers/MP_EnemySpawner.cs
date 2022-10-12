using System;
using UnityEngine;
using Photon.Pun;
public class MP_EnemySpawner : SP_EnemySpawner
{
    public override void InstatiateEnemy()
    {
        if (photonView.IsMine)
        {
            var newEnemy = MP_GenericPool.Instance.SpawnFromPool("Goblin", enemySpawnPoints[GetRandomIndex()].position,
            Quaternion.identity);
        }
    }
}