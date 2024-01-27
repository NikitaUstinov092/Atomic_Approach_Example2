using Entity;
using GamePlay.Custom.Input;
using GamePlay.Custom.Model;
using GamePlay.Custom.View;
using Zenject;

namespace GamePlay.Custom.GameMachine
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<HeroEntity>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyCleaner>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<EnemyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyServiceAdapter>().AsSingle();
            Container.BindInterfacesTo<KillsCounter>().AsSingle();
            Container.BindInterfacesTo<KillsCountView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<ShootInput>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<EntityDestroyer>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<MovementInput>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<CharacterMovementController>().FromComponentsInHierarchy().AsSingle();
        }
    }
}