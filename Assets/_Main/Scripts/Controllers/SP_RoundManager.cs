using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using Photon.Pun;
public class SP_RoundManager : MonoBehaviourPun
    {
        [SerializeField]protected float roundChangeCooldown;
        [SerializeField] protected SP_EnemySpawner _enemySpawner;
        [SerializeField]private int RoundCount;
        [SerializeField] private int ProvisionalEnemies;

        protected virtual void Start()
        {
            CallToSpawn();
        }

        protected virtual void AddRound()
        {
            RoundCount++;
        }

        protected virtual void CallToSpawn()
        {
            StartCoroutine(SpawnEnemies(ProvisionalEnemies));
        }
        IEnumerator SpawnEnemies(int enemySpawnQuantity)
        {
            AddRound();
            yield return new WaitForSeconds(roundChangeCooldown);
            for (int i = 0; i < enemySpawnQuantity; i++)
            {
                _enemySpawner.InstatiateEnemy();
            }
        }

    }