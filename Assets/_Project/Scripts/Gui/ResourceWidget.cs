using System.Collections;
using System.Collections.Generic;
using Reclamation.Equipment;
using Reclamation.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reclamation.Gui
{
    public class ResourceWidget : MonoBehaviour
    {
        [SerializeField] private Image _resourceIcon = null;
        [SerializeField] private TMP_Text _resourceLabel = null;

        public void Setup(ItemDefinition itemDefinition)
        {
            _resourceIcon.sprite = itemDefinition.Icon;
            SetAmount(0);
        }

        public void SetAmount(int amount)
        {
            _resourceLabel.SetText(amount.ToString());
        }
    }
}
