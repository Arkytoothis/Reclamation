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
        [SerializeField] private Transform _formationParent = null;
        [SerializeField] private List<Transform> _formation = null;
        
        [SerializeField] private HeroListEvent onSelectHeroes = null;

        private Vector2 _startPosition;
        private List<Hero> _selectedHeroes = null;

        private void Awake()
        {
            _selectedHeroes = new List<Hero>();
            _formation = new List<Transform>();
            
            foreach (Transform child in _formationParent)
            {
                _formation.Add(child);
            }
            
            SetFormationShown(false);
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
            if (_selectedHeroes.Count == 0) return false;
            
            if (Physics.Raycast(ray, out var hit, 999f, _groundMask))
            {
                transform.position = hit.point;

                if (InputManager.Instance.GetRightMouseDown())
                {
                    _formationParent.position = transform.position;
                    StartCoroutine(SetFormationShown_Delayed(true));
                }

                else if (Input.GetMouseButton(1))
                {
                    _formationParent.LookAt(_selectedHeroes[0].transform.position, Vector3.up);
                    _formationParent.Rotate(Vector3.up, 180f);
                    _formationParent.position = transform.position;
                    _formationParent.Translate(0,0,-1.1f, Space.Self);
                }
                
                else if (Input.GetMouseButtonUp(1))
                {
                    if (_selectedHeroes.Count == 1)
                    {
                        _selectedHeroes[0].MoveTo(_formation[1].position);
                    }
                    else
                    {
                        int formationIndex = 0;
                        foreach (Hero hero in _selectedHeroes)
                        {
                            hero.MoveTo(_formation[formationIndex].position);
                            formationIndex++;
                        }
                    }

                    SetFormationShown(false);
                }
            }

            return false;
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void SetFormationShown(bool shown)
        {
            _formationParent.gameObject.SetActive(shown);
        }

        private IEnumerator SetFormationShown_Delayed(bool shown)
        {
            yield return null;
            
            SetFormationShown(shown);
        }
    }
}