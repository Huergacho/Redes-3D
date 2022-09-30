using System;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(LifeController))]
//public class EnemyController : MonoBehaviourPun
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemySO _enemyStats;
    [SerializeField] private CharacterModel target;
    private LifeController _lifeController;
    private EnemyModel _model;
    private EnemyView _view;


    #region FSM
    private FSM<EnemyStatesConstants> _fsm;
    private INode _root;
    #endregion

    #region IA
    [SerializeField] private ObstacleAvoidanceScriptableObject obstacleAvoidance;
    private bool _previousInSightState;
    private bool _currentInSightState;

    #endregion
    
    #region Actions
    public event Action<Vector3> OnMove;
    public event Action<Vector3> OnLookAt;
    public event Action OnChase;
    public event Action OnIdle;
    public event Action OnAttack;
    public event Action OnDie;
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        _lifeController = GetComponent<LifeController>();
        _model = GetComponent<EnemyModel>();
        _view = GetComponent<EnemyView>();
    }

    private void Start()
    {
        _model.Subscribe(this);
        _view.Subscribe(this);
        _lifeController.AssignLife(_enemyStats.maxLife);
        _lifeController.onDie += DieCommand;
    }

    private void InitFsm()
    {
        //--------------- FSM Creation -------------------//                
        // States Creation
        // var idle = new EnemyIdleState<EnemyStatesConstants>(data.idleLenght, CheckPlayerInSight,
        //    OnIdleCommand, _root);
        // var chase = new EnemyChaseState<EnemyStatesConstants>(target.transform, _root, Behaviour, OnChaseCommand,
        //     data.timeToAttemptAttack, OnMoveCommand,SetIdleStateCooldown);
        // var attack = new EnemyAttackState<EnemyStatesConstants>(_root,OnAttackCommand, data.timeToOutOfAttack,SetIdleStateCooldown);
        //
        
        //Idle State
        // idle.AddTransition(EnemyStatesConstants.Chase,chase);
        //
        // //Chase 
        // chase.AddTransition(EnemyStatesConstants.Attack,attack);
        //
        // //Attack
        // attack.AddTransition(EnemyStatesConstants.Chase,chase);
        //
      //  _fsm = new FSM<EnemyStatesConstants>(idle);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_lifeController.IsAlive()) return;
        
        _fsm.UpdateState();
    }

    bool CheckPlayerInSight()
    {
        return false;
    }
    void MoveCommand(Vector3 dir)
    {
        OnMove?.Invoke(dir);
    }
    void AttackCommand()
    {
        OnAttack?.Invoke();
    }
    private void DieCommand()
    {
        OnDie?.Invoke();
       // PhotonNetwork.Destroy(gameObject);
    }
}



