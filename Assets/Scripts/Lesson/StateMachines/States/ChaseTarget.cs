using System;
using System.Atomic.Implementations;
using GamePlay.Zombie;
using Lessons.StateMachines.States;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ChaseTarget : UpdateState
{
    private Transform _moveTransform;
    private Transform Target;

    private float _speed;
    
    protected override void OnUpdate(float deltaTime)
    {
        Debug.Log("+");
        /*if(_moveTransform ==null || Target == null )
            return;
        var targetPos = Target.position;
        var movePos = _moveTransform.position;
        Vector3.MoveTowards( movePos, targetPos, _speed * deltaTime);
        _moveTransform.LookAt(targetPos);*/
    }
    
    public void Construct(ZombieModel_Core.Chase chase, ZombieModel_Core.TargetChecker targetChecker)
    {
        Target = targetChecker.Target.Value.transform;
        _moveTransform = chase.MoveTransform;
        _speed = Random.Range(chase.MinSpeed.Value, chase.MaxSpeed.Value);
    }
}
