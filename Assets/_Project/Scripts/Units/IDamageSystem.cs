using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Units
{
    public interface IDamageSystem
    {
        public void TakeDamage(GameObject attacker, int amount, string vital);
    }
}