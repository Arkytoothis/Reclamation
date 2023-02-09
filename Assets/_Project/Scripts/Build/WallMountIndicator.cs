using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Reclamation.Build
{
    public class WallMountIndicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private GameObject _visual = null;
        [SerializeField] private Transform _wallMount = null;

        private bool _canBuild = false;

        public Transform WallMount => _wallMount;
        public bool CanBuild => _canBuild;

        private void Start()
        {
            Hide();
        }

        private void Show()
        {
            _visual.SetActive(true);
        }
        
        private void Hide()
        {
            _visual.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (BuildingManager.Instance.BuildMode != BuildModes.Wall) return;
            
            Show();
            BuildCursor.Instance.PlaceWallPreview(_wallMount.position, _wallMount.rotation);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (BuildingManager.Instance.BuildMode != BuildModes.Wall) return;
            
            Hide();
            BuildCursor.Instance.ClearPreview();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (BuildingManager.Instance.BuildMode != BuildModes.Wall) return;
            
            BuildCursor.Instance.PlaceWallBlueprint(_wallMount.position, _wallMount.rotation);
        }
    }
}