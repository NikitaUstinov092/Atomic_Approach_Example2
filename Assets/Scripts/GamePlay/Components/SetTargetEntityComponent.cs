using GamePlay.Components.Interfaces;

namespace GamePlay.Components
{
    public class SetTargetEntityComponent: ISetEntityTargetComponent
    {
        private readonly TargetEntitySection _target;
    
        public SetTargetEntityComponent(TargetEntitySection target)
        {
            _target = target;
        }
    
        void ISetEntityTargetComponent.SetEntityTarget(Entity.Entity entity)
        {
            _target.TargetEntity.Value = entity;
        }
    }
}
