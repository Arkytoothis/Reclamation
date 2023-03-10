using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Interactables
{
    [System.Serializable]
    public class StorageMount
    {
        [SerializeField] private Item _item = null;
        [SerializeField] private Transform _mount = null;

        public Item Item => _item;

        public StorageMount()
        {
            _item = null;
            _mount.ClearTransform();
        }

        public void MountItem(Item item, bool displayItem)
        {
            _item = new Item(item);

            if (displayItem == true)
            {
                GameObject clone = GameObject.Instantiate(item.ItemDefinition.StorageModel, _mount);
            }
        }

        public void AddItemsToStack(int itemsToAdd)
        {
            _item.StackSize += itemsToAdd;
        }

        public void RemoveItemsFromStack(int itemsToRemove, bool displayItem)
        {
            _item.StackSize -= itemsToRemove;

            if (displayItem == true && _item.StackSize <= 0)
            {
                Clear();
            }
        }

        private void Clear()
        {
            _mount.ClearTransform();
            _item = new Item();
        }
    }
}