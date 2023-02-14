using System.Collections;
using System.Collections.Generic;
using Reclamation.Abilities;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Equipment
{
    [CreateAssetMenu(fileName = "Projectile Definition", menuName = "Reclamation/Definition/Projectile Definition")]
    public class ProjectileDefinition : ScriptableObject
    {
        [SerializeReference] private GameObject _prefab = null;
        [SerializeReference] private float _speed = 30f;
        [SerializeReference] private List<DamageEffect> _damageEffects = null;

        public GameObject Prefab => _prefab;
        public float Speed => _speed;
        public List<DamageEffect> DamageEffects => _damageEffects;

        public void ProcessHit(Unit attacker, List<Unit> targets)
        {
            foreach (Unit target in targets)
            {
                foreach (DamageEffect damageEffect in _damageEffects)
                {
                    int damage = Random.Range(damageEffect.MinimumValue, damageEffect.MaximumValue + 1);
                    target.Damage(attacker, damageEffect.DamageType, damage, damageEffect.Attribute.Key);
                }
            }
        }
    }
}