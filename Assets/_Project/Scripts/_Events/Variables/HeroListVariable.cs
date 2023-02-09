using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class HeroListEvent : UnityEvent<List<Hero>> { }

	[CreateAssetMenu(
	    fileName = "HeroListVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "HeroList Event",
	    order = 120)]
	public class HeroListVariable : BaseVariable<List<Hero>, HeroListEvent>
	{
	}
}