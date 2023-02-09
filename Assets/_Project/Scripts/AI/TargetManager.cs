using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.AI
{
    public enum QueueTypes
    {
        Patient, Bed, Office, Toilet, Puddle,
        Number, None
    }
    
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager Instance { get; private set; }
        [SerializeField] private int _freeBeds = 0;
        [SerializeField] private int _freeToilets = 0;
        [SerializeField] private Transform _idleSpot = null;
        
        private TargetQueue _patients;
        private TargetQueue _beds;
        private TargetQueue _toilets;
        //private TargetQueue _puddles;
        
        private WorldStates _worldStates;
        private Dictionary<QueueTypes, TargetQueue> _targets = new Dictionary<QueueTypes, TargetQueue>();
        
        public WorldStates WorldStates => _worldStates;
        public Transform idleSpot => _idleSpot;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple Worlds " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            _worldStates = new WorldStates();
            
            _patients = new TargetQueue("", "");
            _targets.Add(QueueTypes.Patient, _patients);
            
            _beds = new TargetQueue(TargetTags.Bed.ToString(), StateManager.Instance.FreeBed.ToString());
            _targets.Add(QueueTypes.Bed, _beds);
            
            _toilets = new TargetQueue(TargetTags.Toilet.ToString(), StateManager.Instance.FreeToilet.ToString());
            _targets.Add(QueueTypes.Toilet, _toilets);
            
            //_puddles = new TargetQueue(TargetTags.Puddle.ToString(), WorldStateTypes.Free_Puddle.ToString());
            //_targets.Add(QueueTypes.Puddle, _puddles);
        }

        public TargetQueue GetTargetQueue(QueueTypes type)
        {
            return _targets[type];
        }

        private void LateUpdate()
        {
            _freeBeds = _beds.Queue.Count;
            _freeToilets = _toilets.Queue.Count;
        }
    }
}