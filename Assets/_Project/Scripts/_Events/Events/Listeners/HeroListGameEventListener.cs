using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "HeroList")]
	public sealed class HeroListGameEventListener : BaseGameEventListener<List<Hero>, HeroListGameEvent, HeroListUnityEvent>
	{
	}
}