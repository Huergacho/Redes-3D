
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class MP_RoundManager : SP_RoundManager
    {
        protected override void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                base.Start();
               print("blah");
            }
            else
            {
                Destroy(this);
            }
        }

        protected override void GenerateSpawner()
        {
            print("SII");
           var newSpawner = PhotonNetwork.Instantiate(_enemySpawner.gameObject.name, transform.position, Quaternion.identity);
           _enemySpawner = newSpawner.GetComponent<MP_EnemySpawner>();
        }

        // protected override void AddRound()
        // {
        //     if (photonView.IsMine)
        //     {
        //         base.AddRound();
        //     }
        // }
        //
        protected override IEnumerator SpawnEnemies(int enemySpawnQuantity)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                base.AddRound();
                yield return new WaitForSeconds(roundChangeCooldown);
                for (int i = 0; i < enemySpawnQuantity; i++)
                {
                    _enemySpawner.InstatiateEnemy();
                    yield return new WaitForSeconds(3f);
                }
            }
 
        }
    }