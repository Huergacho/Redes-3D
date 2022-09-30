using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(CharacterModel),typeof(Weapon),typeof(LifeController))]
public class CharacterController : MonoBehaviourPun
{
    private CharacterModel model;
    private LifeController _lifeController;
    private Weapon _weapon;
    [SerializeField] private float maxLife;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this); 
        }

        _lifeController = GetComponent<LifeController>();
        _lifeController.AssignLife(maxLife);
        _lifeController.onDie += Die;
        _weapon = GetComponent<Weapon>();
        model = GetComponent<CharacterModel>();

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            MoveCommand();
            Shoot();
            model.CorrectRotation();
        }

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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _weapon.Shoot();
        }
    }

    void Die()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
