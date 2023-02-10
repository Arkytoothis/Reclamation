using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.AI
{
    public abstract class Agent : MonoBehaviour
    {
        [SerializeField] private Transform _actionsParent;
        [SerializeField] protected List<Action> _actions = new List<Action>();

        protected Unit _unit = null;
        protected RichAI _richAi = null;
        protected Dictionary<SubGoal, int> _goals = new Dictionary<SubGoal, int>();
        protected Planner _planner = null;
        protected Queue<Action> _actionQueue = null;
        protected Action _currentAction = null;
        protected SubGoal _currentGoal = null;
        protected bool _invoked = false;
        protected WorldStates _beliefs;
        protected Vector3 _destination = Vector3.zero;
        protected TargetController _targetController = null;

        public List<Action> Actions => _actions;
        public Dictionary<SubGoal, int> Goals => _goals;
        public Planner Planner => _planner;
        public Queue<Action> ActionQueue => _actionQueue;
        public Action CurrentAction => _currentAction;
        public SubGoal CurrentGoal => _currentGoal;
        public bool Invoked => _invoked;
        public TargetController TargetController => _targetController;
        public WorldStates Beliefs => _beliefs;
        public Vector3 Destination => _destination;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _richAi = GetComponent<RichAI>();
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

        [SerializeField] private float _delay = 0.1f;
        private float _nextInterval = 0f;

        private void Update()
        {
            if (Time.time > _nextInterval)
            {
                _nextInterval = Time.time + _delay;
                
                if (_currentAction != null && _currentAction.IsRunning && _currentAction.IsEnabled)
                {
                    if (_currentAction.RichAI.remainingDistance < _currentAction.MaxDistance)
                    {
                        if (!_invoked)
                        {
                            Invoke("CompleteAction", _currentAction.Duration);
                            _richAi.isStopped = true;
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
                        // if (_currentAction.target == null && _currentAction.TargetTag != "")
                        // {
                        //     _currentAction.target = GameObject.FindWithTag(_currentAction.TargetTag);
                        // }

                        if (_currentAction.Target != null)
                        {
                            _currentAction.IsRunning = true;
                            _destination = _currentAction.Target.transform.position;
                            Transform interactionPoint = _currentAction.Target.transform.Find("Interaction Point");
                            if (interactionPoint != null)
                            {
                                _destination = interactionPoint.position;
                            }

                            _richAi.isStopped = false;
                            _currentAction.Seeker.StartPath(transform.position, _destination);
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
    }
}