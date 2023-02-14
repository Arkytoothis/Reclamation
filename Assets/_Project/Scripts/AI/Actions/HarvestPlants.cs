using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Resources;
using UnityEngine;

namespace Reclamation.AI
{
    public class HarvestPlants : Action
    {
        public override bool PrePerform()
        {
            ResourceNode node = ResourcesManager.Instance.FindClosestBushNode(transform);

            if (node == null)
            {
                _agent.UnitPathfinder.Stop();
                _agent.ModifyState(StateManager.Instance.NeedIdle.Name, 0);
                
                return false;
            }

            if(Vector3.Distance(transform.position, node.transform.position) > _maxDistance)
                _agent.UnitPathfinder.Restart();
            
            _target = node.gameObject;
            
            return true;
        }

        public override bool PostPerform()
        {
            _agent.UnitPathfinder.Stop();
            
            ResourceNode node = _target.GetComponent<ResourceNode>();

            if (node != null)
            {
                node.Harvest(500);
            }
            
            return true;
        }
    }
}