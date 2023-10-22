using System;
using Game.GameEngine.Animation;
using GamePlay.Custom.Engines;
using Lessons.StateMachines.States;

[Serializable]
public class AutoShootState : UpdateState
{
    private TargetEntitySection _entitySection;
    private ShootController _shootController;

    private AnimatorState_ListenEvent _shootListener;
    
    private const float RotationPause = 1f; //TO DO вычислять дельту между углом поворота к противнику и скоростью героя, пока по KISS
    private float _timer;
    
    public void Construct(TargetEntitySection entitySection,
        ShootController shootController, AnimatorStateMachine<AnimatorStateType> animStateMachine)
    {
        _entitySection = entitySection;
        _shootController = shootController;
    }
    
    
    
    protected override void OnUpdate(float deltaTime)
    {
        if (_entitySection.TargetEntity.Value == null)
            return;
        
        _timer += deltaTime;
        
        if (_timer < RotationPause)
            return;
        
        _shootController.FireRequest.Invoke();
        _timer = 0;
    }

    private void UpdateShootTimer()
    {
        _timer = 0;
    }
}
