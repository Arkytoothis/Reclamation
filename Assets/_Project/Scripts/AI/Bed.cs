using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    public class Bed : MonoBehaviour
    {
        [SerializeField] private GameObject _interactionPoint = null;

        public GameObject InteractionPoint => _interactionPoint;

        private void Start()
        {
            TargetManager.Instance.GetTargetQueue(QueueTypes.Bed).AddTarget(gameObject);
        }
    }
}