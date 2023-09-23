using System;
using GamePlay.Hero;
using Lessons.StateMachines.States;

[Serializable]
public class AutoShootState : UpdateState
{
    private TargetEntitySection _entitySection;
    private HeroModel_Core.Shoot _shoot;
    
    private const float TimeDelay = 2f;
    private float _timer;
    
    public void Construct(TargetEntitySection entitySection,
      HeroModel_Core.Shoot shoot)
    {
        _entitySection = entitySection;
        _shoot = shoot;
    }
    protected override void OnUpdate(float deltaTime)
    {
        if (_entitySection.TargetEntity == null)
            return;
        
        _timer += deltaTime;
        
        if (_timer < TimeDelay)
            return;
        
        _shoot.OnGetPressedFire.Invoke();
        _timer = 0;
    }
}
