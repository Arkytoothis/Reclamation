using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Build
{
    public class Foundation : BuildingObject
    {
        [SerializeField] private List<WallMountIndicator> _wallMountIndicators = null;
        
        public override void Select()
        {
            Debug.Log(_definition.Name + " selected");
        }
    }
}