using System;
using System.Atomic.Implementations;
using Lessons.StateMachines.States;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ChaseTarget : UpdateState
{
    private AtomicVariable<Transform> _moveTransform;
    private AtomicVariable<Entity.Entity> Target;

    private float _speed;
    
    protected override void OnUpdate(float deltaTime)
    {
        if (Target.Value == null) 
            return;
        
        var targetPos = Target.Value.transform.position;
        var movePos = _moveTransform.Value;
        movePos.position = Vector3.MoveTowards( movePos.position, targetPos, _speed * deltaTime);
        movePos.LookAt(targetPos);
    }
    
    public void Construct(AtomicVariable<Transform> moveTransform, AtomicVariable<Entity.Entity> target,
        float minSpeed, float maxSpeed)
    {
        Target = target;
        _moveTransform = moveTransform;
        _speed = Random.Range(minSpeed, maxSpeed);
    }
}
