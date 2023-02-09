using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reclamation.Build
{
    [System.Serializable]
    public class BuildingIngredient
    {
        [SerializeField] private ItemDefinition _item = null;
        [SerializeField] private int _amount = 0;

        public ItemDefinition Item => _item;
        public int Amount => _amount;

        public BuildingIngredient(BuildingIngredient ingredient)
        {
            _item = ingredient._item;
            _amount = 0;
        }

        public void AddRIngredient(int amount)
        {
            _amount += amount;
        }
    }
}