using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Craft;
using Reclamation.Interactables;
using Reclamation.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reclamation.Gui
{
    public class CraftingStationWorldPanel : MonoBehaviour
    {
        [SerializeField] private Image _recipeIcon = null;
        [SerializeField] private TMP_Text _recipeOrdersLabel = null;

        private CraftingStation _craftingStation = null;
        private RecipeDefinition _recipe = null;
        
        public void Setup(CraftingStation craftingStation)
        {
            _craftingStation = craftingStation;
            _recipe = _craftingStation.CurrentRecipe;
            SyncData();
        }
        
        public void Update()
        {
            Camera camera = Camera.main;
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }

        public void SyncData()
        {
            if (_craftingStation == null || _craftingStation.CurrentRecipe == null)
            {
                _recipeIcon.sprite = null;
                return;
            }

            if (_craftingStation.RecipeOrders > 0)
            {
                _recipeIcon.enabled = true;
                _recipeIcon.sprite = _recipe.Icon;
                _recipeOrdersLabel.SetText("x" + _craftingStation.RecipeOrders);
            }
            else
            {
                _recipeIcon.enabled = false;
                _recipeIcon.sprite = null;
                _recipeOrdersLabel.SetText("");
            }
        }
    }
}
