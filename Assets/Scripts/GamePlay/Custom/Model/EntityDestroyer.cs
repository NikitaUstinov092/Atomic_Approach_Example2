using GamePlay.Components;
using GamePlay.Custom.GameMachine;
using UnityEngine;

namespace GamePlay.Custom.Model
{
    public class EntityDestroyer : MonoBehaviour, IStartListener
    {
        [SerializeField]
        private Entity.Entity _entity;
        void IStartListener.StartGame()
        {
            if(_entity.TryGet(out IGetDeathEventComponent deathEventComponent))
            {
                deathEventComponent.GetDeathEvent().Subscribe(DestroyEntity);
            }
        }
        private void DestroyEntity()
        {
            Destroy(_entity);
        }
    }
}
