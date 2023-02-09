using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    public class TargetController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _targetList = null;

        public List<GameObject> TargetList => _targetList;

        private void Awake()
        {
            _targetList = new List<GameObject>();
        }
        
        public void AddTarget(GameObject resource)
        {
            _targetList.Add(resource);
        }

        private void CleanTargetList()
        {
            for (int i = _targetList.Count - 1; i >= 0; i--)
            {
                if (_targetList[i] == null || _targetList[i].gameObject == null)
                {
                    _targetList.RemoveAt(i);
                }
            }
        }

        public T FindTarget<T>()
        {
            CleanTargetList();
            T type = default(T);
            
            foreach (GameObject target in _targetList)
            {
                T t = target.GetComponent<T>();
                
                if(t != null)
                {
                    type = t;
                }
            }

            return type;
        }
        
        public GameObject FindTarget(string tag)
        {
            CleanTargetList();
            
            foreach (GameObject target in _targetList)
            {
                if (target.CompareTag(tag))
                {
                    return target;
                }
            }

            return null;
        }

        public void RemoveTarget(GameObject target)
        {
            _targetList.Remove(target);
        }
    }
}