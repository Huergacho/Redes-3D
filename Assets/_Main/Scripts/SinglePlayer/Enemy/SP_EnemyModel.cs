using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SP_EnemyModel : MonoBehaviourPun
{
    public EnemySO data;
    private Rigidbody _rb;
    private Transform _transform;
    [SerializeField] protected LineOfSightAI _lineOfSightAI;
    public LineOfSightAI LineOfSightAI => _lineOfSightAI;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        if (!photonView.IsMine)
        {
            photonView.RPC(nameof(UpdateForward),photonView.Owner,PhotonNetwork.LocalPlayer);        
        }
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
        var normDir = dir.normalized;
        photonView.RPC(nameof(LookAtRPC),RpcTarget.Others,normDir);
        _transform.forward = normDir;
    }

    private void Attack(SP_LifeController target)
    {
        if(!CanHit(target.gameObject.transform.position)) return;
        var life = GetTargetLifeComponent(target.gameObject);
        if (life != null)
        {
            life.TakeDamage(data.damage);
        }
    }

    public void AssignStats(EnemySO stats)
    {
        data = stats;
    }

    private bool CanHit(Vector3 target)
    {
        return Vector3.Magnitude(transform.position - target) < data.distanceToAttack;
    }
    
    [PunRPC]
    public void LookAtRPC(Vector3 dir)
    {
        _transform.forward = dir;
    }

    [PunRPC]
    public void UpdateForward(Player player)
    {
        photonView.RPC(nameof(LookAtRPC),player,_transform.forward);
    }
    protected virtual SP_LifeController GetTargetLifeComponent(GameObject target)
    {
        return target.GetComponent<SP_LifeController>();
    }
}