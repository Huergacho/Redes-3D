using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SP_CharacterController : MonoBehaviourPun
{   private CharacterModel model;
    protected SP_LifeController _mpLifeController;
    protected Weapon _weapon;
    [SerializeField] private float maxLife;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _mpLifeController = GetComponent<SP_LifeController>();
        _mpLifeController.AssignLife(maxLife);
        _mpLifeController.OnDie += Die;
        _weapon = GetComponent<Weapon>();
        model = GetComponent<CharacterModel>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {   
            MoveCommand();
            Shoot();
            model.CorrectRotation();

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

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
