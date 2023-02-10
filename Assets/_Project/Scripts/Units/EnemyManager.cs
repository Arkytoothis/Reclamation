using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Units
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Enemy> _enemies = null;

        public List<Enemy> Enemies => _enemies;

        public static EnemyManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple EnemyManagers " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            _enemies = new List<Enemy>();
        }

        public void RegisterEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy) == false)
            {
                _enemies.Add(enemy);
            }
        }

        public Enemy FindClosestEnemy(Transform searcher)
        {
            _enemies.Sort(delegate(Enemy a, Enemy b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            return _enemies[0];
        }
    }
}