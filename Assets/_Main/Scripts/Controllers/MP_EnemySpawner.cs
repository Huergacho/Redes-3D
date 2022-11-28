using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class MP_EnemySpawner : MonoBehaviourPun
{
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private MP_EnemyController enemy;
    [SerializeField] private float secondsToSpawn;
    private Coroutine instanceEnemyRoutine;
    [SerializeField] private int enemyCount;
    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (instanceEnemyRoutine != null) {return;}

        instanceEnemyRoutine = StartCoroutine(InstanceCooldown());
    }

    private int GetRandomIndex()
    {
        var index = Random.Range(0, enemySpawnPoints.Length);
        return index;
    }

    public void InstatiateEnemyMultiPlayer()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject newEnemy = PhotonNetwork.Instantiate(enemy.gameObject.name, enemySpawnPoints[GetRandomIndex()].position, Quaternion.identity);
        }
    }

    private IEnumerator InstanceCooldown()
    {
        InstatiateEnemyMultiPlayer();
        yield return new WaitForSeconds(secondsToSpawn);
        instanceEnemyRoutine = null;
    }
}