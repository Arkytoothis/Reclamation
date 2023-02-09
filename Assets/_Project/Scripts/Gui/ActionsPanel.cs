using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Core;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Reclamation.Gui
{
    public class ActionsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _actionWidgetPrefab = null;
        [SerializeField] private Transform _actionWidgetsParent = null;
        [SerializeField] private GameObject _actionsContainer = null;
        
        [SerializeField] private IntEvent onSetBuildCursorActive = null;

        private List<ActionWidget> _actionWidgets = null;
        private BuildModes _buildMode = BuildModes.None;
        
        public void Setup()
        {
            _actionWidgets = new List<ActionWidget>();
            _actionsContainer.SetActive(false);
            onSetBuildCursorActive.Invoke((int)BuildModes.Off);
        }

        private void Update()
        {
            if (_buildMode != BuildModes.Off)
            {
                if (InputManager.Instance.GetKeyDown(KeyCode.Escape))
                {
                    onSetBuildCursorActive.Invoke((int)BuildModes.Off);
                    Clear();
                }
            }
        }

        public void BuildFoundationButton_Click()
        {
            _buildMode = BuildModes.Foundation;
            _actionsContainer.SetActive(true);
            onSetBuildCursorActive.Invoke((int)_buildMode);
            LoadActionButtons(BuildingObjectTypes.Foundation);
        }
        
        public void BuildFloorButton_Click()
        {
            _buildMode = BuildModes.Floor;
            _actionsContainer.SetActive(true);
            onSetBuildCursorActive.Invoke((int)_buildMode);
            LoadActionButtons(BuildingObjectTypes.Floor);
        }
        
        public void BuildWallButton_Click()
        {
            _buildMode = BuildModes.Wall;
            _actionsContainer.SetActive(true);
            onSetBuildCursorActive.Invoke((int)_buildMode);
            LoadActionButtons(BuildingObjectTypes.Wall);
        }

        private void LoadActionButtons(BuildingObjectTypes type)
        {
            Clear();
            
            foreach (var buildingObjectKvp in Database.instance.BuildingObjects.Dictionary)
            {
                if (buildingObjectKvp.Value.ObjectType == type)
                {
                    GameObject clone = Instantiate(_actionWidgetPrefab, _actionWidgetsParent);
                    ActionWidget widget = clone.GetComponent<ActionWidget>();
                    widget.Setup(buildingObjectKvp.Value);
                    _actionWidgets.Add(widget);
                }
            }
        }

        private void Clear()
        {
            _actionWidgets.Clear();
            _actionWidgetsParent.ClearTransform();
        }
    }
}