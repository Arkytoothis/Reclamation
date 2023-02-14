using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reclamation.Equipment;
using Reclamation.Interactables;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public abstract class Agent : MonoBehaviour
    {
        [SerializeField] private Transform _actionsParent;
        [SerializeField] protected List<Action> _actions = new List<Action>();

        [SerializeField] private float _delay = 0.1f;

        protected Unit _unit = null;
        protected UnitPathfinder _unitPathfinder = null;
        protected UnitAnimator _unitAnimator = null;
        protected Dictionary<SubGoal, int> _goals = new Dictionary<SubGoal, int>();
        protected Planner _planner = null;
        protected Queue<Action> _actionQueue = null;
        protected Action _currentAction = null;
        protected SubGoal _currentGoal = null;
        protected bool _invoked = false;
        protected WorldStates _beliefs;
        protected Transform _destination = null;
        public TargetController _targetController = null;

        private float _nextInterval = 0f;

        public Unit Unit => _unit;

        public List<Action> Actions => _actions;
        public Dictionary<SubGoal, int> Goals => _goals;
        public Planner Planner => _planner;
        public Queue<Action> ActionQueue => _actionQueue;
        public Action CurrentAction => _currentAction;
        public SubGoal CurrentGoal => _currentGoal;
        public bool Invoked => _invoked;
        public TargetController TargetController => _targetController;
        public WorldStates Beliefs => _beliefs;
        public Transform Destination => _destination;
        public UnitPathfinder UnitPathfinder => _unitPathfinder;
        public UnitAnimator UnitAnimator => _unitAnimator;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _unitPathfinder = GetComponent<UnitPathfinder>();
            _unitAnimator = GetComponent<UnitAnimator>();
            SetupActions();
            _beliefs = new WorldStates();
        }

        protected void Start()
        {
            
        }

        public void SetupActions()
        {
            _actions = _actionsParent.GetComponents<Action>().ToList();
        }

        private void Update()
        {
            if (Time.time > _nextInterval)
            {
                _nextInterval = Time.time + _delay;
                
                if (_currentAction != null && _currentAction.IsRunning && _currentAction.IsEnabled)
                {
                    if (_unitPathfinder.GetRemainingDistance() < _unitPathfinder.GetEndReadchedDistance())
                    {
                        if (!_invoked)
                        {
                            //if (_currentAction.Target != null)
                            //    transform.LookAt(_currentAction.Target.transform, Vector3.up);
                            
                            StartCoroutine(CompleteAction(_currentAction.Duration));
                            //Invoke("CompleteAction", _currentAction.Duration);
                            //_unit.Pathfinder.Stop();
                            _invoked = true;
                        }
                    }

                    return;
                }

                if (_planner == null || _actionQueue == null)
                {
                    _planner = new Planner();
                    var sortedGoals = from entry in _goals orderby entry.Value descending select entry;

                    foreach (var subgoal in sortedGoals)
                    {
                        _actionQueue = _planner.Plan(_actions, subgoal.Key.SubGoals, _beliefs);
                        if (_actionQueue != null)
                        {
                            _currentGoal = subgoal.Key;
                            break;
                        }
                    }
                }

                if (_actionQueue != null && _actionQueue.Count == 0)
                {
                    if (_currentGoal.Remove)
                    {
                        _goals.Remove(_currentGoal);
                    }

                    _planner = null;
                }

                if (_actionQueue != null && _actionQueue.Count > 0)
                {
                    _currentAction = _actionQueue.Dequeue();

                    if (_currentAction.PrePerform())
                    {
                        if (_currentAction.Target != null)
                        {
                            _currentAction.IsRunning = true;
                            //Transform interactionPoint = _currentAction.Target.transform.Find("Interaction Point");
                            
                            IInteractionPoint interactionPoint = _currentAction.Target.GetComponent<IInteractionPoint>();
                            
                            if (interactionPoint != null)
                            {
                                _destination = interactionPoint.GetInteractionPoint();
                            }
                            else
                            {
                                _destination = _currentAction.Target.transform;
                            }
                            
                            //_unit.Pathfinder.Restart();
                            _unitPathfinder.SetDestination(_destination);
                        }
                    }
                    else
                    {
                        _actionQueue = null;
                    }
                }
                
                _unit.SyncData();
            }
        }

        private void CompleteAction()
        {
            _currentAction.IsRunning = false;
            _currentAction.PostPerform();
            _invoked = false;
        }
        
        private IEnumerator CompleteAction(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            _currentAction.PostPerform();
            _currentAction.IsRunning = false;
            _invoked = false;
        }

        public void AddGoal(string key, int value, bool remove, int priority)
        {
            _goals.Add(new SubGoal(key, value, remove), priority);
        }

        public void ModifyState(string state, int value)
        {
            _beliefs.ModifyState(state, value);
        }
    }
}