using System;
using GamePlay.Hero;
using Lessons.StateMachines.States;

[Serializable]
public class AutoShootState : UpdateState
{
    private HeroModel_Core.TargetEnemyContainer _enemyContainer;
    private HeroModel_Core.Rotate _rotate;
    private HeroModel_Core.Shoot _shoot;
    private const float TimeDelay = 3f;
    private float _timer;
    public void Construct(HeroModel_Core.TargetEnemyContainer enemyContainer,
        HeroModel_Core.Rotate rotate, HeroModel_Core.Shoot shoot)
    {
        _enemyContainer = enemyContainer;
        _rotate = rotate;
        _shoot = shoot;
    }
    protected override void OnUpdate(float deltaTime)
    {
        /*if (_enemyContainer.Enemy.Value != null)
            _rotate.RotationDirection.Value = _enemyContainer.Enemy.Value.transform.position;
        else
            return;*/
        
        _timer += deltaTime;
        
        if (_timer < TimeDelay)
            return;
        
        _shoot.OnGetPressedFire.Invoke();
        _timer = 0;
    }
}
