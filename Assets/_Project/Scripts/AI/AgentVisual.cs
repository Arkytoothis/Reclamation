using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    [ExecuteInEditMode]
    public class AgentVisual : MonoBehaviour
    {
        private Agent _agent;

        public Agent agent => _agent;

        void Start()
        {
            _agent = GetComponent<Agent>();
        }
    }
}