using Sirenix.OdinInspector;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Units;
using UnityEngine;

namespace Reclamation.Abilities
{
    [System.Serializable]
    public abstract class AbilityEffect
    {
        [SerializeField] protected AbilityEffectAffects _affects = AbilityEffectAffects.None;

        public AbilityEffectAffects Affects { get => _affects; set => _affects = value; }

        public virtual string GetTooltipText() { return ""; }
        public virtual void Process(Unit user, List<Unit> targets) { }
    }
}