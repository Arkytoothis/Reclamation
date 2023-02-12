using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using UnityEngine;

namespace Reclamation.Units
{
    public class TargetRangeChecker : MonoBehaviour
    {
        [SerializeField] private Agent _agent = null;
        [SerializeField] private float _delay = 0.1f;
        [SerializeField] private bool _targetInRange = false;
        
        private float _nextInterval = 0f;

        private void Awake()
        {
            _agent = GetComponent<Agent>();
        }

        private void Update()
        {
            if (_agent.CurrentAction == null || _agent.CurrentAction.Target == null) return;
            
            if (Vector3.Distance(transform.position, _agent.CurrentAction.Target.transform.position) <= _agent.CurrentAction.MaxDistance)
            {
                _targetInRange = true;
                _agent.UnitPathfinder.Stop();  
            }
            else
            {
                _targetInRange = false;
                _agent.UnitPathfinder.Restart(); 
            }
        }
    }
}