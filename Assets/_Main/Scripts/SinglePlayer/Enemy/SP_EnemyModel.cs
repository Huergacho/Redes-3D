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
        controller.OnMove += Move;
        controller.OnAttack += Attack;
        controller.OnIdle += Idle;
        controller.OnLookAt += LookAt;
    }

    private void Idle()
    {
        _rb.velocity = Vector3.zero;
    }

    private void Move(Vector3 dir)
    {
        var dirNorm = dir.normalized;
        _rb.velocity = dirNorm * data.speed;
 
    }

    private void LookAt(Vector3 dir)
    {
        _transform.forward = dir.normalized;
    }

    private void Attack()
    {
        Debug.Log("Pew pew");
    }

    private void Die()
    {
    }
}