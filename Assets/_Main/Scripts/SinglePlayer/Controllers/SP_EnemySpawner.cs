using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SP_EnemySpawner : MonoBehaviourPun
{
    [SerializeField] private SP_EnemyController enemyToInstatiate;
    [SerializeField] private Transform[] enemySpawnPoints;
    private SP_CharacterModel _target;
    private void Awake()
    {
    }

    private void Start()
    {
        _target = SP_GameManager.instance.Character;
    }

    private int GetRandomIndex()
    {
        var index = Random.Range(0, enemySpawnPoints.Length);
        return index;
    }
    public virtual void InstatiateEnemy()
    {
        var newEnemy = SP_GenericPool.Instance.SpawnFromPool("Goblin", enemySpawnPoints[GetRandomIndex()].position,
            Quaternion.identity);
        //newEnemy.GetComponent<SP_EnemyController>().AssignTarget(_target);
        //var newEnemy = Instantiate(enemyToInstatiate, enemySpawnPoints[GetRandomIndex()]);
    }
}
