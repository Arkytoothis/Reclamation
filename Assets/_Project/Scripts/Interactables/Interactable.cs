using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Interactables
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        [SerializeField] protected Transform _interactionTransform = null;
        
        public abstract void Interact(Unit interacter);
    }
}