using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.AI
{
    public class Rest : Action
    {
        public override bool PrePerform()
        {
            _target = TargetManager.Instance.GetTargetQueue(QueueTypes.Bed).GetTarget();

            if (_target == null)
            {
                return false;
            }
            
            _targetController.AddTarget(Target);
            TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.FreeBed.ToString(), -1);
            
            return true;
        }

        public override bool PostPerform()
        {
            TargetManager.Instance.GetTargetQueue(QueueTypes.Bed).AddTarget(_target);
            _targetController.RemoveTarget(_target);
            TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.FreeBed.ToString(), 1);
            _beliefs.RemoveState(StateManager.Instance.NeedRestState.Name);
            
            return true;
        }
    }
}