using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Build;
using Reclamation.Core;
using Reclamation.Resources;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Scene_Overworld
{
    public class OverworldManager : MonoBehaviour
    {
        [SerializeField] private Database _database = null;
        [SerializeField] private GameObject _guiPrefab = null;
        [SerializeField] private Transform _guiParent = null;

        private void Awake()
        {
            SpawnGui();
        }

        private void Start()
        {
            _database.Setup();
            InputManager.Instance.Setup();
            
            ResourcesManager.Instance.Setup();
            BuildingManager.Instance.Setup();
            HeroManager.Instance.Setup();
            PortraitRoom.Instance.Setup();
            HeroManager.Instance.SyncHeroes();
        }

        private void SpawnGui()
        {
            GameObject clone = Instantiate(_guiPrefab, _guiParent);
            clone.GetComponent<GuiManager>().Setup();
        }
    }
}