using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public class FindHeroToAttack : Action
    {
        private Hero _heroTarget = null;
        
        public override bool PrePerform()
        {
            if (!FindTarget()) return false;

            if(Vector3.Distance(transform.position, _heroTarget.transform.position) > _maxDistance)
                _agent.UnitPathfinder.Restart();

            _target = _heroTarget.gameObject;
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
            _heroTarget = _targetController.FindTarget<Hero>();

            if (_heroTarget == null)
            {
                _heroTarget = HeroManager.Instance.FindClosestHero(transform);
            }

            if (_heroTarget == null)
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