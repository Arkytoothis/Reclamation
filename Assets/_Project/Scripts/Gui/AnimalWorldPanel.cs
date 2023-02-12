using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reclamation.Gui
{
    public class AnimalWorldPanel : MonoBehaviour
    {
        [SerializeField] private Image _actionIcon = null;
        [SerializeField] private Image _itemIcon = null;
        [SerializeField] private TMP_Text _itemsCarried = null;
        [SerializeField] private VitalBar _lifeBar = null;
        
        private Animal _animal = null;
        private Camera _camera = null;
        
        public void Setup(Animal animal)
        {
            _animal = animal;
            _camera = Camera.main;
            _itemIcon.enabled = false;
            _itemIcon.sprite = null;
            _itemsCarried.SetText("");
            
            SyncData();
        }
        
        public void Update()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
        }

        public void SyncData()
        {
            _lifeBar.UpdateData(_animal.Attributes.GetVital("Life").Current, _animal.Attributes.GetVital("Life").Maximum);
            
            if (_animal.AnimalAgent == null || _animal.AnimalAgent.CurrentAction == null)
            {
                _actionIcon.sprite = null;
                return;
            }

            if (_animal.AnimalAgent.enabled == true)
            {
                _actionIcon.sprite = _animal.AnimalAgent.CurrentAction.Icon;
                // if (_enemy.ResourceController.ItemCarried != null && _enemy.ResourceController.ItemCarried.StackSize > 0)
                // {
                //     _itemIcon.enabled = true;
                //     _itemIcon.sprite = _enemy.ResourceController.ItemCarried.Icon;
                //     _itemsCarried.SetText("x" + _enemy.ResourceController.ItemCarried.StackSize.ToString());
                // }
                // else
                // {
                    // _itemIcon.enabled = false;
                    // _itemIcon.sprite = null;
                    // _itemsCarried.SetText("");
                //}
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
