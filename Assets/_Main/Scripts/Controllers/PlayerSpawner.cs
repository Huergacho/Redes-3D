using Photon.Pun;
using UnityEngine;



public class PlayerSpawner : MonoBehaviourPun
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    private void Awake()
    {
        if (!photonView.IsMine) Destroy(this);
        int random = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[random];
       // Transform spawnPoint = spawnPoints[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        //GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
      //  PhotonNetwork.Instantiate(playerToSpawn.ToString(), spawnPoint.position, Quaternion.identity);
        PhotonNetwork.Instantiate(playerPrefabs[0].ToString(), spawnPoint.position, Quaternion.identity);
    }
}