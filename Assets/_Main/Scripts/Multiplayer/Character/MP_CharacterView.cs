
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MP_CharacterView : MonoBehaviourPun
{
    private Identificator _identificator;

    private SP_PointCounter _pointCounter;
    
    [SerializeField]private Identificator identificatorPrefab;

    [SerializeField]private SP_PointCounter spPointCounterPrefab;

    [SerializeField]private MP_PointCounter _pointCounterMpPrefab;

    private GameObject _canvas;
    private void Start()
    {
       var canvas = GameObject.Find("Canvas");
       _canvas = canvas;
       SetIdentificator();
       identificatorPrefab.SetTarget(this.transform);
       identificatorPrefab.SetColor(1);
       spPointCounterPrefab.SetColor(identificatorPrefab.GetColor());
       spPointCounterPrefab.Initialize();

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