using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Craft
{
    public class CraftingManager : MonoBehaviour
    {
        [SerializeField] private List<RecipeDefinition> _recipes = null;
        
        public static CraftingManager Instance { get; private set; }
        

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple CraftingManagers " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            _recipes = new List<RecipeDefinition>();
        }

        public void AddRecipe(RecipeDefinition recipeDefinition)
        {
            _recipes.Add(recipeDefinition);
            TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.FreeRecipe.ToString(), 1);
        }
    }
}