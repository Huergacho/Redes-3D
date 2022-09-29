using System;
using System.Security.Cryptography;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun 
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;   
    private void Update()
    {
        //if (photonView.IsMine)
        //{
        Move();
        DestroyOnLifeSpan();
        //}

    }
    private void Move()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    private void DestroyObject()
    {
        
            PhotonNetwork.Destroy(gameObject);
        
    }

    private void DestroyOnLifeSpan()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroyObject();
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (photonView.IsMine)
        {
            DestroyObject();
        }
    }    
}