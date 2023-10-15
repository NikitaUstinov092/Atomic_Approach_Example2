using System.Atomic.Implementations;
using GamePlay.Components;
using GamePlay.Custom.GameMachine;
using GamePlay.Hero;
using UnityEngine;

namespace Entity
{
    public class HeroEntity : Entity, IInitListener, IGetEntityComponent
    {
        [SerializeField]
        private HeroModel model;
        void IInitListener.OnInit()
        {
            Add(new MoveInDirectionComponent(model.Core.CharacterMoveComp.MovementDirection));
            Add(new TakeDamageRequestComponent(model.Core.LifeSectionComp.TakeDamageRequest));
            Add(new DeathEventComponent(model.Core.LifeSectionComp.DeathEvent, model.Core.LifeSectionComp.DeathEventData));
            Add(new ShootComponent(model.Core.ShootComp.ShootController.FireRequest));
            Add(new SetTargetEntityComponent(model.Core.EntityTarget));
        }

        Entity IGetEntityComponent.GetEntity()
        {
            return this;
        }
    }


    public interface IGetEntityComponent
    {
        Entity GetEntity();
    }
}
