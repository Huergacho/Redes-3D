using System;
using System.Security.Cryptography;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet : MonoBehaviourPun 
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    public int damage;


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
            MakeDamage(other.gameObject);
            DestroyObject();
        }
        else
        {
            photonView.RPC("RequestLifeController",photonView.Owner,other.gameObject);
        }
    }

    private void MakeDamage(GameObject target)
    {
        var life = target.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage(damage);
        }
    }
    [PunRPC]
    public void RequestLifeController(Player player, GameObject target)
    {
        photonView.RPC("MakeDamage",player,target);
    }
}