using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class GoToEatingSpot : Action
    {
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            if (_resourceController == null) return false;

                _target = BuildingManager.Instance.IdleSpot;
            
                if (_target == null)
                {
                    Debug.Log("No Target Found");
                    return false;
                }
            
            return true;
        }

        public override bool PostPerform()
        {
            _targetController.RemoveTarget(_target);
            _beliefs.RemoveState(StateManager.Instance.NeedFoodState.Name);
            _resourceController.ClearCarriedItem();
            
            return true;
        }
    }
}