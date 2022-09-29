using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
[RequireComponent(typeof(CharacterModel),typeof(Weapon))]
public class CharacterController : MonoBehaviourPun
{
    private CharacterModel model;

    private Weapon _weapon;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Destroy(this); 
        }
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
}
