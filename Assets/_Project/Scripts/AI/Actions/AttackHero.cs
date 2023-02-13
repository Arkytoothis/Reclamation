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
            //Debug.Log("Preparing To Attack");
            _heroTarget = _targetController.FindTarget<Hero>();
            _target = _heroTarget.gameObject;
            
            _agent.UnitAnimator.MeleeAttack();
            
            if(Random.Range(0, 100) > 50)
            {
                int damage = Random.Range(1, 6);
                _heroTarget.TakeDamage(_agent.Unit, null, 0, "Life");
            }
            else
            {
                //Debug.Log("Miss");
            }
            
            return true;
        }

        public override bool PostPerform()
        {
            //Debug.Log("Attacking Target: " + _heroTarget.GetShortName());
            
            
            return true;
        }
    }
}