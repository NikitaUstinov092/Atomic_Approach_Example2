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
            Add(new TakeDamageRequestComponent(model.Core.lifeSection.TakeDamageRequest));
            Add(new SetTargetEntityComponent(model.Core.target));
            Add(new DeathEventComponent(model.Core.lifeSection.DeathEvent));
        }
    }
}
