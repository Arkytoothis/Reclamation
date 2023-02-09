using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Interactables;
using Reclamation.Resources;
using Reclamation.Scene_Overworld;
using Reclamation.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Reclamation.Core
{
    public class OverworldRaycaster : MonoBehaviour
    {
        [SerializeField] private LayerMask _heroMask = new LayerMask();
        [SerializeField] private LayerMask _resourceNodeMask = new LayerMask();
        [SerializeField] private LayerMask _foundationMask = new LayerMask();

        private Camera _camera = null;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Utilities.IsMouseInWindow() == false)
            {
                //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                //Cursor.SetCursor(_guiCursor, Vector3.zero, CursorMode.Auto);
                return;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            //if (SelectHeroRaycast(ray)) return;
            if (CraftingStationRaycast(ray)) return;
            if (ResourceNodeRaycast(ray)) return;
            if (FoundationRaycast(ray)) return;
        }

        private bool SelectHeroRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, 999f, _heroMask))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Hero hero = hit.collider.gameObject.GetComponent<Hero>();
                    HeroManager.Instance.SelectHero(hero);

                    return true;
                }
            }

            return false;
        }

        private bool ResourceNodeRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, 999f, _resourceNodeMask))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    ResourceNode node = hit.collider.gameObject.GetComponent<ResourceNode>();
                    node.IsMarked = !node.IsMarked;

                    return true;
                }
            }

            return false;
        }

        private bool CraftingStationRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, 999f))
            {
                CraftingStation craftingStation = hit.collider.GetComponent<CraftingStation>();

                if (craftingStation != null && Input.GetMouseButtonUp(0))
                {
                    craftingStation.AddCraftingOrder();
                    return true;
                }
            }

            return false;
        }

        private bool FoundationRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, 999f, _foundationMask))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    BuildingObject buildingObject = hit.collider.GetComponent<BuildingObject>();
                    buildingObject.Select();

                    return true;
                }
            }

            return false;
        }
    }
}