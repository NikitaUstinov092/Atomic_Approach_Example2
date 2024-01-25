using System;
using System.Collections.Generic;
using System.Linq;


namespace GamePlay.Custom.Model
{
    [Serializable]
    public class EnemyService
    {
        private List<Entity.Entity> _enemies = new();

        public void AddEnemy(Entity.Entity enemy)
        {
            _enemies.Add(enemy);
        }

        public List<Entity.Entity> GetAllEnemies()
        {
            return _enemies.Where(enemy => enemy != enemy.Equals(null)).ToList();;
        }
    }
}