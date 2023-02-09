using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;

namespace Reclamation.Gui
{
    public class ActionWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameLabel = null;
        
        [SerializeField] private StringEvent onSelectBuildingObject = null;

        private BuildingObjectDefinition _definition = null;
        
        public void Setup(BuildingObjectDefinition definition)
        {
            _definition = definition;
            _nameLabel.SetText(definition.Key);
        }

        public void OnClick()
        {
            onSelectBuildingObject.Invoke(_definition.Key);
        }
    }
}
