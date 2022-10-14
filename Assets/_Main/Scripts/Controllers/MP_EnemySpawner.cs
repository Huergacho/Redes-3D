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
        GameObject newEnemy = PhotonNetwork.Instantiate(enemy.gameObject.name, enemySpawnPoints[GetRandomIndex()].position, Quaternion.identity);
        //var controller = newEnemy.GetComponent<MP_EnemyController>();
       // controller.AssignTargetFromOutside(PlayerSaver.Instance.characters[Random.Range(0,PlayerSaver.Instance.characters.Count -1)]);
       // photonView.RPC(nameof(controller.AssignTarget),photonView.Owner,PlayerSaver.Instance.characters[Random.Range(0,PlayerSaver.Instance.characters.Count -1)]);
    }
}