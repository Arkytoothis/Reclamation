using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reclamation.Build
{
    public class Blueprint : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private BuildingObjectDefinition _definition = null;
        [SerializeField] private int _buildProgress = 0;
        [SerializeField] private Transform _buildTransform = null;
        [SerializeField] private BuildingIngredientDictionary _ingredientsReady = null;

        public int BuildProgress => _buildProgress;
        public Transform BuildTransform => _buildTransform;
        public BuildingIngredientDictionary IngredientsReady => _ingredientsReady;
        public BuildingObjectDefinition Definition => _definition;

        private void Awake()
        {
            _ingredientsReady = new BuildingIngredientDictionary();
            
            for (int i = 0; i < _definition.ingredientsRequired.Count; i++)
            {
                _ingredientsReady.Add(_definition.ingredientsRequired[i].Item.Key, new BuildingIngredient(_definition.ingredientsRequired[i]));
            }
        }

        private void Start()
        {
            //TargetManager.Instance.WorldStates.ModifyState(StateManager.Instance.PickupItemForBlueprintState.Name, 1);
        }

        public void AddBuildProgress(int amount)
        {
            _buildProgress += amount;

            if (_buildProgress >= 100)
            {
                Build();
            }
        }
        
        private void Build()
        {
            GameObject clone = Instantiate(_prefab, transform.position, transform.rotation);
            BuildingObject buildingObject = clone.GetComponent<BuildingObject>();
            BuildingManager.Instance.RegisterBuildingObject(buildingObject);
            BuildingManager.Instance.RemoveBlueprint(this);
        }
        
        public bool HasIngredients()
        {
            bool hasIngredients = true;

            for (int i = 0; i < _definition.ingredientsRequired.Count; i++)
            {
                if (_ingredientsReady[_definition.ingredientsRequired[i].Item.Key].Amount < _definition.ingredientsRequired[i].Amount)
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
                    kvp.Value.AddRIngredient(amount);
                    break;
                }
            }
        }
        public Item GetFirstRequiredIngredient()
        {
            int index = 0;
            foreach (BuildingIngredient buildingIngredient in _definition.ingredientsRequired)
            {
                if (_ingredientsReady[buildingIngredient.Item.Key].Amount < buildingIngredient.Amount)
                {
                    Item ingredient = ItemGenerator.GenerateIngredient(buildingIngredient.Item.Key, buildingIngredient.Amount);
                    return ingredient;
                }
            }

            Debug.Log("No Ingredients Found");
            return null;
        }
    }
}