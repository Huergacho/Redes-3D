
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MP_CharacterView : MonoBehaviourPun
{
    private Identificator _identificator;

    private MP_PointCounter _pointCounter;
    
    [SerializeField]private Identificator identificatorPrefab;

   private GameObject _canvas;
    private void Start()
    {
       var canvas = GameObject.Find("Canvas");
       _canvas = canvas;
       SetIdentificator();
       identificatorPrefab.SetTarget(this.transform);
       identificatorPrefab.SetColor(1);

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
        var pointerUI = _canvas.GetComponentInChildren<MP_PointCounter>();
        _pointCounter = pointerUI;
        _pointCounter.Initialize(gameObject.GetComponent<CharacterModel>()); 

    }
    

}