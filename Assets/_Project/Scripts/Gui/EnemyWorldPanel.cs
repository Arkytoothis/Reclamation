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
    public class EnemyWorldPanel : MonoBehaviour
    {
        [SerializeField] private Image _actionIcon = null;
        [SerializeField] private Image _itemIcon = null;
        [SerializeField] private TMP_Text _itemsCarried = null;
        [SerializeField] private VitalBar _lifeBar = null;
        
        private Enemy _enemy = null;
        
        public void Setup(Enemy enemy)
        {
            _enemy = enemy;
            
            _itemIcon.enabled = false;
            _itemIcon.sprite = null;
            _itemsCarried.SetText("");
            
            SyncData();
        }
        
        public void Update()
        {
            Camera camera = Camera.main;
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }

        public void SyncData()
        {
            _lifeBar.UpdateData(_enemy.Attributes.GetVital("Life").Current, _enemy.Attributes.GetVital("Life").Maximum);
            
            if (_enemy.EnemyAgent == null || _enemy.EnemyAgent.CurrentAction == null)
            {
                _actionIcon.sprite = null;
                return;
            }

            if (_enemy.EnemyAgent.enabled == true)
            {
                _actionIcon.sprite = _enemy.EnemyAgent.CurrentAction.Icon;
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
