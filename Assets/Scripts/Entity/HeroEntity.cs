using GamePlay.Components;
using GamePlay.Hero;
using UnityEngine;

namespace Entity
{
    public class HeroEntity : Entity
    {
        [SerializeField]
        private HeroModel model;
        private void Awake()
        {
            Add(new MoveComponent(model.Core.MoveComp.OnMove));
            Add(new TakeDamageRequestComponent(model.Core.lifeSectionComp.TakeDamageRequest));
            Add(new RotateComponent(model.Core.RotateComp.RotationDirection));
            Add(new DeathEventComponent(model.Core.lifeSectionComp.DeathEvent));
            Add(new ShootComponent(model.Core.ShootComp.OnGetPressedFire));
        }
    }
}
