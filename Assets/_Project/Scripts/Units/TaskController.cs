using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Gui;
using UnityEngine;

namespace Reclamation.Units
{
    public enum UnitStates
    {
        Working, Combat,
        Number, None
    }
    
    public class TaskController : MonoBehaviour
    {
        [SerializeField] private UnitStates _unitState = UnitStates.None;
        [SerializeField] private float _delay = 0.5f;

        private PlayerAgent _playerAgent = null;
        private AgentVisual _agentVisual = null;
        private HeroPathfinder _pathfinder = null;
        private HeroWorldPanel _worldPanel = null;
        private float _nextInterval = 0f;
        
        public UnitStates UnitState => _unitState;

        private void Awake()
        {
            _playerAgent = GetComponent<PlayerAgent>();
            _agentVisual = GetComponent<AgentVisual>();
            _pathfinder = GetComponent<HeroPathfinder>();
            _worldPanel = GetComponentInChildren<HeroWorldPanel>();
        }

        private void Start()
        {
            SetState(UnitStates.Working);
        }

        private void Update()
        {
            if (Time.time > _nextInterval)
            {
                _nextInterval = Time.time + _delay;
            }
        }

        public void SetState(UnitStates state)
        {
            if (_unitState == state) return;

            _unitState = state;
            switch (_unitState)
            {
                case UnitStates.Working:
                    SetWorking();
                    break;
                case UnitStates.Combat:
                    SetCombat();
                    break;
            }
        }

        private void SetWorking()
        {
            _playerAgent.enabled = true;
            _agentVisual.enabled = true;
            _pathfinder.Restart();
            _worldPanel.SyncData();
        }

        private void SetCombat()
        {
            _playerAgent.enabled = false;
            _agentVisual.enabled = false;
            _pathfinder.Stop();
            _worldPanel.SyncData();
        }
    }
}