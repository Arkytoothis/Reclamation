using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Build;
using Reclamation.Core;
using Reclamation.Craft;
using Reclamation.Equipment;
using Reclamation.Gui;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Interactables
{
    public class CraftingStation : Interactable, IInteractionPoint
    {
        [SerializeField] private Transform _dropSpawn = null;
        [SerializeField] private RecipeDefinition _currentRecipe = null;
        [SerializeField] private int _recipeOrders = 0;
        [SerializeField] private CraftingStationWorldPanel _worldPanel;
        [SerializeField] private RecipeIngredientDictionary _ingredientsReady = null;
        
        public Transform InteractionTransform => _interactionTransform;
        public RecipeDefinition CurrentRecipe => _currentRecipe;
        public int RecipeOrders => _recipeOrders;

        private void Start()
        {
            BuildingManager.Instance.RegisterCraftingStation(this);
            SetRecipe(_currentRecipe);
            _worldPanel.Setup(this);
        }

        public override void Interact(Unit interacter)
        {
            
        }

        public void AddCraftingOrder()
        {
            _recipeOrders++;
            TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.CraftItemState.ToString(), 1);
            _worldPanel.SyncData();
        }

        public void FinishRecipe()
        {
            _recipeOrders--;
            TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.CraftItemState.ToString(), -1);
            ItemDrop itemDrop = Instantiate(_currentRecipe.Result.Item.ItemDrop, _dropSpawn.transform.position, Quaternion.identity);
            itemDrop.Setup(1);
            itemDrop.GetComponent<Rigidbody>().AddExplosionForce(100f, transform.position, 100f);
        
            foreach (var kvp in _ingredientsReady)
            {
                kvp.Value.Clear();
            }
            
            _worldPanel.SyncData();
        }

        public void SetRecipe(RecipeDefinition recipe)
        {
            _currentRecipe = recipe;
            _recipeOrders = 0;

            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                Item ingredient = ItemGenerator.GenerateIngredient(recipe.Ingredients[i].Item.Key, 1);
                _ingredientsReady.Add(ingredient.Key, new RecipeIngredient(recipe.Ingredients[i].Item, 0));
            }
        }
        
        public bool HasIngredients()
        {
            bool hasIngredients = true;

            for (int i = 0; i < _currentRecipe.Ingredients.Count; i++)
            {
                if (_ingredientsReady[_currentRecipe.Ingredients[i].Item.Key].NumberItems < _currentRecipe.Ingredients[i].NumberItems)
                {
                    hasIngredients = false;
                }
            }
            
            return hasIngredients;
        }

        public void AddIngredient(ItemDefinition ingredient, int amount)
        {
            foreach (var kvp in _ingredientsReady)
            {
                if (kvp.Key == ingredient.Key)
                {
                    kvp.Value.AddIngredients(amount);
                    break;
                }
            }
        }

        public Item GetFirstRequiredIngredient()
        {
            int index = 0;
            foreach (RecipeIngredient recipeIngredient in _currentRecipe.Ingredients)
            {
                if (_ingredientsReady[recipeIngredient.Item.Key].NumberItems < recipeIngredient.NumberItems)
                {
                    Item item = ItemGenerator.GenerateIngredient(recipeIngredient.Item.Key, recipeIngredient.NumberItems);
                    
                    return item;
                }
            }

            Debug.Log("No Ingredients Found");
            return null;
        }

        public Transform GetInteractionPoint()
        {
            return _interactionTransform;
        }
    }
}