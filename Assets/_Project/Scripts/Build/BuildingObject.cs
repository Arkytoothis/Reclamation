using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Build
{
    public abstract class BuildingObject : MonoBehaviour
    {
        [SerializeField] protected BuildingObjectDefinition _definition = null;
        
        public abstract void Select();
    }
}