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
        [SerializeField] private int _numberItems = 0;
        [SerializeField] private int _spawnChance = 100;

        public ItemShort ItemShort => _itemShort;
        public int NumberItems => _numberItems;
        public int SpawnChance => _spawnChance;
    }
}