using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class DropoffItemForBlueprint : Action
    {
        private UnitResourceController _resourceController = null;
        private Blueprint _blueprint = null;
        
        public override bool PrePerform()
        {
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            if (_resourceController == null) return false;

            _blueprint = _targetController.FindTarget<Blueprint>();

            if (_blueprint != null)
            {
                _target = _blueprint.gameObject;
            
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
            if (_blueprint.HasIngredients() == true)
            {
                _targetController.RemoveTarget(_target);
                
                return false;
            }
            else
            {
                _blueprint.AddIngredient(_resourceController.ItemCarried.ItemDefinition, 1);
                _resourceController.ClearCarriedItem();
                _targetController.RemoveTarget(_target);
                
                return true;
            }
        }
    }
}