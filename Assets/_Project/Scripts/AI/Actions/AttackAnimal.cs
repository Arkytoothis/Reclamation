using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class AttackAnimal : Action
    {
        private Animal _animalTarget = null;
        
        public override bool PrePerform()
        {
            SetDistances();
            
            _animalTarget = _targetController.FindTarget<Animal>();
            _target = _animalTarget.gameObject;
            
            _agent.UnitAnimator.RangedAttack();
            
            if(Random.Range(0, 100) > 30)
            {
                int damage = Random.Range(3, 6);
                _animalTarget.TakeDamage(gameObject, damage, "Life");
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

        private void SetDistances()
        {
            InventoryController inventory = _agent.Unit.Inventory;

            if (inventory != null)
            {
                _maxDistance = inventory.GetRangedWeapon().GetWeaponData().Range;
            }

            _agent.UnitPathfinder.SetEndReachedDistance(_maxDistance);
        }
    }
}