using System.Collections;
using System.Collections.Generic;
using Pathfinding;
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

            if (_heroTarget != null)
            {
                //Debug.Log("I Already Have A Target");
                if (Vector3.Distance(transform.position, _target.transform.position) > _maxDistance)
                {
                    //Debug.Log("Move Closer To Target");
                    //_richAI.canMove = true;
                }
                
                return true;
            }

            _heroTarget = HeroManager.Instance.FindClosestHero(transform);
            
            if(_heroTarget == null) return false;

            //Debug.Log("New Target Found");
            _target = _heroTarget.gameObject;
            _targetController.AddTarget(_target);
            
            if (Vector3.Distance(transform.position, _target.transform.position) > _maxDistance)
            {
                //Debug.Log("Moving Closer To Target");
                //_richAI.canMove = true;
            }

            return true;
        }

        public override bool PostPerform()
        {
            //Debug.Log("Target Reached");
            //_richAI.canMove = false;
            
            return true;
        }
    }
}