using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using Reclamation.Resources;
using UnityEngine;
using ItemDefinition = Reclamation.Equipment.ItemDefinition;

namespace Reclamation.Craft
{
    [System.Serializable]
    public class RecipeIngredient
    {
        [SerializeField] private ItemDefinition _item = null;
        [SerializeField] protected int _numberItems = 0;

        public ItemDefinition Item => _item;
        public int NumberItems => _numberItems;

        public RecipeIngredient(ItemDefinition item, int numberItems)
        {
            _item = item;
            _numberItems = numberItems;
        }

        public void AddIngredients(int amount)
        {
            _numberItems += amount;
        }

        public void RemoveIngredients(int amount)
        {
            _numberItems -= amount;
        }

        public void Clear()
        {
            _numberItems = 0;
        }
    }
}