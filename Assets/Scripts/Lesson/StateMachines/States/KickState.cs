using Lessons.StateMachines.States;
public class KickState : IState
{
    private AttackEngine _attackEngine;
    public void Construct(AttackEngine attackEngine)
    {
        _attackEngine = attackEngine;
    }
    void IState.Enter()
    {
        _attackEngine.Attack();
    }
    void IState.Exit()
    {
        _attackEngine.StopAttack();
    }
}
