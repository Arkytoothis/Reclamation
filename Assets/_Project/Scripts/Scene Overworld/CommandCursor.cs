using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Units;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Reclamation.Scene_Overworld
{
    public class CommandCursor : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask = new LayerMask();
        [SerializeField] private LayerMask _heroMask = new LayerMask();
        [SerializeField] private RectTransform _selectionBox = null;
        
        [SerializeField] private HeroListEvent onSelectHeroes = null;

        private Vector2 _startPosition;
        private List<Hero> _selectedHeroes = null;

        private void Awake()
        {
            _selectedHeroes = new List<Hero>();
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (GroundRaycast(ray)) return;
            
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                foreach (Hero hero in _selectedHeroes)
                {
                    hero.Deselect();
                }
                
                _selectedHeroes.Clear();
                
                SelectHeroRaycast(ray);
                _startPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                ReleaseSelectionBox();
            }

            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    _selectionBox.sizeDelta = Vector2.zero;
                    return;
                }
                
                UpdateSelectionBox(Input.mousePosition);
            }
        }

        private void UpdateSelectionBox(Vector2 mousePosition)
        {
            if(!_selectionBox.gameObject.activeInHierarchy)
                _selectionBox.gameObject.SetActive(true);

            float width = mousePosition.x - _startPosition.x;
            float height = mousePosition.y - _startPosition.y;

            _selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            _selectionBox.anchoredPosition = _startPosition + new Vector2(width / 2, height / 2);
        }

        private void ReleaseSelectionBox()
        {
            _selectionBox.gameObject.SetActive(false);

            Vector2 min = _selectionBox.anchoredPosition - (_selectionBox.sizeDelta / 2);
            Vector2 max = _selectionBox.anchoredPosition + (_selectionBox.sizeDelta / 2);

            foreach (Hero hero in HeroManager.Instance.Heroes)
            {
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(hero.transform.position);

                if (screenPosition.x > min.x && screenPosition.x < max.x && screenPosition.y > min.y && screenPosition.y < max.y)
                {
                    _selectedHeroes.Add(hero);
                    hero.Select();
                }
            }

            if (_selectedHeroes.Count == 0)
            {
                onSelectHeroes.Invoke(null);
            }
            else
            {
                onSelectHeroes.Invoke(_selectedHeroes);
            }
        }
        
        private bool SelectHeroRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, 999f, _heroMask))
            {
                Hero hero = hit.collider.gameObject.GetComponent<Hero>();
                HeroManager.Instance.SelectHero(hero);
                _selectedHeroes.Add(hero);
                
                return true;
            }

            return false;
        }
        
        private bool GroundRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit, 999f, _groundMask))
            {
                transform.position = hit.point;
                if (InputManager.Instance.GetRightMouseDown())
                {
                    foreach (Hero hero in _selectedHeroes)
                    {
                        hero.MoveTo(hit.point);
                    }
                    
                    return true;
                }
            }

            return false;
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}