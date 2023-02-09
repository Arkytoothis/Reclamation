using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Core;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class FindBlueprintToBuild : Action
    {
        private Blueprint _blueprint = null;
        private StorageObject _storageObject = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _blueprint = BuildingManager.Instance.FindClosestBlueprintThatNeedsResources(transform);
            _resourceController = GetComponentInParent<Hero>().ResourceController;
            
            if(_blueprint == null || _resourceController == null) return false;

            _storageObject = BuildingManager.Instance.FindClosestStorageObject(_blueprint.transform, ItemCategory.Ingredient);
            _resourceController.SetRequiredItem(_blueprint.GetFirstRequiredIngredient(), 1);
            
            if (_resourceController.ItemRequired == null) return false;
            
            if (_storageObject.HasItem(_resourceController.ItemRequired, 1))
            {
                _targetController.AddTarget(_blueprint.gameObject);
                _targetController.AddTarget(_storageObject.gameObject);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool PostPerform()
        {
            if (_blueprint.HasIngredients() == true)
            {
                Debug.Log("Blueprint Has Ingredients");
                _target = _blueprint.gameObject;
                _targetController.AddTarget(_target);

                return true;
            }
            else
            {
                Debug.Log("Blueprint Needs Ingredients");
                _targetController.AddTarget(_blueprint.gameObject);
                
                return true;
            }
        }
    }
}