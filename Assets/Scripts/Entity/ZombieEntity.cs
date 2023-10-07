using GamePlay.Components;
using GamePlay.Zombie;
using UnityEngine;

namespace Entity
{
    public class ZombieEntity: Entity
    {
        [SerializeField]
        private ZombieModel model;
        private void Awake()
        {
            Add(new DeathEventComponent(model.Core.LifeSection.DeathEvent, model.Core.LifeSection.DeathEventData));
            Add(new TakeDamageRequestComponent(model.Core.LifeSection.TakeDamageRequest));
            Add(new SetTargetEntityComponent(model.Core.TargetSection));
        }
    }
}
