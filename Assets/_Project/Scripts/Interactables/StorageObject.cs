using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Interactables
{
    public class StorageObject : Interactable, IInteractionPoint
    {
        [SerializeField] private List<ItemCategory> _categoriesAllowed = new List<ItemCategory>();
        [SerializeField] private int _stackCapacity = 100;
        [SerializeField] private List<StorageMount> _itemMounts = null;
        [SerializeField] private bool _displayItems = true;

        public List<StorageMount> ItemMounts => _itemMounts;

        private void Start()
        {
            BuildingManager.Instance.RegisterStorageObject(this);

            if(CanStoreCategory(ItemCategory.Ingredient))
            {
                AddItem(ItemGenerator.GenerateIngredient("Wood Log", 10));
                AddItem(ItemGenerator.GenerateIngredient("Berry", 10));
            }
        }

        public override void Interact(Unit interacter)
        {
            
        }

        public int FindMatchingSlot(Item item)
        {
            if (item.ItemDefinition.Stackable == false) return -1;
            
            int index = -1;

            for (int i = 0; i < _itemMounts.Count; i++)
            {
                if (_itemMounts[i].Item.IsEqual(item))
                {
                    index = i;
                    break;
                }
            }
            
            return index;
        }
        
        public int FindEmptySlot()
        {
            int index = -1;

            for (int i = 0; i < _itemMounts.Count; i++)
            {
                if (_itemMounts[i].Item.IsEmpty())
                {
                    index = i;
                    break;
                }
            }
            
            return index;
        }

        public void AddItem(Item item, int amountToAdd = 0)
        {
            if (item == null || item.Key == "") return;
            
            int sameSlot = FindMatchingSlot(item);
            
            if (sameSlot != -1)
            {
                _itemMounts[sameSlot].AddItemsToStack(amountToAdd);
                StockpileManager.Instance.AddToStack(item, amountToAdd);
            }
            else
            {
                int emptySlot = FindEmptySlot();

                if (emptySlot != -1)
                {
                    _itemMounts[emptySlot].MountItem(item, _displayItems);
                    StockpileManager.Instance.AddItem(item);
                }
                else
                {
                    Debug.Log(name + " full");
                }
            }
            
            StockpileManager.Instance.SyncStockpile();
        }

        public bool CanStoreCategory(ItemCategory category)
        {
            bool canStore = false;
            
            for (int i = 0; i < _categoriesAllowed.Count; i++)
            {
                if (_categoriesAllowed[i] == category)
                {
                    canStore = true;
                }
            }

            return canStore;
        }
        
        public void RemoveItem(Item item, int amountToRemove)
        {
            if (item == null || item.Key == "") return;
            
            int sameSlot = FindMatchingSlot(item);

            if (sameSlot != -1)
            {
                _itemMounts[sameSlot].RemoveItemsFromStack(amountToRemove, _displayItems);
                StockpileManager.Instance.RemoveItem(item, amountToRemove);
                StockpileManager.Instance.SyncStockpile();
            }
        }

        public bool HasItem(Item item, int amount)
        {
            bool hasItem = false;
            
            for (int i = 0; i < _itemMounts.Count; i++)
            {
                if (_itemMounts[i].Item.IsEqual(item))
                {
                    if (_itemMounts[i].Item.StackSize >= amount)
                    {
                        hasItem = true;
                        break;
                    }
                }
            }

            return hasItem;
        }

        public bool HasItemType(ItemType itemType, int amount)
        {
            bool hasItem = false;
            
            for (int i = 0; i < _itemMounts.Count; i++)
            {
                if(_itemMounts[i].Item == null || _itemMounts[i].Item.Key == "") continue;
                
                if (_itemMounts[i].Item.ItemDefinition.ItemType == itemType)
                {
                    if (_itemMounts[i].Item.StackSize >= amount)
                    {
                        hasItem = true;
                        break;
                    }
                }
            }

            return hasItem;
        }

        public Item FindItemOfType(ItemType type, int amount)
        {
            Item item = null;
            
            for (int i = 0; i < _itemMounts.Count; i++)
            {
                if (_itemMounts[i].Item.ItemDefinition.ItemType == type)
                {
                    if (_itemMounts[i].Item.StackSize >= amount)
                    {
                        item = _itemMounts[i].Item;
                        
                        break;
                    }
                }
            }

            return item;
        }

        public Transform GetInteractionPoint()
        {
            return _interactionTransform;
        }
    }
}