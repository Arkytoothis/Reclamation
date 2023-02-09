using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reclamation.Build
{
    [CreateAssetMenu(fileName = "BuildingObject Definition", menuName = "Reclamation/Definition/BuildingObject Definition")]
    public class BuildingObjectDefinition : ScriptableObject
    {
        [SerializeField] private string _name = "";
        [SerializeField] private string _key = "";
        [SerializeField] private BuildingObjectTypes _objectType = BuildingObjectTypes.None;
        [SerializeField] private GameObject _objectPrefab = null;
        [SerializeField] private GameObject _previewPrefab = null;
        [SerializeField] private GameObject _blueprintPrefab = null;
        [FormerlySerializedAs("_resourcesRequired")] [SerializeField] private List<BuildingIngredient> _ingredientsRequired = null;

        public string Name => _name;
        public string Key => _key;
        public BuildingObjectTypes ObjectType => _objectType;
        public GameObject ObjectPrefab => _objectPrefab;
        public GameObject PreviewPrefab => _previewPrefab;
        public GameObject BlueprintPrefab => _blueprintPrefab;
        public List<BuildingIngredient> ingredientsRequired => _ingredientsRequired;
    }
}