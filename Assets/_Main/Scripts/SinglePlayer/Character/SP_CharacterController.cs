using System.Collections;
using System.Collections.Generic;
using _Main.Scripts.SinglePlayer.Gun;
using UnityEngine;

public class SP_CharacterController : MonoBehaviour
{
    private SP_CharacterModel model;
    private LifeController _mpLifeController;
    private SP_Weapon _mpWeapon;
    [SerializeField] private float maxLife;

    // Start is called before the first frame update
    private void Awake()
    {
        _mpLifeController = GetComponent<LifeController>();
        _mpLifeController.AssignLife(maxLife);
        _mpWeapon = GetComponent<SP_Weapon>();
        model = GetComponent<SP_CharacterModel>();

    }

    // Update is called once per frame
    void Update()
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
            _mpWeapon.Shoot();
        }
    }
}
