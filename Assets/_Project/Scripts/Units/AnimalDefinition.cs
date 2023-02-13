using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Reclamation.Attributes
{
    [CreateAssetMenu(fileName = "Animal Definition", menuName = "Reclamation/Definition/Animal Definition")]
    public class AnimalDefinition : ScriptableObject
	{
        [SerializeField] private string _name = "";
        [SerializeField] private string _key = "";
        [SerializeField] private StartingCharacteristicDictionary _startingCharacteristics = null;
        [SerializeField] private StartingVitalDictionary _startingVitals = null;
        [SerializeField] private StartingStatisticDictionary _startingStatistics = null;
        [SerializeField] private List<Resistance> _resistances = null;
        [SerializeField] private List<LootDropEntry> _lootDrops = null;

        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private AnimatorOverrideController _animatorOverride = null;

        public string Name => _name;
        public string Key => _key;
        public StartingCharacteristicDictionary StartingCharacteristics => _startingCharacteristics;
        public StartingVitalDictionary StartingVitals => _startingVitals;
        public StartingStatisticDictionary StartingStatistics => _startingStatistics;
        public List<Resistance> Resistances => _resistances;
        public GameObject Prefab => _prefab;
        public AnimatorOverrideController AnimatorOverride => _animatorOverride;
        public List<LootDropEntry> LootDrops => _lootDrops;

        [Button("Create Attributes")]
        public void CreateAttributes()
        {
            _startingCharacteristics.Clear();
            
            foreach (KeyValuePair<string,AttributeDefinition> kvp in Database.instance.Attributes.Attributes)
            {
                _startingCharacteristics.Add(kvp.Key, new StartingCharacteristic(kvp.Value, 0, 0));
            }
            
            _startingVitals.Clear();
            
            foreach (KeyValuePair<string,AttributeDefinition> kvp in Database.instance.Attributes.Vitals)
            {
                _startingVitals.Add(kvp.Key, new StartingVital(kvp.Value, 0, 0));
            }
            
            _startingStatistics.Clear();
            
            foreach (KeyValuePair<string,AttributeDefinition> kvp in Database.instance.Attributes.Statistics)
            {
                _startingStatistics.Add(kvp.Key, new StartingStatistic(kvp.Value, 0, 0));
            }
        }
    }
}