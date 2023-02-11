using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class FindEnemyToAttack : Action
    {
        private Enemy _enemyTarget = null;
        
        public override bool PrePerform()
        {
            _enemyTarget = _targetController.FindTarget<Enemy>();

            if (_enemyTarget != null)
            {
                //Debug.Log("I Already Have A Target");
                if (Vector3.Distance(transform.position, _target.transform.position) > _maxDistance)
                {
                    //Debug.Log("Move Closer To Target");
                    //_richAI.canMove = true;
                }
                
                return true;
            }

            _enemyTarget = EnemyManager.Instance.FindClosestEnemy(transform);
            
            if(_enemyTarget == null) return false;

            //Debug.Log("New Target Found");
            _target = _enemyTarget.gameObject;
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