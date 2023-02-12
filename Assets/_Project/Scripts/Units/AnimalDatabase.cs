using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Attributes
{
    [CreateAssetMenu(fileName = "Animal Database", menuName = "Reclamation/Database/Animal Database")]
    public class AnimalDatabase : ScriptableObject
	{
        [SerializeField] private AnimalDefinitionDictionary _animals = null;

        public AnimalDefinitionDictionary Animals { get => _animals; }
        
        public AnimalDefinition GetAnimal(string key)
        {
            return _animals[key];
        }
    }
}