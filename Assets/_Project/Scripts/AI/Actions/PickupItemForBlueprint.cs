using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Core;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class PickupItemForBlueprint : Action
    {
        private StorageObject _storageObject = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _storageObject = BuildingManager.Instance.FindClosestStorageObjectWithItemType(transform, ItemType.Food, 1);
            _resourceController = GetComponentInParent<Hero>().ResourceController;
            
            if(_storageObject == null || _resourceController == null) return false;

            _target = _storageObject.gameObject;

            return true;
        }

        public override bool PostPerform()
        {
            //TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.PickupItemForBlueprintState.Name, -1);
            _storageObject.RemoveItem(_resourceController.ItemCarried, 1);
            _resourceController.PickupItem(_resourceController.ItemRequired, 1);
            _targetController.RemoveTarget(_storageObject.gameObject);
            
            return true; 
        }
    }
}