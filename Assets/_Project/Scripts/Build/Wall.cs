using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Build
{
    public class Wall : BuildingObject
    {
        public override void Select()
        {
            Debug.Log(_definition.Name + " selected");
        }
    }
}