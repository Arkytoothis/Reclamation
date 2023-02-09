using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Reclamation.AI
{
    public abstract class Action : MonoBehaviour
    {
        [SerializeField] protected bool _isRunning = false;
        [SerializeField] protected bool _isEnabled = true;
        [SerializeField] protected string _actionName = "Action";
        [SerializeField] protected Sprite _icon = null;
        [SerializeField] protected float _cost = 1.0f;
        [SerializeField] protected GameObject _target = null;
        [SerializeField] protected string _targetTag = null;
        [SerializeField] protected float _duration = 0.0f;
        [SerializeField] protected float _maxDistance = 2.0f;
        [SerializeField] protected WorldState[] _preConditions;
        [SerializeField] protected WorldState[] _afterEffects;
        
        protected Seeker _seeker = null;
        protected RichAI _richAI = null;
        protected TargetController _targetController = null;
        protected WorldStates _beliefs;
        protected Dictionary<string, int> _conditionsDictionary;
        protected Dictionary<string, int> _effectsDictionary;

        public string ActionName => _actionName;
        public float Cost => _cost;
        public string TargetTag => _targetTag;
        public float Duration => _duration;
        public float MaxDistance => _maxDistance;
        public WorldState[] PreConditions => _preConditions;
        public WorldState[] AfterEffects => _afterEffects;
        public Seeker Seeker => _seeker;
        public TargetController TargetController => _targetController;
        public WorldStates Beliefs => _beliefs;
        public RichAI RichAI => _richAI;
        public Sprite Icon => _icon;
        public Dictionary<string, int> conditionsDictionary => _conditionsDictionary;
        public Dictionary<string, int> effectsDictionary => _effectsDictionary;

        public bool IsRunning
        {
            get => _isRunning;
            set => _isRunning = value;
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        public GameObject Target
        {
            get => _target;
            set => _target = value;
        }

        public Action()
        {
            _conditionsDictionary = new Dictionary<string, int>();
            _effectsDictionary = new Dictionary<string, int>();
        }

        private void Awake()
        {
            _seeker = GetComponentInParent<Seeker>();
            _richAI = GetComponentInParent<RichAI>();
            
            if (_preConditions != null)
            {
                foreach (WorldState preCondition in _preConditions)
                {
                    _conditionsDictionary.Add(preCondition.Key.Name, preCondition.Value);
                }
            }
            if (_afterEffects != null)
            {
                foreach (WorldState effect in _afterEffects)
                {
                    _effectsDictionary.Add(effect.Key.Name, effect.Value);
                }
            }
        }

        private void Start()
        {
            _targetController = GetComponentInParent<Agent>().TargetController;
            _beliefs = GetComponentInParent<Agent>().Beliefs;
            
            if(_targetController == null) Debug.Log("_targetController == null");
            if(_beliefs == null) Debug.Log("_beliefs == null");
        }

        public void SetDetails(ActionDetails details)
        {
            _cost = details.Cost;
            _duration = details.Duration;
            _maxDistance = details.MaxDistance;
            _actionName = details.ActionName;
            _targetTag = details.TargetTag;
            _afterEffects = details.AfterEffects;
            _preConditions = details.PreConditions;
            
            if (_preConditions != null)
            {
                foreach (WorldState preCondition in _preConditions)
                {
                    _conditionsDictionary.Add(preCondition.Key.Name, preCondition.Value);
                }
            }
            if (_afterEffects != null)
            {
                foreach (WorldState effect in _afterEffects)
                {
                    _effectsDictionary.Add(effect.Key.Name, effect.Value);
                }
            }
        }
        
        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (var conditionKvp in _conditionsDictionary)
            {
                if (!conditions.ContainsKey(conditionKvp.Key))
                {
                    return false;
                }
            }

            return true;
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }
}