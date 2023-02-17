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
                SetIdleState();
                return false;
            }

            CheckDistanceToTarget(node.transform);
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