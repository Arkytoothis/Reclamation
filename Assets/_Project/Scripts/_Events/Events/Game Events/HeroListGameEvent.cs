using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "HeroListGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "HeroList Event",
	    order = 120)]
	public sealed class HeroListGameEvent : GameEventBase<List<Hero>>
	{
	}
}