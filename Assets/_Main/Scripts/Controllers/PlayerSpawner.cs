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
        //int random = Random.Range(0, spawnPoints.Length);
        // Transform spawnPoint = spawnPoints[random];
        // Transform spawnPoint = spawnPoints[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        // GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        PhotonNetwork.Instantiate(playerPrefabs[0].name, spawnPoints[0].position, Quaternion.identity);
    }
}