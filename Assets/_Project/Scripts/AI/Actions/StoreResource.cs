using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class StoreResource : Action
    {
        private StorageObject _storageObject = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            _storageObject = BuildingManager.Instance.FindClosestStorageObject(transform, _resourceController.ItemCarried.ItemDefinition.Category);

            if (_storageObject == null || _resourceController == null) return false;
            
            _target = _storageObject.gameObject;
            
            return true;
        }

        public override bool PostPerform()
        {
            _storageObject.AddItem(_resourceController.ItemCarried, 1);
            _resourceController.ClearCarriedItem();
            
            return true;
        }
    }
}