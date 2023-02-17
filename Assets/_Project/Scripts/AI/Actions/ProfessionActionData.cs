using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    [System.Serializable]
    public class ProfessionActionData
    {
        [SerializeField] private StateType _stateType = null;
        [SerializeField] private bool _remove = false;
        [SerializeField] private int _priority = 0;

        public StateType StateType => _stateType;
        public bool Remove => _remove;
        public int Priority => _priority;
        
        
    }
}