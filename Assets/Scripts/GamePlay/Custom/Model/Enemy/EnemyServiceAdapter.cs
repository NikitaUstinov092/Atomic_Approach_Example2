using GamePlay.Custom.GameMachine;
using Zenject;

namespace GamePlay.Custom.Model
{
    public class EnemyServiceAdapter: IStartListener, IDisableListener
    {
        private EnemyFactory _enemyFactory;
        private EnemyService _enemyService;

        [Inject]
        private void Construct(EnemyFactory enemyFactory, EnemyService enemyService)
        {
            _enemyFactory = enemyFactory;
            _enemyService = enemyService;
        }

        void IStartListener.StartGame()
        {
            _enemyFactory.OnEntityCreated += _enemyService.AddEnemy;
        }

        void IDisableListener.Disable()
        {
            _enemyFactory.OnEntityCreated -= _enemyService.AddEnemy;
        }
    }
}