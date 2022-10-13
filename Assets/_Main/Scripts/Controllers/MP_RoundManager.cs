
using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class MP_RoundManager : MonoBehaviourPun
{
    [SerializeField]protected float roundChangeCooldown;
    [SerializeField]private int RoundCount;
    [SerializeField] protected int ProvisionalEnemies;
    [SerializeField] private MP_EnemySpawner mpEnemySpawnerPrefab;
    private MP_EnemySpawner _mpEnemySpawner;
    
    private void Awake()
    {
        GenerateSpawner();
    }

    private void Start()
    {
        CallToSpawn();
    }

    private void GenerateSpawner()
    {
        var spawner = PhotonNetwork.Instantiate(mpEnemySpawnerPrefab.gameObject.name, Vector3.zero, Quaternion.identity);
        _mpEnemySpawner = spawner.GetComponent<MP_EnemySpawner>();
    }

    private void CallToSpawn()
    {
        if (PhotonNetwork.IsMasterClient)
        {
           StartCoroutine(SpawnEnemies(ProvisionalEnemies));
        }
    } 
    protected virtual void AddRound()
    {
        RoundCount++;
    }


    private IEnumerator SpawnEnemies(int enemySpawnQuantity)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            AddRound();
            yield return new WaitForSeconds(roundChangeCooldown);
            for (int i = 0; i < enemySpawnQuantity; i++)
            {
                _mpEnemySpawner.InstatiateEnemyMultiPlayer();
                yield return new WaitForSeconds(3f);
            }
        }

    }
    }