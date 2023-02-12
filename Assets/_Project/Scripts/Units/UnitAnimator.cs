using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Units
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;

        private Rigidbody _rigidbody = null;
        private Unit _unit = null;
        [SerializeField] private float _speed;
        
        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _speed = _rigidbody.velocity.magnitude;
            _animator.SetFloat("Blend", _speed);
        }

        public void Setup(Animator animator)
        {
            _animator = animator;
        }

        public void SetAnimatorOverride(WeaponData weaponData)
        {
            if (weaponData != null && weaponData.AnimatorOverride != null)
            {
                _animator.runtimeAnimatorController = weaponData.AnimatorOverride;
            }
        }
        
        public void SetAnimatorOverride(AnimatorOverrideController overrideController)
        {
            _animator.runtimeAnimatorController = overrideController;
        }

        public void Chop()
        {
            _animator.SetTrigger("Chop");
        }

        public void MeleeAttack()
        {
            _animator.SetTrigger("MeleeAttack");
        }

        public void RangedAttack()
        {
            _animator.SetTrigger("RangedAttack");
        }

        public void SetIsWalking(bool walking)
        {
            _animator.SetBool("IsWalking", walking);
        }
    }
}