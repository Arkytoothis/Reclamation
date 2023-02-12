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
            _heroTarget = _targetController.FindTarget<Hero>();

            if (_heroTarget == null)
            {
                //Debug.Log("I Already Have A Target");
                _heroTarget = HeroManager.Instance.FindClosestHero(transform);
            }

            if (_heroTarget == null)
            {
                _agent.UnitPathfinder.Restart();
                return false;
            }
            
            if(Vector3.Distance(transform.position, _heroTarget.transform.position) > _maxDistance)
                _agent.UnitPathfinder.Restart();

            //Debug.Log("New Target Found");
            _target = _heroTarget.gameObject;
            _targetController.AddTarget(_target);

            return true;
        }

        public override bool PostPerform()
        {
            //Debug.Log("Target Reached");
            _agent.UnitPathfinder.Stop();
            
            return true;
        }
    }
}