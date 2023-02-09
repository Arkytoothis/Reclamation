using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Reclamation.Gui
{
    public class HeroWidget : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _actionIcon = null;
        [SerializeField] private Image _carriedItemIcon = null;
        [SerializeField] private Image _requiredItemIcon = null;
        [SerializeField] private RawImage _portrait = null;
        [SerializeField] private TMP_Text _nameLabel = null;

        private Hero _hero = null;
        private int _index = -1;
        
        public void Setup(Hero hero, int index)
        {
            _hero = hero;
            _index = index;
            Refresh();
        }

        public void Refresh()
        {
            if (_hero == null) return;

            if (_hero.PlayerAgent.CurrentAction != null)
            {
                _actionIcon.sprite = _hero.PlayerAgent.CurrentAction.Icon;
            }
            
            if (_hero.ResourceController.ItemCarried.IsEmpty() == false)
            {
                _carriedItemIcon.enabled = true;
                _carriedItemIcon.sprite = _hero.ResourceController.ItemCarried.Icon;
            }
            else
            {
                _carriedItemIcon.enabled = false;
            }
            
            if (_hero.ResourceController.ItemRequired.IsEmpty() == false)
            {
                _requiredItemIcon.enabled = true;
                _requiredItemIcon.sprite = _hero.ResourceController.ItemRequired.Icon;
            }
            else
            {
                _requiredItemIcon.enabled = false;
            }

            _portrait.texture = _hero.Portrait.RtClose;
            _nameLabel.SetText(_hero.HeroData.Name.FirstName);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_hero == null) return;
            
            HeroManager.Instance.SelectHero(_hero);
        }
    }
}