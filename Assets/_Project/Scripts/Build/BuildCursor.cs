using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Reclamation.Build
{
    public enum BuildModes { Off, Foundation, Floor, Wall, Number, None }
    
    public class BuildCursor : MonoBehaviour
    {
        public static BuildCursor Instance { get; private set; }
        
        [SerializeField] private LayerMask _groundMask = new LayerMask();
        [SerializeField] private LayerMask _foundationMask = new LayerMask();
        [SerializeField] private LayerMask _blueprintMask = new LayerMask();
        [SerializeField] private LayerMask _wallMountMask = new LayerMask();
        [SerializeField] private float _tileSize = 2.5f;
        [SerializeField] private float _foundationStartY = 43.5f;
        [SerializeField] private Transform _buildPreviewParent = null;
        
        private bool _isActive = false;
        private bool _canBuild = false;
        private BuildPreview _buildPreview = null;
        private BuildingObjectDefinition _currentBuildObject = null;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple BuildCursors " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        
        private void Update()
        {
            if (_isActive == false) return;
            if (Utilities.IsMouseInWindow() == false) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            //if (_currentBuildObject == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            switch (BuildingManager.Instance.BuildMode)
            {
                case BuildModes.Floor:
                    break;
                case BuildModes.Foundation:
                    if (GroundRaycast(ray)) return;
                    break;
                case BuildModes.Wall:
                    //if (WallMountRaycast(ray)) return;
                    break;
                case BuildModes.Off:
                    break;
                case BuildModes.None:
                    break;
            }
            
            //if (FoundationRaycast(ray)) return;
            //if (BlueprintRaycast(ray)) return;
        }

        private bool GroundRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, 999f, _groundMask))
            {
                transform.position = hit.point;
                PlaceFoundationPreview(transform.position);
                SetCanBuild(true);
                
                if (_canBuild == true && _currentBuildObject != null && InputManager.Instance.GetLeftMouseDown())
                {
                    PlaceBlueprint();
                }
                
                return true;
            }

            return false;
        }

        // private bool WallMountRaycast(Ray ray)
        // {
        //     Debug.Log("WallMountRaycast");
        //     if (Physics.Raycast(ray, out var hit, 999f))
        //     {
        //         Debug.Log(hit.collider.gameObject.name);
        //         WallMountIndicator indicator = hit.collider.GetComponent<WallMountIndicator>();
        //
        //         if (indicator == null) return false;
        //         
        //         Debug.Log("Placing Wall Preview");
        //         //transform.position = indicator.WallMount.position;
        //         PlacePreview(indicator.WallMount.position);
        //         SetCanBuild(true);
        //         
        //         return true;
        //     }
        //
        //     SetCanBuild(false);
        //     return false;
        // }
        
        // private bool FoundationRaycast(Ray ray)
        // {
        //     if (Physics.Raycast(ray, out var hit, 999f, _foundationMask))
        //     {
        //         transform.position = hit.point;
        //         PlacePreview(transform.position);
        //         SetCanBuild(false);
        //         
        //         return true;
        //     }
        //
        //     return false;
        // }
        //
        // private bool BlueprintRaycast(Ray ray)
        // {
        //     if (Physics.Raycast(ray, out var hit, 999f, _blueprintMask))
        //     {
        //         transform.position = hit.point;
        //         PlacePreview(transform.position);
        //         SetCanBuild(false);
        //         
        //         return true;
        //     }
        //
        //     return false;
        // }

        public void PlaceFoundationPreview(Vector3 position)
        {
            if (_buildPreview == null) return;

            _buildPreview.gameObject.SetActive(true);
            if (BuildingManager.Instance.BuildMode == BuildModes.Foundation)
            {
                float x = (Mathf.Round(position.x / _tileSize) * _tileSize) + (_tileSize * 0.5f);
                float y = _foundationStartY;
                float z = (Mathf.Round(position.z / _tileSize) * _tileSize) + (_tileSize * 0.5f);
                _buildPreview.transform.position = new Vector3(x, y, z);
            }
        }
        
        public void PlaceWallPreview(Vector3 position, Quaternion rotation)
        {
            if (_buildPreview == null) return;

            _buildPreview.gameObject.SetActive(true);
            if (BuildingManager.Instance.BuildMode == BuildModes.Wall)
            {
                _buildPreview.SetCanBuild(true);
                _buildPreview.transform.position = position;
                _buildPreview.transform.rotation = rotation;
            }
        }

        public void ClearPreview()
        {
            _buildPreview.SetCanBuild(false);
            _buildPreview.gameObject.SetActive(false);
        }
        
        private void SpawnPreview()
        {
            _buildPreviewParent.ClearTransform();
            GameObject clone = Instantiate(_currentBuildObject.PreviewPrefab, _buildPreviewParent);
            _buildPreview = clone.GetComponent<BuildPreview>();
        }
        
        private void ShowPreview()
        {
            if (_buildPreview != null)
            {
                _buildPreview.gameObject.SetActive(true);
            }
        }

        private void HidePreview()
        {
            if (_buildPreview != null)
            {
                _buildPreview.gameObject.SetActive(false);
            }
        }
        
        private void PlaceBlueprint()
        {
            float spawnX = (Mathf.Round(transform.position.x / _tileSize) * _tileSize) + (_tileSize * 0.5f);
            float spawnY = _foundationStartY;
            float spawnZ = (Mathf.Round(transform.position.z / _tileSize) * _tileSize) + (_tileSize * 0.5f);
            GameObject clone = Instantiate(_currentBuildObject.BlueprintPrefab, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
            Blueprint blueprint = clone.GetComponent<Blueprint>();
            BuildingManager.Instance.RegisterBuildBlueprint(blueprint);
        }
        
        public void PlaceWallBlueprint(Vector3 position, Quaternion rotation)
        {
            GameObject clone = Instantiate(_currentBuildObject.BlueprintPrefab, position, rotation);
            Blueprint blueprint = clone.GetComponent<Blueprint>();
            BuildingManager.Instance.RegisterBuildBlueprint(blueprint);
        }

        public void SetActive(bool active)
        {
            _isActive = active;

            if (_isActive == false)
            {
                transform.position = new Vector3(0, 0, 0);
            }
        }

        private void SetCanBuild(bool canBuild)
        {
            _canBuild = canBuild;
            if (_buildPreview != null)
            {
                _buildPreview.SetCanBuild(_canBuild);
            }
        }

        public void OnSetBuildMode(int mode)
        {
            BuildingManager.Instance.SetBuildMode((BuildModes)mode);

            if (BuildingManager.Instance.BuildMode == BuildModes.Off)
            {
                SetCanBuild(false);
                SetActive(false);
                HidePreview();
                _buildPreviewParent.ClearTransform();
                _buildPreview = null;
            }
            else if (BuildingManager.Instance.BuildMode == BuildModes.Foundation)
            {
                SetCanBuild(true);
                SetActive(true);
                ShowPreview();
            }
            else if (BuildingManager.Instance.BuildMode == BuildModes.Floor)
            {
                SetCanBuild(true);
                SetActive(true);
                ShowPreview();
            }
            else if (BuildingManager.Instance.BuildMode == BuildModes.Wall)
            {
                SetCanBuild(true);
                SetActive(true);
                ShowPreview();
            }
        }

        public void OnSelectBuildingObject(string key)
        {
            BuildingObjectDefinition definition = Database.instance.BuildingObjects.GetBuildingObject(key);

            if (definition == null) return;
                
            _currentBuildObject = definition;

            if (_currentBuildObject.ObjectType == BuildingObjectTypes.Foundation)
            {
                OnSetBuildMode((int)BuildModes.Foundation);
                SpawnPreview();
            }
            else if (_currentBuildObject.ObjectType == BuildingObjectTypes.Floor)
            {
                OnSetBuildMode((int)BuildModes.Floor);
                //SpawnPreview();
            }
            else if (_currentBuildObject.ObjectType == BuildingObjectTypes.Wall)
            {
                OnSetBuildMode((int)BuildModes.Wall);
                SpawnPreview();
            }
        }
    }
}