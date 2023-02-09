using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class FindHeroToAttack : Action
    {
        private Hero _heroTarget = null;
        
        public override bool PrePerform()
        {
            _heroTarget = HeroManager.Instance.FindClosestHero(transform);
            
            if(_heroTarget == null) return false;
            
            _target = _heroTarget.gameObject;
            _targetController.AddTarget(_target);
            
            return true;
        }

        public override bool PostPerform()
        {
            //Debug.Log("Setting Target");

            return true;
        }
    }
}