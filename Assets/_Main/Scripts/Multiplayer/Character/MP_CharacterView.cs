using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MP_CharacterView : SP_CharacterView
{
    private Identificator _identificator;

    private MP_PointCounter _pointCounter;

    [SerializeField]private MP_PointCounter _pointCounterMpPrefab;

    private GameObject _canvas;
    private void Start()
    {
       var canvas = GameObject.Find("Canvas");
       _canvas = canvas;
       SetIdentificator();

       #region Chequeo de Photon is Mine

       if (photonView.IsMine)
       {
           SetColor();
           SetPointCounter();
       }
       else
       {
           photonView.RPC("RequestColor",photonView.Owner,PhotonNetwork.LocalPlayer);
       }

       #endregion
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

    private void SetIdentificator()
    {
        _identificator = Instantiate(identificatorPrefab, _canvas.transform);
        _identificator.SetTarget(transform);
    }
    private void SetPointCounter()
    {
        
        _pointCounter = Instantiate(_pointCounterMpPrefab, _canvas.transform);
        _pointCounter.SetTarget(gameObject.GetComponent<MP_CharacterController>());
        _pointCounter.SetColor(_identificator.GetColor());
        _pointCounter.Initialize();

    }

}