using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    public class TargetQueue
    {
        private Queue<GameObject> _queue = new Queue<GameObject>();
        private string _tag = "";
        private string _modifyState = "";

        public Queue<GameObject> Queue => _queue;
        public string Tag => _tag;
        public string ModifyState => _modifyState;

        public TargetQueue(string tag, string modifyState)
        {
            _tag = tag;
            _modifyState = modifyState;
            // if (_tag != "")
            // {
            //     GameObject[] resources = GameObject.FindGameObjectsWithTag(_tag);
            //     foreach (GameObject resource in resources)
            //     {
            //         _queue.Enqueue(resource);
            //     }
            // }
            //
            // if (_modifyState != "")
            // {
            //     worldStates.ModifyState(_modifyState, _queue.Count);
            // }
        }

        public void AddTarget(GameObject target)
        {
            _queue.Enqueue(target);
            TargetManager.Instance.WorldStates.ModifyState(_modifyState, _queue.Count);
        }

        public GameObject GetTarget()
        {
            if (_queue.Count == 0) return null;

            return _queue.Dequeue();
        }
    }
}