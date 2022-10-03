using System;
using UnityEngine;
public class EnemyChaseState<T> : State<T>
{   
    private Transform _target;

    private INode _root;
    private ObstacleAvoidance _behaviour;
   
    private float _timeToAttemptAttack;
    private float _counter;
    private Action<Vector3> _onMove;
    private Action<Vector3> _onLookAt;
    private Action <bool> _setIdleCommand;

    public EnemyChaseState(Transform target, INode root, ObstacleAvoidance behaviour, float timeToAttemptAttack, Action<Vector3> onMove, Action<Vector3> onLookAt, Action<bool> setIdleCommand)
    {
        _root = root;
        _behaviour = behaviour;
        _timeToAttemptAttack = timeToAttemptAttack;
        _onMove = onMove;
        _onLookAt = onLookAt;
        _target = target;
        _setIdleCommand = setIdleCommand;
    }
    
    private void ResetCounter()
    {
        _counter = _timeToAttemptAttack;
    }
    public override void Awake()
    {
        _behaviour.SetNewBehaviour(ObstacleAvoidance.DesiredBehaviour.Pursuit);
        _behaviour.SetNewTarget(_target);
        _setIdleCommand?.Invoke(false);
        ResetCounter();
    }

    public override void Execute()
    {
        var dir = _behaviour.GetDir();
        _onMove?.Invoke(dir);
        _onLookAt?.Invoke(dir);
        
        _counter -= Time.deltaTime;
        
        if (_counter > 0) return;
        
        _root.Execute();
        _setIdleCommand?.Invoke(false);
        ResetCounter();
    }
}