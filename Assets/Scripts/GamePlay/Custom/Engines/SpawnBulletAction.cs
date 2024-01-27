﻿using System.Atomic.Interfaces;
using GamePlay.Custom.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Custom.Engines
{
    public class SpawnBulletAction: IAtomicAction
    {
        private Transform _spawnPoint;
        private Rigidbody _bullet;
        private GameObject _parent;
        
        private float _shootSpeed;
        
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
        private void Shoot(Rigidbody bullet)
        {
            var rotation = _spawnPoint.rotation;
            var localShootDirection = rotation * Vector3.forward;
            bullet.velocity = localShootDirection * _shootSpeed;
        }

        public void Invoke()
        {
            var bullet = Object.Instantiate(_bullet,_spawnPoint.position, _spawnPoint.rotation);
            bullet.transform.parent = _parent.transform;
            Shoot(bullet);
        }
    }
}