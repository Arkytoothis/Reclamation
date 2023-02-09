using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Core;
using Reclamation.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reclamation.Gui
{
    public class HeroWorldPanel : MonoBehaviour
    {
        [SerializeField] private Image _actionIcon = null;
        [SerializeField] private Image _itemIcon = null;
        [SerializeField] private TMP_Text _itemsCarried = null;
        
        private Hero _hero = null;
        
        public void Setup(Hero hero)
        {
            _hero = hero;
            SyncData();
        }
        
        public void Update()
        {
            Camera camera = Camera.main;
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }

        public void SyncData()
        {
            if (_hero.PlayerAgent == null || _hero.PlayerAgent.CurrentAction == null)
            {
                _actionIcon.sprite = null;
                return;
            }

            if (_hero.PlayerAgent.enabled == true)
            {
                _actionIcon.sprite = _hero.PlayerAgent.CurrentAction.Icon;
                if (_hero.ResourceController.ItemCarried.StackSize > 0)
                {
                    _itemIcon.enabled = true;
                    _itemIcon.sprite = _hero.ResourceController.ItemCarried.Icon;
                    _itemsCarried.SetText("x" + _hero.ResourceController.ItemCarried.StackSize.ToString());
                }
                else
                {
                    _itemIcon.enabled = false;
                    _itemIcon.sprite = null;
                    _itemsCarried.SetText("");
                }
            }
            else
            {
                _actionIcon.sprite = Database.instance.DraftedSprite;
                _itemIcon.enabled = false;
                _itemIcon.sprite = null;
                _itemsCarried.SetText("");
            }
        }
    }
}
