using UnityEngine;
using Photon.Pun;


public class MasterController : MonoBehaviourPun
{
    [SerializeField] private MP_RoundManager roundManager;
    public static MasterController Instance { get; private set; }
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Instance = this;
            InstatiateMethods();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void InstatiateMethods()
    {
        PhotonNetwork.Instantiate(roundManager.gameObject.name, Vector3.zero, Quaternion.identity);
    }

    [PunRPC]
    public void FinishGame()
    {
        
    }


}