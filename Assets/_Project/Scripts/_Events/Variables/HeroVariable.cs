using Reclamation.Units;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class HeroEvent : UnityEvent<Hero> { }

	[CreateAssetMenu(
	    fileName = "HeroVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Hero Event",
	    order = 120)]
	public class HeroVariable : BaseVariable<Hero, HeroEvent>
	{
	}
}