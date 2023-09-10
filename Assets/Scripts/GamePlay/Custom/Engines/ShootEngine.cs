using GamePlay.Custom.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Custom.Engines
{
    public class ShootEngine: MonoBehaviour
    {
        private Transform _spawnPoint;
        private Rigidbody _bullet;
        private BulletConfig _config;
        private GameObject _parent;
        
        private float _shootSpeed;
        private float _nextFireTime;
        
        public void Construct(BulletConfig bulletConfig, Transform spawnPoint)
        {
            _shootSpeed = bulletConfig.SpeedShoot;
            _bullet = bulletConfig.Bullet;
            _spawnPoint = spawnPoint;
            
            CreateParent();
        }
        private void CreateParent()
        {
            _parent = new GameObject("Bullets");
        }
        
        [Button]
        public void CreateBullet()
        {
            var bullet = Instantiate(_bullet,_spawnPoint.position, _spawnPoint.rotation);
            bullet.transform.parent = _parent.transform;
            Shoot(bullet);
        }
        
        [Button]
        private void Shoot(Rigidbody bullet)
        {
            var rotation = _spawnPoint.rotation;
            var localShootDirection = rotation * Vector3.forward;
            bullet.velocity = localShootDirection * _shootSpeed;
        }
    }
}
