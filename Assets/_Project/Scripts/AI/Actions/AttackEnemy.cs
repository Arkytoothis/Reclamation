using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class AttackEnemy : Action
    {
        private Enemy _enemyTarget = null;
        
        public override bool PrePerform()
        {
            //Debug.Log("Preparing To Attack");
            _enemyTarget = _targetController.FindTarget<Enemy>();
            _target = _enemyTarget.gameObject;
            
            //Debug.Log("Attacking Target: " + _enemyTarget.GetShortName());
            _agent.UnitAnimator.MeleeAttack();
            
            if(Random.Range(0, 100) > 30)
            {
                int damage = Random.Range(3, 6);
                _enemyTarget.TakeDamage(gameObject, damage, "Life");
            }
            else
            {
                //Debug.Log("Miss");
            }
            
            return true;
        }

        public override bool PostPerform()
        {

            return true;
        }
    }
}