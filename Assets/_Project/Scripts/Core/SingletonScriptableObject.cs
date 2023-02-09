using System.Linq;
using UnityEngine;

namespace Reclamation.Core
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        static T _instance = null;

        public static T instance
        {
            get {
                if (!_instance)
                    _instance = UnityEngine.Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                return _instance;
            }
        }
    }
}