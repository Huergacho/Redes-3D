using Photon.Pun;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MP_LifeController))]
public class MP_EnemyController : MonoBehaviourPun, IPooleable
{
    private SP_LifeController _mpLifeController;
    [SerializeField]
    private EnemySO enemyStats;

    private MP_EnemyModel _enemyModel;
    #region Target

    public MP_CharacterModel targetModel;
    private Transform TargetTr => targetModel.transform;

    #endregion
   
    #region FSM and DT
    [SerializeField] private ObstacleAvoidanceScriptableObject obstacleAvoidance;
    public ObstacleAvoidance Behaviour { get; private set; }
    
    private bool _waitForIdleState;
    private FSM<EnemyStatesConstants> _fsm;
    private INode _root;
    private bool _previousInSightState;
    private bool _currentInSightState;
    
    #endregion
    
    #region Actions
    public event Action<Vector3> OnMove;
    public event Action<Vector3> OnLookAt;
    public event Action OnIdle;
    public event Action OnAttack;

    public event Action OnDie;

    #endregion

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient) Destroy(this);
        
        _mpLifeController = GetComponent<MP_LifeController>();
        _mpLifeController.AssignLife(enemyStats.maxLife);
        _enemyModel = GetComponent<MP_EnemyModel>();

    }

    private void Start()
    {
        AssignTarget();
        if (targetModel != null)
        {
            InitializeObs();
        }
        _enemyModel.AssignStats(enemyStats);
        _enemyModel.Subscribe(this);
        _mpLifeController.OnDie += OnDieCommand;
        _mpLifeController.OnTakeHit += targetModel.GetComponent<MP_CharacterController>().AddPoints;
        InitDecisionTree();
        InitFsm();
    }

    private void AssignTarget()
    {
        // Chequear el juego de la bomba de clase
        int playerCount = PhotonNetwork.CountOfPlayers;
        int randomPlayerIndex = Random.Range(0, playerCount -1);
        var newPlayer = PlayerSaver.Instance.characters[randomPlayerIndex];
        if (newPlayer != null)
            targetModel = newPlayer;
        else
        {
            print("NO TA PLAYER");
        }
    }
    #region FSM Methods
    private void InitDecisionTree()
    {
  
        // Actions

        var goToChase = new ActionNode(()=> _fsm.Transition(EnemyStatesConstants.Chase));
        var goToAttack = new ActionNode(()=> _fsm.Transition(EnemyStatesConstants.Attack));
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStatesConstants.Idle));
        
        // Questions
        var attemptPlayerKill = new QuestionNode(IsCloseEnoughToAttack, goToAttack, goToChase);
        var didSightChangeToAttack = new QuestionNode(SightStateChanged, goToChase, attemptPlayerKill);
        var isInSight = new QuestionNode(LastInSightState, didSightChangeToAttack, attemptPlayerKill);
        
        var isPlayerAlive = new QuestionNode(() => targetModel.GetLife().IsAlive(), isInSight, goToIdle);
         
        _root = isPlayerAlive;
    }   
    private void InitFsm()
    {
        //--------------- FSM Creation -------------------//                
        // States Creation
        var idle = new EnemyIdleState<EnemyStatesConstants>(enemyStats.idleTimeLenght, CheckPlayerInSight,
            OnIdleCommand, _root, SetIdleStateCooldown);
       
        var chase = new EnemyChaseState<EnemyStatesConstants>(TargetTr, _root, Behaviour, 
            enemyStats.attackTimeLenght, OnMoveCommand,OnLookAtCommand, SetIdleStateCooldown);
        
        var attack = new EnemyAttackState<EnemyStatesConstants>(_root, OnAttackCommand, enemyStats.attackTimeLenght, 
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
    #endregion

    void Update()
    {
        if (!targetModel.GetLife().IsAlive()) return;
        _fsm.UpdateState();

    }
    private bool SightStateChanged()
    { 
        return _currentInSightState != _previousInSightState;
    }
    private bool LastInSightState()
    {     
        _previousInSightState = _currentInSightState;    
        _currentInSightState = _enemyModel.LineOfSightAI.SingleTargetInSight(TargetTr);
        return _currentInSightState;
    }
    
    private bool IsCloseEnoughToAttack()
    {
        var distance = Vector3.Distance(transform.position, TargetTr.position);

        return distance < enemyStats.distanceToAttack;
    }
    private void SetIdleStateCooldown(bool newState)
    {
        _waitForIdleState = newState;
    }
    private bool CheckPlayerInSight()
    {
        var playerIsInSight = _enemyModel.LineOfSightAI.SingleTargetInSight(TargetTr);
        return playerIsInSight;
    }
    
    private bool IsIdleStateCooldown()
    {
        return _waitForIdleState;
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
        OnAttack?.Invoke();
    }
    private void OnDieCommand()
    {
        gameObject.SetActive(false);
        OnDie?.Invoke();
    }
    #endregion


    public void OnObjectSpawn()
    {
        _mpLifeController.AssignLife(enemyStats.maxLife);
    }

    private void InitializeObs()
    {
        Behaviour = new ObstacleAvoidance(transform, null, obstacleAvoidance.radius,
            obstacleAvoidance.maxObjs, obstacleAvoidance.obstaclesMask,
            obstacleAvoidance.multiplier, targetModel, obstacleAvoidance.timePrediction,
            ObstacleAvoidance.DesiredBehaviour.Seek);
    }
}