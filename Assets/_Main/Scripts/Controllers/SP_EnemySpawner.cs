using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SP_EnemySpawner : MonoBehaviourPun
{
    [SerializeField] protected Transform[] enemySpawnPoints;

    protected int GetRandomIndex()
    {
        var index = Random.Range(0, enemySpawnPoints.Length);
        return index;
    }
    public virtual void InstatiateEnemy()
    {
        var newEnemy = SP_GenericPool.Instance.SpawnFromPool("Goblin", enemySpawnPoints[GetRandomIndex()].position,
            Quaternion.identity);
    }
}
