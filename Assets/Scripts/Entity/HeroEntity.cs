using GamePlay.Components;
using GamePlay.Custom.GameMachine;
using GamePlay.Hero;
using Lessons.Character.Components;
using UnityEngine;

namespace Entity
{
    public class HeroEntity : Entity, IInitListener
    {
        [SerializeField]
        private HeroModel model;
        void IInitListener.OnInit()
        {
            Add(new MoveComponent(model.Core.MoveComp.OnMove));
            Add(new MoveInDirectionComponent(model.Core.CharacterMoveComp.movementDirection));
            Add(new TakeDamageRequestComponent(model.Core.lifeSectionComp.TakeDamageRequest));
            Add(new RotateComponent(model.Core.RotateComp.RotationDirection));
            Add(new DeathEventComponent(model.Core.lifeSectionComp.DeathEvent));
            Add(new ShootComponent(model.Core.ShootComp.OnGetPressedFire));
        }
    }
}
