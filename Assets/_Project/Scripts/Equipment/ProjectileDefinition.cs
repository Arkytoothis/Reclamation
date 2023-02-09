using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Abilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reclamation.Equipment
{
    [CreateAssetMenu(fileName = "Projectile Definition", menuName = "Descending/Definition/Projectile Definition")]
    public class ProjectileDefinition : ScriptableObject
    {
        public GameObject Prefab = null;
        public DamageTypeDefinition DamageType = null;
        public DamageClasses DamageClass = DamageClasses.None;
        public float Speed = 30f;

        public List<DamageEffect> DamageEffects;
    }
}