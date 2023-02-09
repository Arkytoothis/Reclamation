using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class PickupFoodToEat : Action
    {
        private StorageObject _storageObject = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _storageObject = BuildingManager.Instance.FindClosestStorageObjectWithItemType(transform, ItemType.Food, 1);
            _resourceController = GetComponentInParent<Hero>().ResourceController;
            
            if(_storageObject == null || _resourceController == null) return false;

            Item foodItem = _storageObject.FindItemOfType(ItemType.Food, 1);

            if (foodItem == null) return false;
            
            _resourceController.SetRequiredItem(foodItem, 0);
            _target = _storageObject.gameObject;

            return true;
        }

        public override bool PostPerform()
        {
            _storageObject.RemoveItem(_resourceController.ItemRequired, 1);
            _resourceController.PickupItem(_resourceController.ItemRequired, 1);
            _targetController.RemoveTarget(_storageObject.gameObject);
            
            return true; 
        }
    }
}