using System.Collections;
using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Interactables
{
    public interface IInteractable
    {
        public void Interact(Unit interacter);
    }
}