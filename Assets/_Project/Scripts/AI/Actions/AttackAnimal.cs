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
            Setup();    
            
            _animalTarget = _targetController.FindTarget<Animal>();
            _target = _animalTarget.gameObject;
            _agent.UnitAnimator.RangedAttack();
            _weapon.Use(_agent.Unit, new List<Unit> { _animalTarget });
            
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
            
            _weapon = inventory.GetRangedWeapon();
            _maxDistance = _weapon.GetWeaponData().Range;
            _agent.UnitPathfinder.SetEndReachedDistance(_maxDistance);
            _duration = _weapon.GetWeaponData().AttackDelay;
        }
    }
}