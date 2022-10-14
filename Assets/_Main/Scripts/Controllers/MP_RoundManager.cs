
using System;
using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class MP_RoundManager : MonoBehaviourPun
{
    [SerializeField]protected float roundChangeCooldown;
    private int _roundCount;
    public int RoundCount => _roundCount;
    [SerializeField] protected int ProvisionalEnemies;
    [SerializeField] private MP_EnemySpawner mpEnemySpawnerPrefab;
    private MP_EnemySpawner _mpEnemySpawner;
    private int _enemiesInScene;
    public static MP_RoundManager Instance;
    private RoundCounter _roundCounter;
    private RoundCounter[] _totalCounters;
    private void Awake()
    {
        Instance = this;
        GenerateSpawner();
    }

    private void Start()
    {
        _roundCounter = FindObjectOfType<RoundCounter>();
        CallToSpawn();
    }

    private void GenerateSpawner()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var spawner = PhotonNetwork.Instantiate(mpEnemySpawnerPrefab.gameObject.name, Vector3.zero, Quaternion.identity);
            _mpEnemySpawner = spawner.GetComponent<MP_EnemySpawner>();
        }
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
        if (photonView.IsMine)
        {
            _roundCount++;
            _roundCounter.UpdateText(_roundCount);
        }
        
    }


    private IEnumerator SpawnEnemies(int enemySpawnQuantity)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            yield return new WaitForSeconds(roundChangeCooldown);
            for (int i = 0; i < enemySpawnQuantity; i++)
            {
                _mpEnemySpawner.InstatiateEnemyMultiPlayer();
                _enemiesInScene++;
                yield return new WaitForSeconds(3f);
            }
        }
    }
    [PunRPC]
    public void EnemyUpdates()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _enemiesInScene--;
            if (_enemiesInScene <= 0)
            {
                AddRound();
                CallToSpawn();
            }
        }
        else
        {
            AddRound();
        }

    }
    
}