using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy", order = 0)]
public class EnemySO : ScriptableObject
{
    public float maxLife;
    public float speed;
    public float damage;
}
