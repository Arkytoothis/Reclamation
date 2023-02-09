using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.AI
{
    public class Wait : Action
    {
        public override bool PrePerform()
        {
            _target = TargetManager.Instance.idleSpot.gameObject;

            if (_target == null)
            {
                return false;
            }
            
            return true;
        }

        public override bool PostPerform()
        {
            _beliefs.RemoveState(StateManager.Instance.NeedWaitState.Name);
            
            return true;
        }
    }
}