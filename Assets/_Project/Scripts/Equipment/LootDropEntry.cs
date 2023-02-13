using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Equipment
{
    [System.Serializable]
    public class LootDropEntry
    {
        [SerializeField] private ItemShort _itemShort = null;
        [SerializeField] private int _minNumberItems = 0;
        [SerializeField] private int _maxNumberItems = 0;
        [SerializeField] private int _spawnChance = 100;

        public ItemShort ItemShort => _itemShort;
        public int MinNumberItems => _minNumberItems;
        public int MaxNumberItems => _maxNumberItems;
        public int SpawnChance => _spawnChance;
    }
}