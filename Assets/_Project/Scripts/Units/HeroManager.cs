using System.Collections;
using System.Collections.Generic;
using Reclamation.Attributes;
using Reclamation.Core;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Reclamation.Units
{
    public class HeroManager : MonoBehaviour
    {
        public static HeroManager Instance { get; private set; }
        
        [SerializeField] private GameObject _heroPrefab = null;
        [SerializeField] private Transform _heroesParent = null;
        [SerializeField] private Transform _spawnPoint = null;
        [SerializeField] private List<Hero> _heroes = null;

        [SerializeField] private HeroEvent onSelectHero = null;
        [SerializeField] private BoolEvent onSyncHeroes = null;

        private Hero _selectedHero = null;

        public List<Hero> Heroes => _heroes;
        public Hero SelectedHero => _selectedHero;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple HeroManagers " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            _heroes = new List<Hero>();
        }
        
        public void Setup()
        {
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Soldier"), _heroes.Count, new Vector3( 0f, 0f,-8f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Forester"), _heroes.Count, new Vector3( 0f, 0f,0f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Miner"), _heroes.Count,    new Vector3( -2f,0f,0f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Laborer"), _heroes.Count,  new Vector3( 0f,0f,-2f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Laborer"), _heroes.Count,  new Vector3( -2f,0f,-2f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Builder"), _heroes.Count,  new Vector3( -0f,0f,-4f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Crafter"), _heroes.Count,  new Vector3( -2f,0f,-4f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Forager"), _heroes.Count,  new Vector3( 0f,0f,-6f));
            SpawnHero(Utilities.GetRandomGender(), Utilities.RandomValues(Database.instance.Races.Races), Database.instance.Profession.GetProfession("Hunter"), _heroes.Count,  new Vector3( -2f,0f,-6f));
            
        }

        private void SpawnHero(Genders gender, RaceDefinition race, ProfessionDefinition profession, int index, Vector3 spawnOffset)
        {
            GameObject clone = Instantiate(_heroPrefab, _heroesParent);
            clone.transform.position = _spawnPoint.position + spawnOffset;
            
            Hero hero = clone.GetComponent<Hero>();
            hero.SetupHero(gender, race, profession, index);
            clone.name = hero.HeroData.Name.ShortName;
            _heroes.Add(hero);
        }

        public void SelectHero(Hero hero)
        {
            for (int i = 0; i < _heroes.Count; i++)
            {
                _heroes[i].Deselect();
            }
            
            _selectedHero = hero;
            _selectedHero.Select();
            onSelectHero.Invoke(_selectedHero);
        }

        // public void MovementOrder(Transform target)
        // {
        //     _selectedHero.MoveTo(target);
        // }

        public void SyncHeroes()
        {
            onSyncHeroes.Invoke(true);
        }

        public void MoveSelectedUnits(Vector3 position)
        {
            if (_selectedHero == null) return;
            
            _selectedHero.MoveTo(position);
        }

        public Hero FindClosestHero(Transform searcher)
        {
            List<Hero> heroTargets = new List<Hero>();

            foreach (Hero hero in _heroes)
            {
                heroTargets.Add(hero);
            }
            
            heroTargets.Sort(delegate(Hero a, Hero b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            return heroTargets[0];
        }
    }
}