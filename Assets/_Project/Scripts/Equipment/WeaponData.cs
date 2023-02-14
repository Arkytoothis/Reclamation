using System.Collections;
using System.Collections.Generic;
using System.Text;
using Reclamation.Core;
using Reclamation.Abilities;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Equipment
{
    [System.Serializable]
    public class WeaponData
    {
        [SerializeField] private bool _hasData = true;
        [SerializeField] private WeaponTypes _weaponType = WeaponTypes.None;
        [SerializeField] private int _range = 1;
        [SerializeField] private float _attackDelay = 1f;
        [SerializeField] private float _projectileDelay = 1f;
        [SerializeField] private ProjectileDefinition _projectile = null;
        [SerializeField] private GameObject _attackEffectPrefab = null;
        [SerializeField] private AnimatorOverrideController _animatorOverride = null;
        [SerializeReference] private List<DamageEffect> _damageEffects = null;

        //[SerializeField, SoundGroup] private List<string> _hitSounds;
        //[SerializeField, SoundGroup] private List<string> _attackSounds;
        
        public bool HasData => _hasData;
        public ProjectileDefinition Projectile => _projectile;
        public WeaponTypes WeaponType => _weaponType;
        public int Range => _range;
        public float AttackDelay => _attackDelay;
        public float ProjectileDelay => _projectileDelay;
        public AnimatorOverrideController AnimatorOverride => _animatorOverride;
        public GameObject AttackEffectPrefab => _attackEffectPrefab;
        public List<DamageEffect> DamageEffects => _damageEffects;
        //public List<string> HitSounds => _hitSounds;
        //public List<string> AttackSounds => _attackSounds;

        public WeaponData(WeaponData weaponData)
        {
            _hasData = weaponData._hasData;
            _projectileDelay = weaponData._projectileDelay;
            _range = weaponData._range;
            _weaponType = weaponData._weaponType;
            _animatorOverride = weaponData.AnimatorOverride;
            _damageEffects = weaponData.DamageEffects;
        }

        public string GetTooltipText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Range: ");
            sb.Append(_range);
            sb.AppendLine();

            foreach (DamageEffect attack in _damageEffects)
            {
                sb.Append(attack.MinimumValue).Append("-").Append(attack.MaximumValue).Append(" ");
                sb.Append(attack.DamageType.Name).Append(" damage (").Append(attack.DamageClass).Append(")").AppendLine();
            }

            return sb.ToString();
        }
        
        public string GetItemWidgetText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Range ");
            sb.Append(_range);
            sb.AppendLine();
            
            foreach (DamageEffect attack in _damageEffects)
            {
                sb.Append(attack.MinimumValue).Append("-").Append(attack.MaximumValue).Append(" ");
                sb.Append(attack.DamageType.Name).Append(" damage (").Append(attack.DamageClass).Append(")").AppendLine();
            }

            return sb.ToString();
        }
        
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