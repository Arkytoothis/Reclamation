using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class HeroListReference : BaseReference<List<Hero>, HeroListVariable>
	{
	    public HeroListReference() : base() { }
	    public HeroListReference(List<Hero> value) : base(value) { }
	}
}