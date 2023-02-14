using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class AttackEnemy : Action
    {
        private Enemy _enemyTarget = null;
        private Item _weapon = null;
        
        public override bool PrePerform()
        {
            Setup();
            
            _enemyTarget = _targetController.FindTarget<Enemy>();
            _target = _enemyTarget.gameObject;
            _agent.UnitAnimator.MeleeAttack();
            
            _weapon.Use(_agent.Unit, new List<Unit> { _enemyTarget });
            
            if(Random.Range(0, 100) > 30)
            {
                //int damage = Random.Range(3, 6);
                //_enemyTarget.TakeDamage(_agent.Unit, null, damage, "Life");
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

        private void Setup()
        {
            InventoryController inventory = _agent.Unit.Inventory;

            if (inventory == null) return; 
            
            _weapon = inventory.GetMeleeWeapon();
            _maxDistance = _weapon.GetWeaponData().Range;
            _agent.UnitPathfinder.SetEndReachedDistance(_maxDistance);
            _duration = _weapon.GetWeaponData().AttackDelay;
        }
    }
}