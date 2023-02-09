using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Resources;
using UnityEngine;

namespace Reclamation.Gui
{
    public class ResourcesPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _widgetPrefab = null;
        [SerializeField] private Transform _widgetsParent = null;
        
        private ResourceWidgetDictionary _widgets = null;
        
        public void Setup()
        {
            _widgets = new ResourceWidgetDictionary();
            
            foreach (var itemKvp in Database.instance.Items.Dictionary)
            {
                if(itemKvp.Value.IngredientData.HasData == false) continue;
                
                GameObject clone = Instantiate(_widgetPrefab, _widgetsParent);
                ResourceWidget widget = clone.GetComponent<ResourceWidget>();
                widget.Setup(itemKvp.Value);
                _widgets.Add(itemKvp.Key, widget);
            }    
        }

        public void OnSyncStockpile(bool b)
        {
            int index = 0;
            foreach (Item item in StockpileManager.Instance.Items)
            {
                if(item.ItemDefinition.IngredientData.HasData == false) continue;
                
                _widgets[item.Key].SetAmount(item.StackSize);
                index++;
            }
        }
    }
}