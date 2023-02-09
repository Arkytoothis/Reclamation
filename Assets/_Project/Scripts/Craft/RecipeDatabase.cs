using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reclamation.Core;
using Reclamation.Craft;
using UnityEngine;

namespace Reclamation.Equipment
{
    [CreateAssetMenu(fileName = "Recipe Database", menuName = "Reclamation/Database/Recipe Database")]
    public class RecipeDatabase : ScriptableObject
    {
        [SerializeField] private RecipesDictionary _data = null;
        public RecipesDictionary Dictionary { get => _data; }

        public RecipeDefinition GetRecipe(string key)
        {
            if (Contains(key))
            {
                return _data[key];
            }
            else
            {
                Debug.Log("Recipe Key: (" + key + ") does not exist");
                return null;
            }
        }

        public bool Contains(string key)
        {
            return _data.ContainsKey(key);
        }
    }
}