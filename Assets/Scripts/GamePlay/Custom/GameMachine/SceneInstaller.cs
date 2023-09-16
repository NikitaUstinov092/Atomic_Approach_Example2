using Entity;
using GamePlay.Custom.Input;
using GamePlay.Custom.View;
using Lessons.Character.Controllers;
using Zenject;

namespace GamePlay.Custom.GameMachine
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HeroEntity>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyCleaner>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<KillsCounter<Entity.Entity>>().AsSingle();
            Container.BindInterfacesTo<KillsCountView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<MoveInput>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<NewInput>().FromComponentsInHierarchy().AsSingle(); //To DO удалить
            Container.BindInterfacesTo<CharacterMovementController>().FromComponentsInHierarchy().AsSingle(); //To DO удалить
            Container.BindInterfacesTo<RotationInput>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<ShootInput>().FromComponentsInHierarchy().AsSingle();
        }
    
    }
}