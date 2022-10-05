using UnityEngine;
public class MP_EnemySpawner : SP_EnemySpawner
{
    public override void InstatiateEnemy()
    {
        var newEnemy = MP_GenericPool.Instance.SpawnFromPool("Goblin", enemySpawnPoints[GetRandomIndex()].position,
            Quaternion.identity);
    }
}