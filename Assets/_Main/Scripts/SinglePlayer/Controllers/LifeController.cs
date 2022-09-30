using System;
using UnityEngine;

public class LifeController: MonoBehaviour
{
    private float currentLife;
    public event Action onDie;
    public void AssignLife(float data)
    {
        currentLife = data;
    }

    public void TakeDamage(float damage)
    { 
        if (currentLife - damage <= 0)
        {
            Die();
        }
        currentLife -= damage;
    }

    private void Die()
    {
        onDie.Invoke();
    }
}