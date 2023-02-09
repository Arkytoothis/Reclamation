using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reclamation.Build;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Equipment
{
    [CreateAssetMenu(fileName = "BuildingObject Database", menuName = "Reclamation/Database/BuildingObject Database")]
    public class BuildingObjectDatabase : ScriptableObject
    {
        [SerializeField] private BuildingObjectDefinitionDictionary _data = null;
        public BuildingObjectDefinitionDictionary Dictionary { get => _data; }

        public BuildingObjectDefinition GetBuildingObject(string key)
        {
            if (Contains(key))
            {
                return _data[key];
            }
            else
            {
                Debug.Log("BuildingObject Key: (" + key + ") does not exist");
                return null;
            }
        }

        public bool Contains(string key)
        {
            return _data.ContainsKey(key);
        }
    }
}