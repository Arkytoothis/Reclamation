using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Equipment
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileDefinition _definition = null;
        [SerializeField] private int _damage = 0;
        [SerializeField] private Unit _owner = null;
        [SerializeField] private Unit _target = null;
        [SerializeField] private int _ownerLayer = 0;

        
        public void Setup(Unit owner, Unit target)
        {
            _owner = owner;
            _target = target;
            _ownerLayer = _owner.gameObject.layer;
            _damage = _definition.GetDamage();
        }

        // private void Update()
        // {
        //     transform.LookAt(_target.transform);
        // }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer != _ownerLayer)
            {
                //Debug.Log(collision.gameObject.name + " hit");
                IDamageSystem damageSystem = collision.gameObject.GetComponent<IDamageSystem>();

                if (damageSystem != null)
                {
                    damageSystem.TakeDamage(_owner, null, _damage, "Life");
                }
                
                Destroy(gameObject);
            }
        }
    }
}