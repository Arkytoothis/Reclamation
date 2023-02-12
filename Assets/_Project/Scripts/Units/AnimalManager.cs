using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Units
{
    public class AnimalManager : MonoBehaviour
    {
        [SerializeField] private List<Animal> _animals = null;

        public List<Animal> Animals => _animals;

        public static AnimalManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple AnimalManagers " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            _animals = new List<Animal>();
        }

        public void RegisterAnimal(Animal animal)
        {
            if (_animals.Contains(animal) == false)
            {
                _animals.Add(animal);
            }
        }

        public Animal FindClosestAnimal(Transform searcher)
        {
            if (_animals == null || _animals.Count == 0) return null;
            
            _animals.Sort(delegate(Animal a, Animal b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            return _animals[0];
        }

        public void RemoveAnimal(Animal animal)
        {
            if (_animals.Contains(animal))
            {
                _animals.Remove(animal);
            }
        }
    }
}