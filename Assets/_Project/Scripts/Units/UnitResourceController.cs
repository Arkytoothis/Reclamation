using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Units
{
    public class UnitResourceController : MonoBehaviour
    {
        [SerializeField] private Item _itemRequired = null;
        [SerializeField] private Item _itemCarried = null;
        [SerializeField] private int _maxCarried = 0;

        public Item ItemRequired => _itemRequired;
        public Item ItemCarried => _itemCarried;
        public int MaxCarried => _maxCarried;


        public void PickupItem(Item item, int amount)
        {
            _itemCarried = new Item(item);
            _itemCarried.StackSize = amount;
        }

        public void SetRequiredItem(Item item, int numberItemsRequired)
        {
            if (item != null)
            {
                _itemRequired = new Item(item);
                _itemRequired.StackSize = numberItemsRequired;
            }
        }

        public void ClearCarriedItem()
        {
            _itemCarried = new Item();
        }

        public void ClearRequiredItem()
        {
            _itemRequired = new Item();
        }
    }
}