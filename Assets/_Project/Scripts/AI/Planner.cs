using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.AI
{
    public class Planner
    {
        public Queue<Action> Plan(List<Action> actions, Dictionary<string, int> goal, WorldStates beliefStates)
        {
            List<Action> usableActions = new List<Action>();

            foreach (Action action in actions)
            {
                if (action.IsAchievable())
                {
                    usableActions.Add(action);
                }
            }

            List<Node> leaves = new List<Node>();
            Node start = new Node(null, 0, TargetManager.Instance.WorldStates.States, beliefStates.States, null);

            bool success = BuildGraph(start, leaves, usableActions, goal);

            if (!success)
            {
                return null;
            }

            Node cheapestNode = null;
            foreach (Node leaf in leaves)
            {
                if (cheapestNode == null)
                {
                    cheapestNode = leaf;
                }
                else
                {
                    if (leaf.cost < cheapestNode.cost)
                    {
                        cheapestNode = leaf;
                    }
                }
            }

            List<Action> result = new List<Action>();
            Node n = cheapestNode;
            while (n != null)
            {
                if (n.action != null)
                {
                    result.Insert(0, n.action);
                }

                n = n.parent;
            }

            Queue<Action> actionQueue = new Queue<Action>();
            foreach (Action action in result)
            {
                actionQueue.Enqueue(action);
            }

            // string actionString = "The plan is: ";
            //
            // int index = 0;
            // foreach (Action action in actionQueue)
            // {
            //     actionString += action.actionName;
            //
            //     if (index != actionQueue.Count - 1)
            //     {
            //         actionString += ", ";
            //     }
            //     
            //     index++;
            // }
            //
            // Debug.Log(actionString);
            return actionQueue;
        }

        private bool BuildGraph(Node parent, List<Node> leaves, List<Action> usableActions, Dictionary<string, int> goal)
        {
            bool foundPath = false;

            foreach (Action usableAction in usableActions)
            {
                if (usableAction.IsAchievableGiven(parent.state))
                {
                    Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                    foreach (var effectKvp in usableAction.effectsDictionary)
                    {
                        if (!currentState.ContainsKey(effectKvp.Key))
                        {
                            currentState.Add(effectKvp.Key, effectKvp.Value);
                        }
                    }

                    Node node = new Node(parent, parent.cost + usableAction.Cost, currentState, usableAction);

                    if (GoalAchieved(goal, currentState))
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<Action> subset = ActionSubset(usableActions, usableAction);
                        bool found = BuildGraph(node, leaves, subset, goal);

                        if (found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }

            return foundPath;
        }

        private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
        {
            foreach (var kvp in goal)
            {
                if (!state.ContainsKey(kvp.Key))
                {
                    return false;
                }
            }

            return true;
        }

        private List<Action> ActionSubset(List<Action> actions, Action removeMe)
        {
            List<Action> subset = new List<Action>();
            foreach (Action action in actions)
            {
                if (!action.Equals(removeMe))
                {
                    subset.Add(action);
                }
            }

            return subset;
        }
    }
}