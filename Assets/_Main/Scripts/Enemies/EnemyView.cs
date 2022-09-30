using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Subscribe(EnemyController controller)
    {
        // eventos del controlador
    }
}