using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Craft;
using Reclamation.Resources;
using UnityEngine;

namespace Reclamation.Equipment
{
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField] private ItemShort _itemToDrop = null;
        [SerializeField] private Item _item = null;
        [SerializeField] private bool _isTarget = false;

        public Item Item => _item;

        public bool IsTarget
        {
            get => _isTarget;
            set => _isTarget = value;
        }

        public void Setup(int stackSize)
        {
            if (_itemToDrop.Item.Category == ItemCategory.Ingredient)
            {
                _item = ItemGenerator.GenerateIngredient(_itemToDrop.Item.Key, stackSize);
            }
            else if (_itemToDrop.Item.Category == ItemCategory.Weapons)
            {
                _item = ItemGenerator.GenerateItem(_itemToDrop);
            }
        }

        private void Start()
        {
            StartCoroutine(Register());
        }

        private IEnumerator Register()
        {
            yield return new WaitForSeconds(2f);
            
            ResourcesManager.Instance.RegisterItemDrop(this);
        }
    }
}
