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
        [SerializeField] private Unit _owner = null;
        [SerializeField] private List<Unit> _targets = null;
        [SerializeField] private int _ownerLayer = 0;

        
        public void Setup(Unit owner, List<Unit> targets)
        {
            _owner = owner;
            _targets = targets;
            _ownerLayer = _owner.gameObject.layer;
        }

        // private void Update()
        // {
        //     transform.LookAt(_target.transform);
        // }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer != _ownerLayer)
            {
                _definition.ProcessHit(_owner, _targets);
                //Debug.Log(collision.gameObject.name + " hit");
                // IDamageSystem damageSystem = collision.gameObject.GetComponent<IDamageSystem>();
                //
                // if (damageSystem != null)
                // {
                //     damageSystem.TakeDamage(_owner, null, _damage, "Life");
                // }
                
                Destroy(gameObject);
            }
        }
    }
}