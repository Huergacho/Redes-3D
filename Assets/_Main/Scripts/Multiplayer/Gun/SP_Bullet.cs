using Photon.Pun;
using UnityEngine;
public class SP_Bullet : MonoBehaviourPun
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeTime;
    public int damage;
    public MP_CharacterController owner;

    protected virtual void Update()
    {
        Move();
        DestroyOnLifeSpan();

    }

    private void Move()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void DestroyOnLifeSpan()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            DestroyObject();
        }
    }
    protected virtual void OnCollisionEnter(Collision other) 
    {
        MakeDamage(other.gameObject);
            //  photonView.RPC("RequestLifeController",photonView.Owner,other.gameObject);
        DestroyObject();
        
    }

    private void MakeDamage(GameObject target)
    {
       var life = GetTargetLifeComponent(target);
        if (life != null)
        {
            life.TakeDamage(damage);
            owner.AddPoints();
        }
    }

    protected virtual SP_LifeController GetTargetLifeComponent(GameObject target)
    {
        return target.GetComponent<SP_LifeController>();
    }
    
}