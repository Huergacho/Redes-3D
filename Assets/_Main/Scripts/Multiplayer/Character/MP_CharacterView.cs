using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MP_CharacterView : SP_CharacterView
{
    private Identificator _identificator;



    private void Start()
    {
       var canvas = GameObject.Find("Canvas");
       _identificator = GameObject.Instantiate<Identificator>(identificatorPrefab, canvas.transform);
        _identificator.SetTarget(transform);  
        if (photonView.IsMine)
        {
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
            _identificator.SetColor(photonView.OwnerActorNr);
        }
    }


}