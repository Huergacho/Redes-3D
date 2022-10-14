using Photon.Pun;
using UnityEngine;

public class MP_EnemyView : MonoBehaviourPun
{
    private Animator _animator;

    private void Awake()
    {
        if(!photonView.IsMine) Destroy(this);
        _animator = GetComponentInChildren<Animator>();
    }

    public void Subscribe(MP_EnemyController controller)
    {
        controller.OnMove += Walk;
        controller.OnAttack += Attack;
        controller.OnDie += Die;
    }
    
    private void Attack(SP_LifeController blah)
    {
        _animator.SetFloat("Speed",0f);
        photonView.RPC(nameof(UpdateSpeed), RpcTarget.Others, 0f);
    }
    private void Walk(Vector3 a)
    {
        _animator.SetFloat("Speed",1f);
        photonView.RPC(nameof(UpdateSpeed), RpcTarget.Others, 1f);
    }

    private void Die()
    {
        _animator.SetFloat("Speed", -1f);
        photonView.RPC(nameof(UpdateSpeed), RpcTarget.Others, -1f);
    }



    [PunRPC]
    public void UpdateSpeed(float update)
    {
        _animator.SetFloat("Speed", update);
    }
    
}