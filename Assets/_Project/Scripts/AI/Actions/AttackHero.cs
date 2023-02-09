using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class AttackHero : Action
    {
        private Hero _heroTarget = null;
        
        public override bool PrePerform()
        {
            _heroTarget = _targetController.FindTarget<Hero>();
            
            if(_heroTarget == null) return false;

            _target = _heroTarget.gameObject;
            
            return true;
        }

        public override bool PostPerform()
        {
            Debug.Log("Attacking Target");

            return true;
        }
    }
}