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
        private Item _weapon = null;
        
        public override bool PrePerform()
        {
            SetupWeapon();    
            SetDistances();
            
            _animalTarget = _targetController.FindTarget<Animal>();
            _target = _animalTarget.gameObject;
            
            _agent.UnitAnimator.RangedAttack();

            //if (_weapon != null)
            //{
                _weapon.Use(_agent.Unit, new List<Unit> { _animalTarget });
            //}
            
            return true;
        }

        public override bool PostPerform()
        {
            return true;
        }

        private void SetupWeapon()
        {
            InventoryController inventory = _agent.Unit.Inventory;

            if (inventory != null)
            {
                _weapon = inventory.GetRangedWeapon();
            }
        }
        
        private void SetDistances()
        {
            _maxDistance = _weapon.GetWeaponData().Range;
            _agent.UnitPathfinder.SetEndReachedDistance(_maxDistance);
        }
    }
}