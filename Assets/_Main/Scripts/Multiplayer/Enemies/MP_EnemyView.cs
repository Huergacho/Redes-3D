using UnityEngine;

public class MP_EnemyView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Subscribe(MP_EnemyController controller)
    {
        // eventos del controlador
    }
}