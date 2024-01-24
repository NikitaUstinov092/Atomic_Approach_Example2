using System.Atomic.Implementations;
using System.Declarative.Scripts;
using GamePlay.Custom.Model;
using GamePlay.Hero;
using UnityEngine;

namespace GamePlay.AI
{
    public class EnemySelectMechanics: IUpdate
    {
        private EnemyService _enemyService;
        
        private AtomicVariable<Entity.Entity> _targetEnemy;
        private AtomicVariable<HeroModel> _hero;
        private AtomicVariable<bool> _autoMode;
        
        public void Construct(EnemyService enemyService, AtomicVariable<Entity.Entity> targetEnemy, AtomicVariable<HeroModel> hero, AtomicVariable<bool> autoMode)
        {
            _enemyService = enemyService;
            _targetEnemy = targetEnemy;
            _hero = hero;
            _autoMode = autoMode;
        }
        void IUpdate.Update(float deltaTime)
        {
            if (!_autoMode.Value)
            {
                return;
            }
            
            var enemies = _enemyService.GetAllEnemies();

            var closestDistance = float.MaxValue;
            
            foreach (var enemy in enemies)
            {
                if(enemy.Equals(null))
                    continue;
                
                var distance = Vector3.Distance(enemy.transform.position, _hero.Value.transform.position);
                
                if (!(distance < closestDistance))
                    continue;
                
                closestDistance = distance;
                _targetEnemy.Value = enemy;
            }
            
        }
    }
}