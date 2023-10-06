using System;
using System.Collections;
using Entity;
using GamePlay.Components.Interfaces;
using GamePlay.Custom.GameMachine;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GamePlay.Custom
{
    public class EnemyFactory : MonoBehaviour, IStartListener, IEntityFactory<Entity.Entity>
    {
        public event Action<Entity.Entity> OnEntityCreated;

        [Inject]
        private IGetEntityComponent _targetEntityComp;
       
        [SerializeField] 
        private string _parentName = "Enemies";
        
        [SerializeField] 
        private Entity.Entity _enemy;

        [SerializeField] 
        private Transform[] _spawnPoints;

        [SerializeField] 
        private float _delaySpawn = 2;

        private GameObject _parent;
        
        private Entity.Entity _targetEntity;

        void IStartListener.StartGame()
        {
            _targetEntity = _targetEntityComp.GetEntity();
            _parent = new GameObject(_parentName);
            StartCoroutine(Spawn());
        }
        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(_delaySpawn);
            
            while (TargetState())
            {
                var spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
                var spawnEntity =  Instantiate(_enemy, spawnPosition.position,Quaternion.identity);
                spawnEntity.transform.parent = _parent.transform;
                
                AddTarget(spawnEntity);
                
                OnEntityCreated?.Invoke(spawnEntity);
                
                yield return new WaitForSeconds(_delaySpawn);
            }
        }
        
        private void AddTarget(Entity.Entity enemy)
        {
            if(enemy.TryGet(out ISetEntityTargetComponent setEntityComp))
                setEntityComp.SetEntityTarget(_targetEntity);
            else
                throw new NullReferenceException();
        }

        private bool TargetState()
        {
           return _targetEntity != null; 
        }
    }

    public interface IEntityFactory<T>
    {
       event Action<T> OnEntityCreated;
    }
}
