using GamePlay.Components.Interfaces;
using GamePlay.Zombie;

namespace GamePlay.Components
{
    public class SetTargetEntityComponent: ISetEntityTargetComponent
    {
        private readonly ZombieModel_Core.TargetChecker _targetChecker;
    
        public SetTargetEntityComponent(ZombieModel_Core.TargetChecker targetChecker)
        {
            _targetChecker = targetChecker;
        }
    
        void ISetEntityTargetComponent.SetEntityTarget(Entity.Entity entity)
        {
            _targetChecker.Target.Value = entity;
        }
    }
}
