using System;
using GamePlay.Custom.Engines;
using Lessons.StateMachines.States;

[Serializable]
public class AutoShootState : UpdateState
{
    private TargetEntitySection _entitySection;
    private ShootController _shootController;
    
    private const float TimeDelay = 2f;
    private float _timer;
    
    public void Construct(TargetEntitySection entitySection,
        ShootController shootController)
    {
        _entitySection = entitySection;
        _shootController = shootController;
    }
    protected override void OnUpdate(float deltaTime)
    {
        if (_entitySection.TargetEntity == null)
            return;
        
        _timer += deltaTime;
        
        if (_timer < TimeDelay)
            return;
        
        _shootController.OnGetPressedFire.Invoke();
        _timer = 0;
    }
}
