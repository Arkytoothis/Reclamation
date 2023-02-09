using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Reclamation.Units
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemyPrefabs = null;
        [SerializeField] private List<Enemy> _enemies = null;
        [SerializeField] private Transform _spawnPoint = null;
        [SerializeField] private Transform _enemiesParent = null;
        [SerializeField] private int _maxEnemies = 0;
        [SerializeField] private float _startSpawningDelay = 5f;
        [SerializeField] private float _delayBetweenSpawns = 5f;

        private void Start()
        {
            StartCoroutine(SpawnerCoroutine());
        }

        private void SpawnEnemy(Vector3 position)
        {
            int enemyIndex = Random.Range(0, _enemyPrefabs.Count);
            GameObject clone = Instantiate(_enemyPrefabs[enemyIndex], _enemiesParent);
            clone.transform.position = position;
            
            Enemy enemy = clone.GetComponent<Enemy>();
            enemy.SetupEnemy(_enemies.Count);
            _enemies.Add(enemy);
        }

        private IEnumerator SpawnerCoroutine()
        {
            yield return new WaitForSeconds(_startSpawningDelay);

            while (true)
            {
                if (_enemies.Count < _maxEnemies)
                {
                    SpawnEnemy(_spawnPoint.position);
                }
                
                yield return new WaitForSeconds(_delayBetweenSpawns);
            }
        }
    }
}