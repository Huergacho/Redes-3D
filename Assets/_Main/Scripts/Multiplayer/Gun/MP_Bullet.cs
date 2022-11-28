using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MP_Bullet : MonoBehaviourPun 
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    public int damage;
    public CharacterModel owner;

    private void Start()
    {
    }

    private void Update()
    {
        Move();
        if (photonView.IsMine)
        {
            DestroyOnLifeSpan();
        }

    }
    private void DestroyObject()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if (photonView.IsMine)
        {
            MakeDamage(other.gameObject);
            DestroyObject();
        }
    }
    private void MakeDamage(GameObject target)
    {
        var life = GetTargetLifeComponent(target);
        if (life != null)
        {
            owner.AddPoints();
            life.TakeDamage(damage);
        }
    }
    private void DestroyOnLifeSpan()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroyObject();
        }
    }
    private void Move()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }
    private MP_LifeController GetTargetLifeComponent(GameObject target)
    {
        return target.GetComponent<MP_LifeController>();
    }
}