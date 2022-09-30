using UnityEngine;
public class SP_Bullet : MonoBehaviour
    {
            [SerializeField] private float bulletSpeed;
            [SerializeField] private float lifeTime;
            public int damage;
        
        
            private void Update()
            {
                Move();
                DestroyOnLifeSpan();

            }
            private void Move()
            {
                transform.position += transform.forward * bulletSpeed * Time.deltaTime;
            }
        
            private void DestroyObject()
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
            private void OnCollisionEnter(Collision other) 
            {
                MakeDamage(other.gameObject);
                DestroyObject();
            }

            private void MakeDamage(GameObject target)
            {
                var life = target.GetComponent<MP_LifeController>();
                if (life != null)
                {
                    life.TakeDamage(damage);
                }
            }
    }