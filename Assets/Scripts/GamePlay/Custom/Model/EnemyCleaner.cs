using System.Collections;
using GamePlay.Components;
using GamePlay.Custom.GameMachine;
using UnityEngine;
using Zenject;

namespace GamePlay.Custom
{
    public class EnemyCleaner: MonoBehaviour, IInitListener, IDisableListener
    {
        [Inject]
        private IEnemyFactory<Entity.Entity> _enemyFactory;

        [SerializeField]
        private float _destroyDelay = 2f;

        void IInitListener.OnInit()
        {
            _enemyFactory.OnEnemyCreated += HandleEnemyCreated;
        }

        void IDisableListener.Disable()
        {
            _enemyFactory.OnEnemyCreated -= HandleEnemyCreated;
        }

        private void HandleEnemyCreated(Entity.Entity enemy)
        {
            if (enemy.TryGet(out IGetDeathEventComponent lifeComp))
                lifeComp.GetDeathEvent().Subscribe(() => StartCoroutine(DestroyEnemyAfterDelay(enemy.gameObject)));
        }

        private IEnumerator DestroyEnemyAfterDelay(Object enemyObject)
        {
            yield return new WaitForSeconds(_destroyDelay);
            Destroy(enemyObject);
        }
    }
}

