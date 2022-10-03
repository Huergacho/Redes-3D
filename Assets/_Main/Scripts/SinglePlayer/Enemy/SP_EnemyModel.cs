using UnityEngine;

public class SP_EnemyModel : MonoBehaviour
{
    public EnemySO data;
    private Rigidbody _rb;
    private Transform _transform;
    [SerializeField] private LineOfSightAI _lineOfSightAI;
    public LineOfSightAI LineOfSightAI => _lineOfSightAI;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
    }

    public void Subscribe(SP_EnemyController controller)
    {
        // eventos del controlador
    }

    private void Idle()
    {
        _rb.velocity = Vector3.zero;
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