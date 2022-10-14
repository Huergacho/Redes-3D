using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MP_CharacterView : SP_CharacterView
{
    private Identificator _identificator;

    private MP_PointCounter _pointCounter;

    [SerializeField]private MP_PointCounter _pointCounterMpPrefab;
    // private MP_PointCounter _pointCounter;
    // private MP_PointCounter mp_PointCounterPrefab;


    private void Start()
    {
       var canvas = GameObject.Find("Canvas");
       _identificator = GameObject.Instantiate<Identificator>(identificatorPrefab, canvas.transform);
        _identificator.SetTarget(transform);

        if (photonView.IsMine)
        {
            SetColor();
            // SetPointColor();
            _pointCounter = Instantiate(_pointCounterMpPrefab, canvas.transform);
            _pointCounter.SetTarget(gameObject.GetComponent<MP_CharacterController>());
            _pointCounter.SetColor(_identificator.GetColor());
            _pointCounter.Initialize();
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

    // [PunRPC]
    // public void RequestPointColor(Player client)
    // {
    //     photonView.RPC("SetPointColor",client);
    // }
    [PunRPC]
    public void SetColor()
    {
        if (_identificator != null)
        {
            _identificator.SetColor(photonView.OwnerActorNr);
        }
    }

    // [PunRPC]
    // public void SetPointColor()
    // {
    //     // if (_pointCounter)
    //     // {
    //     //     _pointCounter.SetColor(_identificator.GetColor());
    //     // }
    // }


}