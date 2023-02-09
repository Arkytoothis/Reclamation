using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Resources
{
    [CreateAssetMenu(fileName = "Resource Node Database", menuName = "Reclamation/Database/Resource Node Database")]
    public class ResourceNodeDatabase : ScriptableObject
    {
        [SerializeField] private ResourceNodeDefinitionDictionary _data = null;
        
        public ResourceNodeDefinitionDictionary Dictionary { get => _data; }

        public ResourceNodeDefinition GetResourceNode(string key)
        {
            if (Contains(key))
            {
                return _data[key];
            }
            else
            {
                Debug.Log("Resource Key: (" + key + ") does not exist");
                return null;
            }
        }

        public bool Contains(string key)
        {
            return _data.ContainsKey(key);
        }
    }
}