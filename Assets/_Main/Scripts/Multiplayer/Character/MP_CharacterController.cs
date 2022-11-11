using System;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(MP_CharacterModel),typeof(MP_Weapon),typeof(MP_LifeController))]
public class MP_CharacterController : MonoBehaviourPun
{
    [SerializeField] private Stats baseStats;
    private MP_CharacterModel model;
    public MP_CharacterModel Model => model;
    private MP_LifeController _mpLifeController;
    private MP_Weapon _mpWeapon;
    
    [SerializeField] private int currentPoints;
    public event Action<int> OnAddPoints;
    private void Awake()
    {
        if(!photonView.IsMine) Destroy(this);
        
        _mpLifeController = GetComponent<MP_LifeController>();
        model = GetComponent<MP_CharacterModel>();
        _mpLifeController.AssignLife(baseStats.MaxLife);
        model.AssignStats(baseStats, _mpLifeController);
        _mpLifeController.OnDie += Die;
        _mpWeapon = GetComponent<MP_Weapon>();

    }

    private  void Update()
    {
        MoveCommand();
        Shoot();
        model.CorrectRotation();
    }

    private void Die()
    {
        // gameObject.SetActive(false);
        
       // PhotonNetwork.LocalPlayer;
       photonView.RPC(nameof(PlayerDieRPC),RpcTarget.All,gameObject);
    }

    void MoveCommand()
    {
        var movement = new Vector3(Input.GetAxis("Horizontal"),0 ,Input.GetAxis("Vertical"));
        if(movement != Vector3.zero)
        {
            model.Move(movement);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            _mpWeapon.Shoot();
        }
    }

    public bool IsAlive()
    {
        return _mpLifeController.IsAlive();
    }

    public MP_CharacterModel GetModel()
    {
        return model;
    }
    public void AddPoints()
    {
        currentPoints++;
        OnAddPoints?.Invoke(currentPoints);
    }

    [PunRPC]
    void PlayerDieRPC(GameObject me)
    {
       // photonView.enable = false;
        me.SetActive(false);
    }
    
}
