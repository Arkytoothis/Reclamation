using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Reclamation.AI
{
    
    public class PlayerAgent : Agent
    {
        [SerializeField] private float _minRestInterval = 10f;
        [SerializeField] private float _maxRestInterval = 20f;
        [SerializeField] private float _minReliefInterval = 30f;
        [SerializeField] private float _maxReliefInterval = 60f;
        [SerializeField] private float _minHungerInterval = 10f;
        [SerializeField] private float _maxHungerInterval = 30f;
        
        private new void Start()
        {
            base.Start();
            _targetController = GetComponent<TargetController>();
            _goals.Add(new SubGoal(StateManager.Instance.Satiated.Name, 1, false), 3);
            _goals.Add(new SubGoal(StateManager.Instance.Rested.Name, 1, false), 4);
            _goals.Add(new SubGoal(StateManager.Instance.Relieved.Name, 1, false), 5);
            
            StartCoroutine(NeedRest());
            StartCoroutine(NeedRelief());
            StartCoroutine(NeedFood());
        }
        
        private IEnumerator NeedRest()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_minRestInterval, _maxRestInterval));
                _beliefs.ModifyState(StateManager.Instance.NeedRestState.Name, 0);
            }
        }

        private IEnumerator NeedRelief()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_minReliefInterval, _maxReliefInterval));
                _beliefs.ModifyState(StateManager.Instance.NeedReliefState.Name, 0);
            }
        }

        private IEnumerator NeedFood()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_minHungerInterval, _maxHungerInterval));
                _beliefs.ModifyState(StateManager.Instance.NeedFoodState.Name, 0);
            }
        }
    }
}