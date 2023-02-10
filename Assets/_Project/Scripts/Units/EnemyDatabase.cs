using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Attributes
{
    [CreateAssetMenu(fileName = "Enemy Database", menuName = "Reclamation/Database/Enemy Database")]
    public class EnemyDatabase : ScriptableObject
	{
        [SerializeField] private EnemyDefinitionDictionary _enemies = null;

        public EnemyDefinitionDictionary Enemies { get => _enemies; }
        
        public EnemyDefinition GetEnemy(string key)
        {
            return _enemies[key];
        }
    }
}