using Reclamation.Units;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "HeroGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Hero Event",
	    order = 120)]
	public sealed class HeroGameEvent : GameEventBase<Hero>
	{
	}
}