using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    [System.Serializable]
    public class Node
    {
        private Node _parent = null;
        private float _cost = 0f;
        private Dictionary<string, int> _state = null;
        private Action _action = null;

        public Node parent => _parent;
        public float cost => _cost;
        public Dictionary<string, int> state => _state;
        public Action action => _action;

        public Node(Node parent, float cost, Dictionary<string, int> allStates, Action action)
        {
            _parent = parent;
            _cost = cost;
            _state = new Dictionary<string, int>(allStates);
            _action = action;
        }

        public Node(Node parent, float cost, Dictionary<string, int> allStates, Dictionary<string, int> beliefStates, Action action)
        {
            _parent = parent;
            _cost = cost;
            _state = new Dictionary<string, int>(allStates);
            _action = action;

            foreach (var kvp in beliefStates)
            {
                if (!_state.ContainsKey(kvp.Key))
                {
                    _state.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}