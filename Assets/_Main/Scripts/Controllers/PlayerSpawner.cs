using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerSpawner : MonoBehaviourPun
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    
    private void Awake()
    {
        
    }
    // Hacer un spawner que tome lo de photon y devueva el objeto que queremos

    private void Start()
    {
        //int random = Random.Range(0, spawnPoints.Length);
        //Transform spawnPoint = spawnPoints[random];
        Transform spawnPoint = spawnPoints[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
    }
}