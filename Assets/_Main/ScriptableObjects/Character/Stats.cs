using UnityEngine;

namespace _Main.ScriptableObjects.Character
{
    [CreateAssetMenu(fileName = "BaseStats", menuName = "Character Stats", order = 0)]
    public class Stats : ScriptableObject
    {
        [SerializeField] private float maxLife;
        public float MaxLife => maxLife;
        [SerializeField] private float maxSpeed;
        public float MaxSpeed => maxSpeed;
    }
}