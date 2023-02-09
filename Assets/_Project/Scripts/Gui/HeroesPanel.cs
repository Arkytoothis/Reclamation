using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Gui
{
    public class HeroesPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _heroWidgetPrefab = null;
        [SerializeField] private Transform _heroWidgetsParent = null;

        private List<HeroWidget> _heroWidgets = null;
        
        public void Setup()
        {
            _heroWidgets = new List<HeroWidget>();
        }

        public void OnSyncHeroes(bool b)
        {
            SyncHeroes();
        }
        
        public void OnSyncHero(Hero hero)
        {
            _heroWidgets[hero.HeroData.ListIndex].Refresh();
        }
        
        private void SyncHeroes()
        {
            if (_heroWidgets.Count == 0)
            {
                _heroWidgets = new List<HeroWidget>();
                _heroWidgetsParent.ClearTransform();

                for (int i = 0; i < HeroManager.Instance.Heroes.Count; i++)
                {
                    GameObject clone = Instantiate(_heroWidgetPrefab, _heroWidgetsParent);
                    HeroWidget widget = clone.GetComponent<HeroWidget>();
                    widget.Setup(HeroManager.Instance.Heroes[i], i);
                    _heroWidgets.Add(widget);
                }
            }
            else
            {
                foreach (HeroWidget widget in _heroWidgets)
                {
                    widget.Refresh();
                }
            }
        }
    }
}