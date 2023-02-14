using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Resources;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class GatherResource : Action
    {
        private ItemDrop _drop = null;
        private UnitResourceController _resourceController = null;
        
        public override bool PrePerform()
        {
            _drop = ResourcesManager.Instance.FindClosestItemDropNotTargeted(transform);
            _resourceController = GetComponentInParent<Hero>().ResourceController;

            if (_drop == null)
            {
                _agent.UnitPathfinder.Stop();
                _agent.ModifyState(StateManager.Instance.NeedIdle.Name, 0);
                
                return false;
            }

            if(Vector3.Distance(transform.position, _drop.transform.position) > _maxDistance)
                _agent.UnitPathfinder.Restart();
            
            _target = _drop.gameObject;
            
            return true;
        }

        public override bool PostPerform()
        {
            if (_drop == null || _drop.gameObject == null) return false;
            
            _agent.UnitPathfinder.Stop();
            _resourceController.PickupItem(_drop.Item, 1);
            ResourcesManager.Instance.RemoveItemDrop(_drop);
            Destroy(_target);

            // if (_resourceController.ItemCarried.StackSize > 0)
            // {
            //     _beliefs.RemoveState(StateManager.Instance.GatherResourceState.Name);
            //     _beliefs.ModifyState(StateManager.Instance.StoreResourceState.Name, 1);
            // }
            
            return true;
        }
    }
}