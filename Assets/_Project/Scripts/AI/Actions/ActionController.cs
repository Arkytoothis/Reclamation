using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.AI
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private ActionDictionary _actionsDictionary = new ActionDictionary();

        private PlayerAgent _agent = null;

        public void Setup()
        {
            Action[] actions = transform.GetComponents<Action>();
            
            foreach (Action action in actions)
            {
                _actionsDictionary.Add(action.ActionName, action);
            }
        }
        
        public void SetActionEnabled(string action, bool enable)
        {
            if (_actionsDictionary.ContainsKey(action))
            {
                _actionsDictionary[action].IsEnabled = enable;
                _actionsDictionary[action].enabled = enable;
            }
            else
            {
                Debug.Log("Action: (" + action + ") does not exist");
            }
        }
    }
}