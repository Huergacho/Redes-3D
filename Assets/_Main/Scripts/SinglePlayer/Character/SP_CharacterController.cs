using System;
using _Main.ScriptableObjects.Character;
using Photon.Pun;
using UnityEngine;

public class SP_CharacterController : MonoBehaviourPun
{
    [SerializeField] private Stats baseStats;
    private SP_CharacterModel model;
    public SP_CharacterModel Model => model;
    private SP_LifeController _spLifeController;
    private SP_Weapon _spWeapon;

    [SerializeField] private int currentPoints;

    public event Action<int> OnAddPoints;
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
        if (Input.GetMouseButton(0))
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

    public virtual SP_CharacterModel GetModel()
    {
        return model;
    }
    public virtual void AddPoints()
    {
        currentPoints++;
        OnAddPoints?.Invoke(currentPoints);
    }
}
