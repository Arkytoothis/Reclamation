using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Core
{
    [CreateAssetMenu(fileName = "Damage Type Definition", menuName = "Reclamation/Definition/Damage Type Definition")]
    public class DamageTypeDefinition : ScriptableObject
    {
        [SerializeField] private string _name = "";
        [SerializeField] private string _key = "";
        [SerializeField] private Color _textColor = Color.white;
        
        public string Name => _name;
        public string Key => _key;
        public Color TextColor => _textColor;
    }
}