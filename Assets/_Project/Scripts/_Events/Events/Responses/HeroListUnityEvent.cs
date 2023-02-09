using System.Collections.Generic;
using Reclamation.Units;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class HeroListUnityEvent : UnityEvent<List<Hero>>
	{
	}
}