using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class FindAnimalToAttack : Action
    {
        private Animal _animalTarget = null;
        
        public override bool PrePerform()
        {
            SetDistances();

            if (!FindTarget())
            {
                SetIdleState();
                return false;
            }
            
            CheckDistanceToTarget(_animalTarget.transform);
            _target = _animalTarget.gameObject;
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
            _animalTarget = _targetController.FindTarget<Animal>();

            if (_animalTarget == null)
            {
                _animalTarget = AnimalManager.Instance.FindClosestAnimal(transform);
            }

            if (_animalTarget == null)
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
                _maxDistance = inventory.GetRangedWeapon().GetWeaponData().Range;
            }

            _agent.UnitPathfinder.SetEndReachedDistance(_maxDistance);
        }
    }
}