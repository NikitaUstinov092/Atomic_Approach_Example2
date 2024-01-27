using System.Atomic.Implementations;
using UnityEngine;
using System.Collections;
using GamePlay.Custom.Model;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Entity.Entity _player;
    
    [SerializeField]
    private Entity.Entity _enemy;

    [SerializeField] 
    private string _parentName = "Enemies";
   
    [SerializeField] 
    private Transform[] _spawnPoints;

    [SerializeField] 
    private float _delaySpawn = 2;

    [Inject]
    private EnemyFactory _enemyFactory;

    private AtomicVariable<int> _playerHealth;

    private GameObject _parent;
    
    private IEnumerator Start()
    {
        if(_player.TryGet(out IGetHealth getHealth))
        {
            _playerHealth = getHealth.GetHealth();
        }

        _parent = new GameObject(_parentName);
        
        while (PlayerAlive())
        {
            var spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
          
            _enemyFactory.CreateEnemy(_enemy, _player, spawnPosition.position, _parent.transform);
            
            yield return new WaitForSeconds(_delaySpawn);
        }
    }
    
    private bool PlayerAlive()
    {
        return _playerHealth.Value > 0;
    }
}
