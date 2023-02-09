using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    [CreateAssetMenu(fileName = "State Type", menuName = "Reclamation/Definition/StateType")]
    public class StateType : ScriptableObject
    {
        [SerializeField] private string _name = "";

        public string Name => _name;
    }
}