using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class FindRecipeToCraft : Action
    {
        private CraftingStation _craftingStation = null;
        private StorageObject _storageObject = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _craftingStation = BuildingManager.Instance.FindClosestCraftingStationWithRecipe(transform);
            _resourceController = GetComponentInParent<Hero>().ResourceController;
            
            if(_craftingStation == null || _resourceController == null) return false;
            if (_craftingStation.CurrentRecipe == null || _craftingStation.RecipeOrders == 0) return false;

            _resourceController.SetRequiredItem(_craftingStation.GetFirstRequiredIngredient(), 1);
            _storageObject = BuildingManager.Instance.FindClosestStorageObject(_craftingStation.transform, _resourceController.ItemRequired.ItemDefinition.Category);
            
            if (_resourceController.ItemRequired == null) return false;
            
            if (_storageObject.HasItem(_resourceController.ItemRequired, 1))
            {
                _targetController.AddTarget(_craftingStation.gameObject);
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
            if (_craftingStation.HasIngredients() == true)
            {
                Debug.Log("Recipe Has Ingredients");
                _target = _craftingStation.gameObject;
                _targetController.AddTarget(_target);

                return true;
            }
            else
            {
                Debug.Log("Recipe Needs Ingredients");
                _targetController.AddTarget(_craftingStation.gameObject);
                
                return true;
            }
        }
    }
}