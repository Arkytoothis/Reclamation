using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
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
            transform.LookAt(_target.transform.position, Vector3.up);
            //_richAI.canMove = true;
            
            return true;
        }

        public override bool PostPerform()
        {
            //Debug.Log("Attacking Target: " + _enemyTarget.GetShortName());
            transform.LookAt(_target.transform.position, Vector3.up);
            if(Random.Range(0, 100) > 35)
            {
                int damage = Random.Range(3, 13);
                _enemyTarget.TakeDamage(gameObject, damage, "Life");
            }
            else
            {
                Debug.Log("Miss");
            }
            
            //_richAI.canMove = true;
            
            return true;
        }
    }
}