using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Reclamation.AI
{
    [CreateAssetMenu(fileName = "Action Details", menuName = "Reclamation/Definition/Action Details")]
    public class ActionDetails : ScriptableObject
    {
        [SerializeField] protected string _actionName = "Action";
        [SerializeField] protected float _cost = 1.0f;
        [SerializeField] protected string _targetTag = null;
        [SerializeField] protected float _duration = 0.0f;
        [SerializeField] protected float _maxDistance = 2.0f;
        [SerializeField] protected WorldState[] _preConditions;
        [SerializeField] protected WorldState[] _afterEffects;
        
        public string ActionName => _actionName;
        public float Cost => _cost;
        public string TargetTag => _targetTag;
        public float Duration => _duration;
        public float MaxDistance => _maxDistance;
        public WorldState[] PreConditions => _preConditions;
        public WorldState[] AfterEffects => _afterEffects;

    }
}
