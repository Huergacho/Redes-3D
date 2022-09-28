using System;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private float bulletSpeed;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position += Vector3.forward * bulletSpeed;
    }
    private void OnCollisionEnter(Collision other) 
    {
        Destroy(gameObject);
    }    
}