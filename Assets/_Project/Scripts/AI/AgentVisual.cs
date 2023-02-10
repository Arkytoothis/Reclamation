using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Reclamation.AI
{
    [ExecuteInEditMode]
    public class AgentVisual : MonoBehaviour
    {
        private Agent _agent;
        private RichAI _richAI;

        public Agent Agent => _agent;
        public RichAI RichAI => _richAI;

        void Start()
        {
            _agent = GetComponent<Agent>();
            _richAI = GetComponent<RichAI>();
        }
    }
}