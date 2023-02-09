using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "HeroListCollection.asset",
	    menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "HeroList Event",
	    order = 120)]
	public class HeroListCollection : Collection<List<Hero>>
	{
	}
}