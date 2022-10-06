using Photon.Pun;
using UnityEngine;
using System;
public class SP_LifeController : MonoBehaviourPun
{
    private float _currentLife;
    public event Action OnDie;
    public event Action OnTakeHit;
    public void AssignLife(float data)
    {
        _currentLife = data;
    }
    public virtual void TakeDamage (float damage)
    {
        if (_currentLife - damage <= 0)
        {
            Die();
        }
        OnTakeHit?.Invoke();
        _currentLife -= damage;
        }

    public bool IsAlive()
    {
        return  _currentLife > 0;
    }
    protected virtual void Die()
    {
        OnDie?.Invoke();
    }
}