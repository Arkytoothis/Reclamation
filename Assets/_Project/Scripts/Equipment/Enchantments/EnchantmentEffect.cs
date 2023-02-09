using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Equipment.Enchantments
{
    [System.Serializable]
    public class EnchantmentEffect
    {
        public virtual string GetTooltipText() { return ""; }
    }
}