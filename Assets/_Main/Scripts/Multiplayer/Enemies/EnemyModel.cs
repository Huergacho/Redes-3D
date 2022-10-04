using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    private EnemySO _stats;
    private Rigidbody _rb;
    private Transform _transform;
    [SerializeField] private LineOfSightAI _lineOfSightAI;
    public LineOfSightAI LineOfSightAI => _lineOfSightAI;
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
        _rb.velocity = dir.normalized * _stats.speed;
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

    public void AssignStats(EnemySO stats)
    {
        _stats = stats;
    }
}