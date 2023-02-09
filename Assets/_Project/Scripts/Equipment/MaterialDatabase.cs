using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;

namespace Reclamation.Equipment
{
    [CreateAssetMenu(fileName = "Material Database", menuName = "Descending/Database/Material Database")]
    public class MaterialDatabase : ScriptableObject
	{
        [SerializeField] private MaterialsDictionary _materials = null;

        public MaterialsDictionary Materials => _materials;

        public MaterialDefinition GetMaterial(string key)
        {
            return _materials[key];
        }
    }
}