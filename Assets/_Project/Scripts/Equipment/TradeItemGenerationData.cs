﻿using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Equipment
{
    [System.Serializable]
    public class TradeItemGenerationData
    {
        [SerializeField] private GenerateItemType _itemType = GenerateItemType.None;
        [SerializeField] private int _numItems = 0;

        public GenerateItemType ItemType { get => _itemType; }
        public int NumItems { get => _numItems; }

        public TradeItemGenerationData(GenerateItemType itemType, int numItems)
        {
            _itemType = itemType;
            _numItems = numItems;
        }
    }
}