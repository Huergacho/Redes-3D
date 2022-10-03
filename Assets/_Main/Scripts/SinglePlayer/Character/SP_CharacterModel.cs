using System.Collections;
using System.Collections.Generic;
using _Main.ScriptableObjects.Character;
using Photon.Pun;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class SP_CharacterModel : MonoBehaviourPun, IVel
{
    protected Rigidbody _rb;
    private Stats _stats;
    protected Camera _camera;
    private SP_LifeController _lifeController;
    
    private float _lastMoveMagnitude;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        _rb.velocity = new Vector3(dir.x, 0,dir.z) * _stats.MaxSpeed;
    }
    public void CorrectRotation()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            var distance = Vector3.Distance(transform.position, hitInfo.point);
            if (distance >= 1f)
                transform.LookAt(target);
        }
    }

    public void AssignStats(Stats data, SP_LifeController lifeController)
    {
        _stats = data;
        _lifeController = lifeController;
    }

    
    public float Vel
    {
        get => _lastMoveMagnitude;
        private set => _lastMoveMagnitude = value;
    }

    public SP_LifeController GetLife()
    {
        return _lifeController;
    }
}
