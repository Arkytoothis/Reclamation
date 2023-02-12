using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Interactables;
using UnityEngine;

namespace Reclamation.AI
{
    public class Toilet : MonoBehaviour, IInteractionPoint
    {
        [SerializeField] private Transform _interactionPoint = null;
        
        private void Start()
        {
            TargetManager.Instance.GetTargetQueue(QueueTypes.Toilet).AddTarget(gameObject);
        }

        public Transform GetInteractionPoint()
        {
            return _interactionPoint;
        }
    }
}