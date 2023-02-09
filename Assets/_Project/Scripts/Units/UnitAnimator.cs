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
        [SerializeField] private bool _applyMotion = false;
        [SerializeField] private float _speedMultiplier = 75f;

        private Rigidbody _rigidbody = null;
        private Unit _unit = null;
        private Vector3 _lastPosition = Vector3.zero;
        [SerializeField] private float _speed;
        
        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //if (_applyMotion == false) return;
            
            //_speed = (transform.position - _lastPosition).magnitude * _speedMultiplier;
            //_lastPosition = transform.position;
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

        public void Chop()
        {
            _animator.SetTrigger("Chop");
        }

        public void SetIsWalking(bool walking)
        {
            _animator.SetBool("IsWalking", walking);
        }
    }
}