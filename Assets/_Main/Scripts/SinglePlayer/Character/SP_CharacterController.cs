using System;
using System.Collections;
using System.Collections.Generic;
using _Main.ScriptableObjects.Character;
using Photon.Pun;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SP_CharacterController : MonoBehaviourPun
{
    [SerializeField] private Stats baseStats;
    private SP_CharacterModel model;
    private SP_LifeController _spLifeController;
    private SP_Weapon _spWeapon;

    [SerializeField] private int currentPoints;

    public event Action<int> addPoints;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _spLifeController = GetComponent<SP_LifeController>();
        model = GetComponent<SP_CharacterModel>();
        _spLifeController.AssignLife(baseStats.MaxLife);
        model.AssignStats(baseStats, _spLifeController);
        _spLifeController.OnDie += Die;
        _spWeapon = GetComponent<SP_Weapon>();

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
        if (Input.GetKey(KeyCode.P))
        {
            _spWeapon.Shoot();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return _spLifeController.IsAlive();
    }

    public void AddPoints()
    {
        currentPoints++;
        addPoints?.Invoke(currentPoints);
    }
}
