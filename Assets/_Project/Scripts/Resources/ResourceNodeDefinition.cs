using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Resources
{
    [CreateAssetMenu(fileName = "Resource Node Definition", menuName = "Reclamation/Definition/Resource Node Definition")]
    public class ResourceNodeDefinition : ScriptableObject
    {
        [SerializeField] private string _name = "";
        [SerializeField] private string _key = "";
        [SerializeField] private int _minDrops = 1;
        [SerializeField] private int _maxDrops = 1;
        [SerializeField] private int _specialNodeChance = 0;
        [SerializeField] private float _minDistance = 0;
        [SerializeField] private GameObject _nodePrefab = null;
        [SerializeField] private GameObject _dropPrefab = null;

        public string Name => _name;
        public string Key => _key;
        public int MinDrops => _minDrops;
        public int MaxDrops => _maxDrops;
        public float MinDistance => _minDistance;

        public int SpecialNodeChance => _specialNodeChance;
        public GameObject NodePrefab => _nodePrefab;
        public GameObject DropPrefab => _dropPrefab;
    }
}