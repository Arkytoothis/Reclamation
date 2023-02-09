using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class BuildBlueprint : Action
    {
        private Blueprint _blueprint = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _blueprint = BuildingManager.Instance.FindClosestBlueprintReadyToBuild(transform);
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            if (_blueprint == null || _resourceController == null) return false;

            _target = _blueprint.gameObject;
            _targetController.AddTarget(_target);
            
            return true;
        }

        public override bool PostPerform()
        {
            _blueprint.AddBuildProgress(100);
            TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.BuildBlueprintState.Name, -1);

            return true;
        }
    }
}