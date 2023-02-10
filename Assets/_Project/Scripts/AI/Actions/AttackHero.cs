using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
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
            
            return true;
        }

        public override bool PostPerform()
        {
            //Debug.Log("Attacking Target: " + _heroTarget.GetShortName());
            if(Random.Range(0, 100) > 50)
            {
                int damage = Random.Range(1, 6);
                _heroTarget.TakeDamage(gameObject, damage, "Life");
            }
            else
            {
                Debug.Log("Miss");
            }
            
            return true;
        }
    }
}