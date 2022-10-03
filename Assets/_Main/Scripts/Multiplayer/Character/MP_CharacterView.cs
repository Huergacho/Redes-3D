using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MP_CharacterView : MonoBehaviourPun
{
    private Identificator _identificator;
    [SerializeField] private Identificator _identificatorPrefab;

    

    private void Start()
    {
       var canvas = GameObject.Find("Canvas");
       _identificator = GameObject.Instantiate<Identificator>(_identificatorPrefab, canvas.transform);
        _identificator.SetTarget(transform);  
        if (photonView.IsMine)
        {
          //  photonView.RPC("SetColor",RpcTarget.AllBuffered);
          SetColor();
        }
        else
        {
            photonView.RPC("RequestColor",photonView.Owner,PhotonNetwork.LocalPlayer);
        }
    }

    [PunRPC]
    public void RequestColor(Player client)
    {
        photonView.RPC("SetColor",client);
    }
    [PunRPC]
    public void SetColor()
    {
        if (_identificator != null)
        {
           var newColor = Random.ColorHSV();
            _identificator.SetColor(photonView.OwnerActorNr);
        }
    }


}