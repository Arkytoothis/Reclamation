using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Core
{
    [CreateAssetMenu(fileName = "Damage Type Database", menuName = "Descending/Database/Damage Type Database")]
    public class DamageTypeDatabase : ScriptableObject
    {
        [SerializeField] private DamageTypeDictionary _damageTypes = null;
        public DamageTypeDictionary Data { get => _damageTypes; }

        public DamageTypeDefinition GetDamageType(string key)
        {
            return _damageTypes[key];
        }
    }
}