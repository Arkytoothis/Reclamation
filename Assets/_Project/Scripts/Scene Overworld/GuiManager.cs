using System.Collections;
using System.Collections.Generic;
using Reclamation.Gui;
using Reclamation.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reclamation.Scene_Overworld
{
    public class GuiManager : MonoBehaviour
    {
        [SerializeField] private GameObject _selectedHeroPanelPrefab = null;
        [SerializeField] private GameObject _actionsPanelPrefab = null;
        [SerializeField] private GameObject _resourcesPanelPrefab = null;
        [SerializeField] private GameObject _heroesPanelPrefab = null;
        
        [SerializeField] private SelectedHeroPanel _selectedHeroPanel = null;
        [SerializeField] private ActionsPanel _actionsPanel = null;
        [SerializeField] private ResourcesPanel _resourcesPanel = null;
        [SerializeField] private HeroesPanel _heroesPanel = null;
        
        public void Setup()
        {
            SpawnHeroesPanel();
            SpawnActionsPanel();
            SpawnResourcesPanel();
            SpawnSelectedHeroPanel();
        }

        private void SpawnHeroesPanel()
        {
            GameObject clone = Instantiate(_heroesPanelPrefab, transform);
            _heroesPanel = clone.GetComponent<HeroesPanel>();
            _heroesPanel.Setup();
        }

        private void SpawnSelectedHeroPanel()
        {
            GameObject clone = Instantiate(_selectedHeroPanelPrefab, transform);
            _selectedHeroPanel = clone.GetComponent<SelectedHeroPanel>();
            _selectedHeroPanel.Setup();
        }

        private void SpawnActionsPanel()
        {
            GameObject clone = Instantiate(_actionsPanelPrefab, transform);
            _actionsPanel = clone.GetComponent<ActionsPanel>();
            _actionsPanel.Setup();
        }

        private void SpawnResourcesPanel()
        {
            GameObject clone = Instantiate(_resourcesPanelPrefab, transform);
            _resourcesPanel = clone.GetComponent<ResourcesPanel>();
            _resourcesPanel.Setup();
        }
    }
}