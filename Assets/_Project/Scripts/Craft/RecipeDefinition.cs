using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Craft
{
    [CreateAssetMenu(fileName = "Recipe Definition", menuName = "Reclamation/Definition/Recipe Definition")]
    public class RecipeDefinition : ScriptableObject
    {
        [SerializeField] private string _name = "";
        [SerializeField] private string _key = "";
        [SerializeField] private Sprite _icon = null;
        [SerializeField] private ItemShort _result = null;
        [SerializeField] private int _numberOfItems = 0;
        [SerializeField] private float _secondsToCraft = 0f;
        [SerializeReference] private List<RecipeIngredient> _ingredients = null;

        public string Name => _name;
        public string Key => _key;
        public Sprite Icon => _icon;
        public ItemShort Result => _result;
        public int NumberOfItems => _numberOfItems;
        public float SecondsToCraft => _secondsToCraft;
        public List<RecipeIngredient> Ingredients => _ingredients;
    }
}