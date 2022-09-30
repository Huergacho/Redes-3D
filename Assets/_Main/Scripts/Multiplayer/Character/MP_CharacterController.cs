using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(SP_CharacterModel),typeof(MP_Weapon),typeof(MP_LifeController))]
public class MP_CharacterController : MonoBehaviourPun
{
    private SP_CharacterModel model;
    private MP_LifeController _mpLifeController;
    private MP_Weapon _mpWeapon;
    [SerializeField] private float maxLife;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this); 
        }

        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(maxLife);
        _mpWeapon = GetComponent<MP_Weapon>();
        model = GetComponent<SP_CharacterModel>();

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
            _mpWeapon.Shoot();
        }
    }
}
