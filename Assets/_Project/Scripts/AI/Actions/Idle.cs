using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.AI
{
    public class Idle : Action
    {
        public override bool PrePerform()
        {
            _target = TargetManager.Instance.idleSpot.gameObject;
            
            if (_target == null) return false;
            
            _agent.UnitPathfinder.SetEndReachedDistance(_maxDistance);
            if(Vector3.Distance(transform.position, _target.transform.position) > _maxDistance)
                _agent.UnitPathfinder.Restart();
            
            return true;
        }

        public override bool PostPerform()
        {
            _agent.UnitPathfinder.Stop();
            _beliefs.RemoveState(StateManager.Instance.NeedIdle.Name);
            
            return true;
        }
    }
}