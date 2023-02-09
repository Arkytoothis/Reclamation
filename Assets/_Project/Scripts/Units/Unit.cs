using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Abilities;
using Reclamation.Attributes;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;
using Attribute = Reclamation.Attributes.Attribute;

namespace Reclamation.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected GameObject _selectionIndicator = null;
        [SerializeField] protected Transform _hitTransform = null;
        [SerializeField] protected Transform _combatTextTransform = null;
        [SerializeField] protected Transform _projectileSpawnPoint = null;
        [SerializeField] protected Transform _cameraMount = null;
        [SerializeField] protected Transform _cameraTarget = null;
        [SerializeField] protected Transform _modelParent = null;
        [SerializeField] protected UnitAnimator _unitAnimator = null;
        [SerializeField] protected AttributesController _attributes = null;
        [SerializeField] protected SkillsController _skills = null;
        [SerializeField] protected InventoryController _inventory = null;
        [SerializeField] protected AbilityController _abilities = null;
        //[SerializeField] protected RagdollSpawner _ragdollSpawner = null;
        //[SerializeField] protected UnitWorldPanel _worldPanel = null;
        [SerializeField] protected UnitEffects _unitEffects = null;
        [SerializeField] protected AnimationEvents _animationEvents = null;
        
        public abstract void SpendActionPoints(int actionPointCost);
        
        protected bool _isActive = false;
        protected bool _isAlive = false;
        protected bool _isSelected = false;
        
        public Transform HitTransform => _hitTransform;
        public Transform CombatTextTransform => _combatTextTransform;
        public Transform CameraMount => _cameraMount;
        public Transform CameraTarget => _cameraTarget;
        public AttributesController Attributes => _attributes;
        public SkillsController Skills => _skills;
        public InventoryController Inventory => _inventory;
        public AbilityController Abilities => _abilities;
        public UnitAnimator UnitAnimator => _unitAnimator;
        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
        public UnitEffects UnitEffects => _unitEffects;
        public AnimationEvents AnimationEvents => _animationEvents;

        public bool IsActive => _isActive;
        public bool IsAlive => _isAlive;
        public bool IsSelected => _isSelected;

        public abstract string GetFullName();
        public abstract string GetShortName();
        public abstract Item GetMeleeWeapon();
        public abstract Item GetRangedWeapon();
        public abstract void Damage(GameObject attacker, DamageTypeDefinition damageType, int damage, string vital);
        public abstract void RestoreVital(string vital, int damage);
        public abstract void UseResource(string vital, int damage);
        protected abstract void Dead();
        
        private void Awake()
        {
            _isAlive = true;
        }

        private void Start()
        {
            Deselect();
        }

        private void Update()
        {
        }

        public Attribute GetActions()
        {
            return _attributes.GetVital("Actions");
        }

        public Attribute GetArmor()
        {
            return _attributes.GetVital("Armor");
        }

        public Attribute GetLife()
        {
            return _attributes.GetVital("Life");
        }

        public float GetHealth()
        {
            return _attributes.GetVital("Life").Current;
        }

        public virtual void Select()
        {
            if (_isSelected == false)
            {
                _selectionIndicator.SetActive(true);
                _isSelected = true;
            }
        }

        public void Deselect()
        {
            _isSelected = false;
            if (_selectionIndicator != null)
            {
                _selectionIndicator.SetActive(false);
            }
        }

        public void AddUnitEffect(AbilityEffect abilityEffect)
        {
            _unitEffects.AddEffect(abilityEffect);
        }

        public void RecalculateAttributes()
        {
            _attributes.CalculateModifiers();
        }

        public void SyncWorldPanel()
        {
            //_worldPanel.Sync();
        }
    }
}
