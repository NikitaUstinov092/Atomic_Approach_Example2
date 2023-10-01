using System.Atomic.Implementations;
using System.Declarative.Scripts.Attributes;
using GamePlay.Custom.Sections;
using GamePlay.Zombie;
using Lessons.StateMachines.States;
using UnityEngine;


public class ChaseTarget : UpdateState
{
    private Transform _moveTransform;
    private Transform _targetTransform;

    private float _speed;
    
    protected override void OnUpdate(float deltaTime)
    {
        var targetPos = _targetTransform.position;
        var movePos = _moveTransform.position;
        Vector3.MoveTowards( movePos, targetPos, _speed * deltaTime);
        _moveTransform.LookAt(targetPos);
    }
    
    public void Construct(ZombieModel_Core.Chase chase, ZombieModel_Core.TargetChecker targetChecker)
    {
        _targetTransform = targetChecker.Target.Value.transform;
        _speed = Random.Range(chase.MinSpeed.Value, chase.MaxSpeed.Value);
    }
}
