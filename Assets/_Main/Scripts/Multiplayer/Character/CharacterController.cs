using System;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterModel),typeof(MP_Weapon),typeof(MP_LifeController))]
public class CharacterController : MonoBehaviourPun
{
    [SerializeField] private Stats baseStats;
    private CharacterModel model;
    public CharacterModel Model => model;
    private MP_LifeController _mpLifeController;
    private MP_Weapon _mpWeapon;
    
    [SerializeField] private int currentPoints;
    public event Action<int> OnAddPoints;
    private void Awake()
    {
        if(!photonView.IsMine) Destroy(this);
        
        _mpLifeController = GetComponent<MP_LifeController>();
        model = GetComponent<CharacterModel>();
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
        if (photonView.IsMine)
        {
            photonView.RPC(nameof(PlayerDieRPC),RpcTarget.All,gameObject);
        }
    }

    void MoveCommand()
    {
        if (photonView.IsMine)
        {
            var movement = new Vector3(Input.GetAxis("Horizontal"),0 ,Input.GetAxis("Vertical"));
            if(movement != Vector3.zero)
            {
                model.Move(movement);
            }
        }

    }

    void Shoot()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                _mpWeapon.Shoot();
            }
        }
    }

    public bool IsAlive()
    {
        return _mpLifeController.IsAlive();
    }

    public CharacterModel GetModel()
    {
        if (photonView.IsMine)
        {
            return model;
        }
        else return null;
    }
    public void AddPoints()
    {
        if (photonView.IsMine)
        {
            currentPoints++;
            OnAddPoints?.Invoke(currentPoints);
        }
    }

    [PunRPC]
    void PlayerDieRPC(GameObject me)
    {
        me.SetActive(false);
    }
    
}
