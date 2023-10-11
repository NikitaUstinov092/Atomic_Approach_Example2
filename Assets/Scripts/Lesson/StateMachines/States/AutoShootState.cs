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

        ConstructShootListener(animStateMachine);
    }

    private void ConstructShootListener(AnimatorStateMachine<AnimatorStateType> animStateMachine)
    {
        _shootListener.ConstructAnimEvents("Shoot");
        _shootListener.ConstructAnimMachine(animStateMachine);
        _shootListener.ConstructAction(() =>
        {
            //звук и изменение анимации
        });
    }
    
    
    protected override void OnUpdate(float deltaTime)
    {
        if (_entitySection.TargetEntity.Value == null)
            return;
        
        _timer += deltaTime;
        
        if (_timer < RotationPause)
            return;
        
        _shootController.OnGetPressedFire.Invoke();
        _timer = 0;
    }

    protected override void OnEnter()
    {
        UpdateShootTimer();
        _entitySection.TargetEntity.Subscribe(_=> UpdateShootTimer());
    }
    
    protected override void OnExit()
    {
        _entitySection.TargetEntity.Unsubscribe(_=> UpdateShootTimer());
    }

    private void UpdateShootTimer()
    {
        _timer = 0;
    }
}
