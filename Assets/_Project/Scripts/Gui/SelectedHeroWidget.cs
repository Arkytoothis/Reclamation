using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Reclamation.Gui
{
    public class SelectedHeroWidget : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RawImage _portrait = null;

        private SelectedHeroPanel _heroPanel = null;
        private Hero _hero = null;
        private int _index = -1;
        
        public void Setup(SelectedHeroPanel heroPanel, Hero hero, int index)
        {
            _heroPanel = heroPanel;
            _hero = hero;
            _index = index;
            Refresh();
        }

        public void Refresh()
        {
            if (_hero == null) return;

            _portrait.texture = _hero.Portrait.RtClose;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_hero == null) return;
            
            HeroManager.Instance.SelectHero(_hero);
            _heroPanel.OnSelectHeroes(new List<Hero> { _hero });
        }
    }
}