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
            Add(new MoveComponent(model.Core.MoveComp.OnMove));
            Add(new TakeDamageRequestComponent(model.Core.LifeSectionComp.TakeDamageRequest));
            Add(new RotateComponent(model.Core.RotateComp.RotationDirection));
            Add(new DeathEventComponent(model.Core.LifeSectionComp.DeathEvent, model.Core.LifeSectionComp.DeathEventData));
            Add(new ShootComponent(model.Core.ShootComp.OnGetPressedFire));
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
