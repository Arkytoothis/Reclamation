using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Resources;
using UnityEngine;

namespace Reclamation.AI
{
    public class MineOre : Action
    {
        public override bool PrePerform()
        {
            ResourceNode node = ResourcesManager.Instance.FindClosestOreNode(transform);

            if (node == null)
            {
                return false;
            }
            
            _target = node.gameObject;

            if (_target == null)
            {
                return false;
            }
            
            return true;
        }

        public override bool PostPerform()
        {
            ResourceNode node = _target.GetComponent<ResourceNode>();

            if (node != null)
            {
                node.Harvest(500);
            }
            
            return true;
        }
    }
}