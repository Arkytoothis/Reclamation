using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class DropoffItemForCraft : Action
    {
        private UnitResourceController _resourceController = null;
        private CraftingStation _craftingStation = null;
        
        public override bool PrePerform()
        {
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            if (_resourceController == null) return false;

            _craftingStation = _targetController.FindTarget<CraftingStation>();

            if (_craftingStation != null)
            {
                _target = _craftingStation.gameObject;
            
                if (_target == null)
                {
                    Debug.Log("No Target Found");
                    return false;
                }
            }
            else
            {
                Debug.Log("No Crafting Station Found");
                return false;
            }
            
            return true;
        }

        public override bool PostPerform()
        {
            if (_craftingStation.HasIngredients() == true)
            {
                _targetController.RemoveTarget(_target);
                
                return false;
            }
            else
            {
                _craftingStation.AddIngredient(_resourceController.ItemCarried.ItemDefinition, 1);
                _resourceController.ClearCarriedItem();
                _targetController.RemoveTarget(_target);
                
                return true;
            }
        }
    }
}