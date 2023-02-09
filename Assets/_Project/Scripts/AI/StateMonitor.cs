using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    public class StateMonitor : MonoBehaviour
    {
        [SerializeField] private string _state = "";
        [SerializeField] private float _stateStrength = 0f;
        [SerializeField] private float _stateDecayRate = 0f;
        [SerializeField] private GameObject _resourcePrefab = null;
        [SerializeField] private QueueTypes _queueType;
        [SerializeField] private string _worldState = "";
        [SerializeField] private Action _action = null;

        private WorldStates _beliefs = null;
        private bool _stateFound = false;
        private float _initialStrength = 0f;

        private void Awake()
        {
            _initialStrength = _stateStrength;
        }

        private void Start()
        {
            _beliefs = GetComponent<Agent>().Beliefs;
        }

        private void LateUpdate()
        {
            if (_action.IsRunning)
            {
                _stateFound = false;
                _stateStrength = _initialStrength;
            }

            if (!_stateFound && _beliefs.HasState(_state))
            {
                _stateFound = true;
            }

            if (_stateFound)
            {
                _stateStrength -= _stateDecayRate * Time.deltaTime;

                if (_stateStrength <= 0)
                {
                    Vector3 location = new Vector3(transform.position.x, _resourcePrefab.transform.position.y, transform.position.z);
                    GameObject clone = Instantiate(_resourcePrefab, location, _resourcePrefab.transform.rotation);
                    _stateFound = false;
                    _stateStrength = _initialStrength;
                    _beliefs.RemoveState(_state);
                    TargetManager.Instance.GetTargetQueue(QueueTypes.Puddle).AddTarget(clone);
                    TargetManager.Instance.WorldStates.ModifyState(_worldState, 1);
                }
            }
        }
    }
}