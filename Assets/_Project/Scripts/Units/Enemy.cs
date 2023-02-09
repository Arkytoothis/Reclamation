using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Units
{
    public class Enemy : Unit
    {
        public override void SpendActionPoints(int actionPointCost)
        {
            
        }

        public override string GetFullName()
        {
            return "";
        }

        public override string GetShortName()
        {
            return "";
        }

        public override Item GetMeleeWeapon()
        {
            return null;
        }

        public override Item GetRangedWeapon()
        {
            return null;
        }

        public override void Damage(GameObject attacker, DamageTypeDefinition damageType, int damage, string vital)
        {
            
        }

        public override void RestoreVital(string vital, int damage)
        {
            
        }

        public override void UseResource(string vital, int damage)
        {
            
        }

        protected override void Dead()
        {
            
        }
    }
}