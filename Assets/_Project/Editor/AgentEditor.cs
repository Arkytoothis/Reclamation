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

            if (agentVisual.agent == null) return;
            
            GUILayout.Label("Current Action: " + agentVisual.agent.CurrentAction);
            
            GUILayout.Label("Actions: ");
            foreach (Action action in agentVisual.agent.Actions)
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
            if (agentVisual.agent.ActionQueue != null)
            {
                string actionString = agentVisual.agent.ActionQueue.Count + ": ";
                
                int index = 0;
                foreach (Action action in agentVisual.agent.ActionQueue)
                {
                    actionString += action.ActionName;
                
                    if (index != agentVisual.agent.ActionQueue.Count - 1)
                    {
                        actionString += ", ";
                    }
                    
                    index++;
                }
                
                GUILayout.Label(actionString);
            }
            

            GUILayout.Label("Goals: ");
            foreach (KeyValuePair<SubGoal, int> goal in agentVisual.agent.Goals)
            {
                foreach (KeyValuePair<string, int> subGoal in goal.Key.SubGoals)
                {
                    GUILayout.Label(goal.Value + " - " + subGoal.Key);
                }
            }

            GUILayout.Label("Beliefs: ");
            if (agentVisual.agent.Beliefs != null)
            {
                foreach (KeyValuePair<string, int> state in agentVisual.agent.Beliefs.States)
                {
                    GUILayout.Label("  " + state.Key + " - " + state.Value);
                }
            }

            GUILayout.Label("Inventory: ");
            if (agentVisual.agent.TargetController != null)
            {
                foreach (GameObject g in agentVisual.agent.TargetController.TargetList)
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