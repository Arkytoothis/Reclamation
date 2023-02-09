using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    [System.Serializable]
    public class SubGoal
    {
        public Dictionary<string, int> SubGoals;
        public bool Remove;

        public SubGoal(string subGoal, int value, bool remove)
        {
            SubGoals = new Dictionary<string, int>();
            SubGoals.Add(subGoal, value);
            Remove = remove;
        }
    }
}