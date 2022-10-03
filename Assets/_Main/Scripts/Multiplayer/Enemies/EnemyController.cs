using Photon.Pun;
using UnityEngine;
using System;

[RequireComponent(typeof(MP_LifeController))]
public class EnemyController : MonoBehaviourPun
{
    private MP_LifeController _mpLifeController;
    [SerializeField]
    private EnemySO _enemyStats;

    private EnemyModel _enemyModel;

    #region Target

    public SP_CharacterController targetController;
    private Transform _targetTr => targetController.transform;

    #endregion
   

    #region FSM and DT
    [SerializeField] private ObstacleAvoidanceScriptableObject obstacleAvoidance;
    private FSM<EnemyStatesConstants> _fsm;
    private INode _root;
    private bool _previousInSightState;
    private bool _currentInSightState;

    
    #endregion
    
    #region Actions
    public event Action<Vector3> OnMove;
    public event Action OnChase;
    public event Action OnIdle;

    public event Action OnAttack;

    #endregion

    private void Awake()
    {
        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(_enemyStats.maxLife);
    }
    void Start()
    {
        _mpLifeController.OnDie += OnDieCommand;
        _enemyModel.Subscribe(this);
        InitDecisionTree();
        InitFsm();
    }

    void InitDecisionTree()
    {
  
        // Actions

        var goToFollow = new ActionNode(()=> _fsm.Transition(EnemyStatesConstants.Chase));
        var goToAttack = new ActionNode(()=> _fsm.Transition(EnemyStatesConstants.Attack));
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStatesConstants.Idle));
        
        // Questions
        var attemptPlayerKill = new QuestionNode(IsCloseEnoughToAttack, goToAttack, goToFollow);
        var DidSightChangeToAttack = new QuestionNode(SightStateChanged, goToFollow, attemptPlayerKill);
        var IsInSight = new QuestionNode(LastInSightState, DidSightChangeToAttack, attemptPlayerKill);
        
        var IsPlayerAlive = new QuestionNode(() => targetController.IsAlive(), IsInSight, goToIdle);
         
        _root = IsPlayerAlive;
    }   
    
    private bool SightStateChanged()
    { 
        return _currentInSightState != _previousInSightState;
    }

    private bool LastInSightState()
    {     
        _previousInSightState = _currentInSightState;    
        _currentInSightState = _enemyModel.LineOfSightAI.SingleTargetInSight(_targetTr);
        return _currentInSightState;
    }

    private bool IsCloseEnoughToAttack()
    {
        var distance = Vector3.Distance(transform.position, _targetTr.position);

        return distance <= _enemyStats.distanceToAttack;
    }

    void InitFsm()
    {
        //--------------- FSM Creation -------------------//                
        // States Creation
        var idle = new EnemyIdleState<EnemyStatesConstants>();//(data.idleLenght, CheckPlayerInSight, OnIdleCommand, _root);
        var chase = new EnemyChaseState<EnemyStatesConstants>();
        var attack = new EnemyAttackState<EnemyStatesConstants>();
      
        //Idle 
        idle.AddTransition(EnemyStatesConstants.Chase,chase);
        
        //Chase 
        chase.AddTransition(EnemyStatesConstants.Idle,idle);
        chase.AddTransition(EnemyStatesConstants.Attack,attack);
        
        //Attack
        attack.AddTransition(EnemyStatesConstants.Chase,chase);
        attack.AddTransition(EnemyStatesConstants.Idle,idle);
        
        _fsm = new FSM<EnemyStatesConstants>(idle);
   
    }
    // Update is called once per frame
    void Update()
    {
        if (!targetController.IsAlive()) return;
        
        _fsm.UpdateState();

    }
    #region Commands

    private void OnMoveCommand(Vector3 moveDir)
    {
        OnMove?.Invoke(moveDir);
    }    
    private void OnIdleCommand()
    {
        OnIdle?.Invoke();
    }

    private void OnChaseCommand()
    {
        OnChase?.Invoke();
    }

    private void OnAttackCommand()
    {
        OnAttack?.Invoke();
    }

    private void OnDieCommand()
    {
        // Ver si disparamos por model o view por animación
        PhotonNetwork.Destroy(gameObject);
    }
    #endregion
}