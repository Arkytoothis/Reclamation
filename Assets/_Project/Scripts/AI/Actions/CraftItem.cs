using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class CraftItem : Action
    {
        private CraftingStation _craftingStation = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _craftingStation = BuildingManager.Instance.FindClosestCraftingStationWithRecipe(transform);
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            if (_craftingStation == null || _resourceController == null || _craftingStation.CurrentRecipe == null || _craftingStation.RecipeOrders == 0)
            {
                return false;
            }

            if (_craftingStation.HasIngredients() == true)
            {
                _target = _craftingStation.gameObject;
                _targetController.AddTarget(_target);

                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool PostPerform()
        {
            _craftingStation.FinishRecipe();
            _targetController.RemoveTarget(_target);
            
            return true;
        }
    }
}