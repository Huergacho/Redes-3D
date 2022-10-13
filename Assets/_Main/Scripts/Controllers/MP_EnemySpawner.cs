using UnityEngine;
using Photon.Pun;

public class MP_EnemySpawner : MonoBehaviourPun
{
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private MP_EnemyModel enemy;
    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Destroy(gameObject);
        }
    }

    private int GetRandomIndex()
    {
        var index = Random.Range(0, enemySpawnPoints.Length);
        return index;
    }

    public void InstatiateEnemyMultiPlayer()
    {
        PhotonNetwork.Instantiate(enemy.gameObject.name, enemySpawnPoints[GetRandomIndex()].position, Quaternion.identity);
    }
}