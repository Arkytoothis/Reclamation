using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reclamation.Gui
{
    public class VitalBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueLabel = null;
        [SerializeField] private Image _foregroundImage = null;

        public void UpdateData(int current, int maximum)
        {
            float fillAmount = (float)current / maximum;
            _foregroundImage.fillAmount = fillAmount;
            
            if(_valueLabel != null)
            {
                _valueLabel.SetText(current + " / " + maximum);
            }
        }
    }
}
