using Photon.Pun;
using UnityEngine;
using System;
using Photon.Pun.Demo.PunBasics;

[RequireComponent(typeof(SP_LifeController))]
public class SP_EnemyController : MonoBehaviourPun, IPooleable
{
    private SP_LifeController _spLifeController;
    [SerializeField]
    protected EnemySO _enemyStats;

    protected SP_EnemyModel _enemyModel;
    #region Target

    public SP_CharacterModel targetModel;
    private Transform _targetTr => targetModel.transform;

    #endregion
   
    #region FSM and DT
    [SerializeField] private ObstacleAvoidanceScriptableObject obstacleAvoidance;
    public ObstacleAvoidance Behaviour { get; private set; }
    
    private bool _waitForIdleState;
    protected FSM<EnemyStatesConstants> _fsm;
    private INode _root;
    private bool _previousInSightState;
    private bool _currentInSightState;
    
    #endregion
    
    #region Actions
    public event Action<Vector3> OnMove;
    public event Action<Vector3> OnLookAt;
    public event Action OnIdle;
    public event Action<SP_LifeController> OnAttack;
    public event Action OnDie; 

    #endregion
   
    protected  virtual void Awake()
    {
        if (!photonView.IsMine) Destroy(this);
        _spLifeController = GetComponent<SP_LifeController>();
        _spLifeController.AssignLife(_enemyStats.maxLife);
        _enemyModel = GetComponent<SP_EnemyModel>();
        AssignTarget();
    }
    protected virtual void Start()
    {
        _enemyModel.AssignStats(_enemyStats);
        InitializeVoids();
    }

    protected void InitDecisionTree()
    {
  
        // Actions

        var goToChase = new ActionNode(()=> _fsm.Transition(EnemyStatesConstants.Chase));
        var goToAttack = new ActionNode(()=> _fsm.Transition(EnemyStatesConstants.Attack));
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStatesConstants.Idle));
        
        // Questions
        var attemptPlayerKill = new QuestionNode(IsCloseEnoughToAttack, goToAttack, goToChase);
        var DidSightChangeToAttack = new QuestionNode(SightStateChanged, goToChase, attemptPlayerKill);
        var IsInSight = new QuestionNode(LastInSightState, DidSightChangeToAttack, attemptPlayerKill);
        
        var IsPlayerAlive = new QuestionNode(() => targetModel.GetLife().IsAlive(), IsInSight, goToIdle);
         
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

        return distance < _enemyStats.distanceToAttack;
    }
    private bool IsIdleStateCooldown()
    {
        return _waitForIdleState;
    }

    private void SetIdleStateCooldown(bool newState)
    {
        _waitForIdleState = newState;
    }
    private bool CheckPlayerInSight()
    {
        var playerIsInSight = _enemyModel.LineOfSightAI.SingleTargetInSight(_targetTr);
        return playerIsInSight;
    }

    protected void InitFsm()
    {
        //--------------- FSM Creation -------------------//                
        // States Creation
        var idle = new EnemyIdleState<EnemyStatesConstants>(_enemyStats.idleTimeLenght, CheckPlayerInSight,
            OnIdleCommand, _root, SetIdleStateCooldown);
       
        var chase = new EnemyChaseState<EnemyStatesConstants>(_targetTr, _root, Behaviour, 
            _enemyStats.attackTimeLenght, OnMoveCommand,OnLookAtCommand, SetIdleStateCooldown);
        
        var attack = new EnemyAttackState<EnemyStatesConstants>(_root, OnAttackCommand, _enemyStats.attackTimeLenght, 
            SetIdleStateCooldown);
      
        //Idle 
        idle.AddTransition(EnemyStatesConstants.Chase,chase);
        idle.AddTransition(EnemyStatesConstants.Attack,attack);
        
        //Chase 
        chase.AddTransition(EnemyStatesConstants.Idle,idle);
        chase.AddTransition(EnemyStatesConstants.Attack,attack);
        
        //Attack
        attack.AddTransition(EnemyStatesConstants.Chase,chase);
        attack.AddTransition(EnemyStatesConstants.Idle,idle);
        
        _fsm = new FSM<EnemyStatesConstants>(idle);
   
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!targetModel.GetLife().IsAlive()) return;
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
        SetIdleStateCooldown(true);
    }

    private void OnLookAtCommand(Vector3 dir)
    {
        OnLookAt?.Invoke(dir);
    }

    private void OnAttackCommand()
    {
        OnAttack?.Invoke(targetModel.GetLife());
    }

    private void OnDieCommand()
    {
        OnDie?.Invoke();
        PhotonNetwork.Destroy(gameObject);
    }

    public virtual void AssignTarget()
    {
        targetModel = SP_GameManager.SPInstance.Character;
    }

    protected virtual void InitializeVoids()
    {
        if (targetModel != null)
        {
            InitializeOBS();
        }
        _spLifeController.OnDie += OnDieCommand;
        _enemyModel.Subscribe(this);
        InitDecisionTree();
        InitFsm();
    }
    public void InitializeOBS()
    {
        Behaviour = new ObstacleAvoidance(transform, null, obstacleAvoidance.radius,
            obstacleAvoidance.maxObjs, obstacleAvoidance.obstaclesMask,
            obstacleAvoidance.multiplier, targetModel, obstacleAvoidance.timePrediction,
            ObstacleAvoidance.DesiredBehaviour.Seek);
    }
    #endregion

    public virtual void OnObjectSpawn()
    {
        _spLifeController.AssignLife(_enemyStats.maxLife);
    }
}