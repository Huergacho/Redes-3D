using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
public class SP_RoundManager : MonoBehaviourPun
    {
        [SerializeField]protected float roundChangeCooldown;
        [SerializeField] protected SP_EnemySpawner _enemySpawner;
        [SerializeField]private int RoundCount;
        [SerializeField] protected int ProvisionalEnemies;

        private void Awake()
        {
            GenerateSpawner();
        }

        protected virtual void Start()
        {
            CallToSpawn();
        }
   

        protected virtual void GenerateSpawner()
        {
            Instantiate(_enemySpawner, transform.position,Quaternion.identity);
        }

        protected virtual void AddRound()
        {
            RoundCount++;
        }

        protected virtual void CallToSpawn()
        {
            if (RoundCount < 3)
            {
                StartCoroutine(SpawnEnemies(ProvisionalEnemies));
            }
        }
        protected virtual IEnumerator SpawnEnemies(int enemySpawnQuantity)
        {
            AddRound();
            yield return new WaitForSeconds(roundChangeCooldown);
            for (int i = 0; i < enemySpawnQuantity; i++)
            {
                _enemySpawner.InstatiateEnemy();
            }
        }

    }