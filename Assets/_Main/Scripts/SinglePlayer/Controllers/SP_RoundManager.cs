using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
public class SP_RoundManager : MonoBehaviourPun
    {
        [SerializeField]protected float roundChangeCooldown;
        [SerializeField] protected SP_EnemySpawner _enemySpawner;
        [SerializeField]private int RoundCount;

        protected void Awake()
        {
            _enemySpawner = GetComponent<SP_EnemySpawner>();
        }
        
        protected virtual void AddRound()
        {
            RoundCount++;
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