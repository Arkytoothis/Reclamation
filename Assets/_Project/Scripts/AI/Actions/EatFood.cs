using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class EatFood : Action
    {
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            Debug.Log("EatFood.PrePerform");
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            if (_resourceController == null) return false;
            
            return true;
        }

        public override bool PostPerform()
        {
            Debug.Log("EatFood.PostPerform");
            _beliefs.RemoveState(StateManager.Instance.NeedFoodState.Name);
            _resourceController.ClearCarriedItem();
            
            return true;
        }
    }
}