using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Attributes
{
    [CreateAssetMenu(fileName = "Enemy Definition", menuName = "Reclamation/Definition/Enemy Definition")]
    public class EnemyDefinition : ScriptableObject
	{
        [SerializeField] private string _name = "";
        [SerializeField] private string _key = "";
        [SerializeField] private StartingCharacteristicDictionary _startingCharacteristics = null;
        [SerializeField] private StartingVitalDictionary _startingVitals = null;
        [SerializeField] private StartingStatisticDictionary _startingStatistics = null;
        [SerializeField] private StartingSkillDictionary _startingSkills = null;
        [SerializeField] private List<Resistance> _resistances = null;
        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private List<LootDropEntry> _lootDrops = null;

        public string Name => _name;
        public string Key => _key;
        public StartingCharacteristicDictionary StartingCharacteristics => _startingCharacteristics;
        public StartingVitalDictionary StartingVitals => _startingVitals;
        public StartingStatisticDictionary StartingStatistics => _startingStatistics;
        public StartingSkillDictionary StartingSkills => _startingSkills;
        public List<Resistance> Resistances => _resistances;
        public GameObject Prefab => _prefab;
        public List<LootDropEntry> LootDrops => _lootDrops;
    }
}