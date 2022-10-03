using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class SP_CharacterModel : MonoBehaviourPun
{ 
    public float speed;
    protected Rigidbody _rb;

    protected Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector3 dir)
    {
        _rb.velocity = new Vector3(dir.x, 0,dir.z) * speed;
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
}
