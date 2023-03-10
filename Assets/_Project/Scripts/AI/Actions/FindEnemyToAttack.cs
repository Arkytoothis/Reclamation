using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class FindEnemyToAttack : Action
    {
        private Enemy _enemyTarget = null;
        
        public override bool PrePerform()
        {
            SetDistances();

            if (!FindTarget())
            {
                SetIdleState();
                return false;
            }

            CheckDistanceToTarget(_enemyTarget.transform);
            _target = _enemyTarget.gameObject;
            _targetController.AddTarget(_target);
            
            return true;
        }

        public override bool PostPerform()
        {
            _agent.UnitPathfinder.Stop();
            
            return true;
        }

        private bool FindTarget()
        {
            _enemyTarget = _targetController.FindTarget<Enemy>();

            if (_enemyTarget == null)
            {
                _enemyTarget = EnemyManager.Instance.FindClosestEnemy(transform);
            }

            if (_enemyTarget == null)
            {
                _agent.UnitPathfinder.Restart();
                return false;
            }

            return true;
        }

        private void SetDistances()
        {
            InventoryController inventory = _agent.Unit.Inventory;

            if (inventory != null)
            {
                _maxDistance = inventory.GetMeleeWeapon().GetWeaponData().Range;
            }

            _agent.UnitPathfinder.SetEndReachedDistance(_maxDistance);
        }
    }
}