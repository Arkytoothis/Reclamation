using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Reclamation.AI
{
    [CustomEditor(typeof(AgentVisual))]
    [CanEditMultipleObjects]
    public class AgentVisualEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();
            AgentVisual agentVisual = (AgentVisual)target;
            GUILayout.Label("Name: " + agentVisual.name);

            if (agentVisual.Agent == null) return;
            
            GUILayout.Label("Current Action: " + agentVisual.Agent.CurrentAction);
            GUILayout.Label("End Reached Distance: " + agentVisual.RichAI.endReachedDistance);
            GUILayout.Label("RichAI.remainingDistance: " + agentVisual.RichAI.remainingDistance);

            if (agentVisual.Agent.CurrentAction != null && agentVisual.Agent.CurrentAction.Target != null)
            {
                GUILayout.Label("Distance To Target: " + Vector3.Distance(agentVisual.transform.position, agentVisual.Agent.CurrentAction.Target.transform.position));
            }
            
            GUILayout.Label("Actions: ");
            foreach (Action action in agentVisual.Agent.Actions)
            {
                if(action.IsEnabled == false) continue;
                
                string pre = "";
                string eff = "";

                foreach (KeyValuePair<string, int> p in action.conditionsDictionary)
                    pre += p.Key + ", ";
                foreach (KeyValuePair<string, int> e in action.effectsDictionary)
                    eff += e.Key + ", ";

                GUILayout.Label("====  " + action.ActionName + "(" + pre + ")(" + eff + ")");
            }

            GUILayout.Label("Action Queue: ");
            if (agentVisual.Agent.ActionQueue != null)
            {
                string actionString = agentVisual.Agent.ActionQueue.Count + ": ";
                
                int index = 0;
                foreach (Action action in agentVisual.Agent.ActionQueue)
                {
                    actionString += action.ActionName;
                
                    if (index != agentVisual.Agent.ActionQueue.Count - 1)
                    {
                        actionString += ", ";
                    }
                    
                    index++;
                }
                
                GUILayout.Label(actionString);
            }
            

            GUILayout.Label("Goals: ");
            foreach (KeyValuePair<SubGoal, int> goal in agentVisual.Agent.Goals)
            {
                foreach (KeyValuePair<string, int> subGoal in goal.Key.SubGoals)
                {
                    GUILayout.Label(goal.Value + " - " + subGoal.Key);
                }
            }

            GUILayout.Label("Beliefs: ");
            if (agentVisual.Agent.Beliefs != null)
            {
                foreach (KeyValuePair<string, int> state in agentVisual.Agent.Beliefs.States)
                {
                    GUILayout.Label("  " + state.Key + " - " + state.Value);
                }
            }

            GUILayout.Label("Inventory: ");
            if (agentVisual.Agent.TargetController != null)
            {
                foreach (GameObject g in agentVisual.Agent.TargetController.TargetList)
                {
                    if (g != null)
                    {
                        GUILayout.Label(" " + g.name);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}