using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    public EnemySO data;
    private Rigidbody _rb;
    private Transform _transform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
    }

    public void Subscribe(EnemyController controller)
    {
        // eventos del controlador
    }

    private void Move(Vector3 dir)
    {
        _rb.velocity = dir.normalized * data.speed;
    }

    private void LookAt(Vector3 dir)
    {
        _transform.forward = dir.normalized;
    }

    private void Attack()
    {
    }

    private void Die()
    {
    }
}