using System.Collections;
using System.Collections.Generic;
using Reclamation.Craft;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Reclamation.Equipment
{
    public class StockpileManager : MonoBehaviour
    {
        public static StockpileManager Instance { get; private set; }

        [SerializeField] private List<Item> _items = null;
        [SerializeField] private BoolEvent onSyncStockpile = null;

        public List<Item> Items => _items;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple StockpileManagers " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        public void Setup()
        {
        }
        
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void AddToStack(Item itemToAdd, int amount)
        {
            foreach (Item item in _items)
            {
                if (item.IsEqual(itemToAdd))
                {
                    item.AddToStack(amount);
                    return;
                }
            }
        }

        public void RemoveItem(Item itemToRemove, int amountToRemove)
        {
            foreach (Item item in _items)
            {
                if (item.IsEqual(itemToRemove))
                {
                    item.StackSize -= amountToRemove;
                    return;
                }
            }
        }

        public void SyncStockpile()
        {
            onSyncStockpile.Invoke(true);
        }

        public bool HasItem(ItemDefinition itemDefinition)
        {
            foreach (Item item in _items)
            {
                if (item.Key == itemDefinition.Key && item.StackSize > 0)
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool CanCraftRecipe(RecipeDefinition recipe)
        {
            bool canCraft = true;

            foreach (RecipeIngredient recipeIngredient in recipe.Ingredients)
            {
                if (HasItem(recipeIngredient.Item) == false)
                {
                    canCraft = false;
                }
            }

            return canCraft;
        }
    }
}