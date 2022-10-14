using UnityEngine;

public class MP_EnemyView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Subscribe(MP_EnemyController controller)
    {
        controller.OnMove += Walk;
        controller.OnAttack += Attack;
    }
    
    private void Attack()
    {
        _animator.SetFloat("Speed",0f);
    }
    private void Walk(Vector3 a)
    {
        _animator.SetFloat("Speed",1f);
    }
}