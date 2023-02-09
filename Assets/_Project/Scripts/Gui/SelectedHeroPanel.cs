using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Core;
using Reclamation.Units;
using TMPro;
using UnityEngine;

namespace Reclamation.Gui
{
    public class SelectedHeroPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _singleHeroContainer = null;
        [SerializeField] private GameObject _multiHeroContainer = null;
        [SerializeField] private TMP_Text _nameLabel = null;
        [SerializeField] private TMP_Text _currentActionLabel = null;
        [SerializeField] private TMP_Text _carriedItemLabel = null;
        [SerializeField] private TMP_Text _requiredItemLabel = null;
        
        [SerializeField] private GameObject _heroWidgetPrefab = null;
        [SerializeField] private Transform _heroWidgetsParent = null;
        
        private List<Hero> _selectedHeroes = null;
        private List<SelectedHeroWidget> _selectedHeroWidgets = null;
        private int _selectedIndex = -1;
        
        public void Setup()
        {
            
        }
        
        public void OnSelectHeroes(List<Hero> selectedHeroes)
        {
            if (selectedHeroes != null)
            {
                _selectedHeroes = selectedHeroes;

                if (selectedHeroes.Count == 1)
                {
                    _singleHeroContainer.SetActive(true);
                    _multiHeroContainer.SetActive(false);
                    DisplaySingleHero(0);
                }
                else if (selectedHeroes.Count > 1)
                {
                    _singleHeroContainer.SetActive(false);
                    _multiHeroContainer.SetActive(true);
                    DisplaySelectedHeroes();
                }
            }
            else
            {
                _selectedHeroes = null;
                _singleHeroContainer.SetActive(false);
                _multiHeroContainer.SetActive(false);
            }
        }

        private void DisplaySingleHero(int index)
        {
            if (_selectedHeroes == null || index == -1 || _selectedHeroes[index] == null) return;
            _selectedIndex = index;
            
            _nameLabel.SetText(_selectedHeroes[_selectedIndex].HeroData.Name.FullName);

            if (_selectedHeroes[_selectedIndex].PlayerAgent.enabled == false)
            {
                _currentActionLabel.SetText("Drafted");
                return;
            }
            
            if (_selectedHeroes[_selectedIndex].PlayerAgent.CurrentAction != null)
            {
                _currentActionLabel.SetText("Current Action: " + _selectedHeroes[_selectedIndex].GetComponent<Agent>().CurrentAction.ActionName);
            }

            UnitResourceController unitResources = _selectedHeroes[0].GetComponentInChildren<UnitResourceController>();

            if (unitResources == null) return;
            
            if (unitResources.ItemCarried != null)
            {
                _carriedItemLabel.SetText("Carried Item: " + unitResources.ItemCarried.Name + " x" + unitResources.ItemCarried.StackSize);
            }
            else
            {
                _carriedItemLabel.SetText("");
            }
                
            if (unitResources.ItemRequired != null)
            {
                _requiredItemLabel.SetText("Required Item: " + unitResources.ItemRequired.Name + " x" + unitResources.ItemRequired.StackSize);
            }
            else
            {
                _requiredItemLabel.SetText("");
            }
        }

        private void DisplaySelectedHeroes()
        {
            _selectedHeroWidgets = new List<SelectedHeroWidget>();
            _heroWidgetsParent.ClearTransform();
            
            int index = 0;
            foreach (Hero hero in _selectedHeroes)
            {
                GameObject clone = Instantiate(_heroWidgetPrefab, _heroWidgetsParent);
                SelectedHeroWidget widget = clone.GetComponent<SelectedHeroWidget>();
                widget.Setup(this, hero, index);
                _selectedHeroWidgets.Add(widget);
                index++;
            }
        }
        
        public void OnSyncData(bool b)
        {
            DisplaySingleHero(_selectedIndex);
        }

        public void OnDraftButtonClick()
        {
            if (_selectedHeroes == null) return;
            
            foreach (Hero hero in _selectedHeroes)
            {
                hero.Draft();
            }
        }

        public void OnUndraftButtonClick()
        {
            if (_selectedHeroes == null) return;
            
            foreach (Hero hero in _selectedHeroes)
            {
                hero.UnDraft();
            }
            // if (_selectedHeroes[0] == null) return;
            //
            // _selectedHeroes[0].UnDraft();
            // DisplaySingleHero();
        }
    }
}