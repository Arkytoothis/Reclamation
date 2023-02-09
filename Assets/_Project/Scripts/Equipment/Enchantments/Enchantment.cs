using Reclamation.Core;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Reclamation.Equipment.Enchantments
{
    [System.Serializable]
    public class Enchantment
    {
        [SerializeField] private EnchantmentDefinition _definition = null;
        public EnchantmentDefinition Definition => _definition;


        public Enchantment(EnchantmentDefinition definition)
        {
            _definition = definition;
        }

        public string GetTooltipText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(_definition.Name).AppendLine();

            return sb.ToString();
        }
    }
}