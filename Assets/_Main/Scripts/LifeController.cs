using System;
using Photon.Pun;
using UnityEngine;

//public class LifeController: MonoBehaviourPun
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
        // if (photonView.IsMine)
        // {
            if (currentLife - damage <= 0)
            {
                Die();
            }
            currentLife -= damage;
        //}

    }

    public bool IsAlive()
    {
        return  currentLife > 0;
    }
    private void Die()
    {
        onDie.Invoke();
    }
}