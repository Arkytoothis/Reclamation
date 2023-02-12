using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Interactables
{
    public interface IInteractionPoint
    {
        public Transform GetInteractionPoint();
    }
}