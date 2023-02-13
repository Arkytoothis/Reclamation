using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Units
{
    public interface IDamageSystem
    {
        public void TakeDamage(Unit attacker, DamageTypeDefinition damageType, int amount, string vital);
    }
}