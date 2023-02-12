using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Attributes;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Gui;
using UnityEngine;

namespace Reclamation.Units
{
    public class Animal : Unit, IDamageSystem
    {
        [SerializeField] private AnimalDefinition _animalDefinition = null;
        [SerializeField] private ActionController _actionController = null;
        [SerializeField] private UnitResourceController _resourceController = null;
        [SerializeField] private AnimalAgent _animalAgent = null;
        [SerializeField] private AgentVisual _agentVisual = null;
        [SerializeField] private AnimalWorldPanel _worldPanel = null;
        
        public AnimalDefinition AnimalDefinition => _animalDefinition;
        public AnimalAgent AnimalAgent => _animalAgent;
        public AgentVisual AgentVisual => _agentVisual;

        protected override void Start()
        {
            base.Start();
            AnimalManager.Instance.RegisterAnimal(this);
            SetupAnimal(-1);
        }

        public void SetupAnimal(int listIndex)
        {
            _attributes.Setup(_animalDefinition);
            _isSelected = false;
            
            _animalAgent = GetComponent<AnimalAgent>();
            _agentVisual = GetComponent<AgentVisual>();
            
            _actionController.Setup();
            _unitAnimator.SetAnimatorOverride(_animalDefinition.AnimatorOverride);
            
            _animalAgent.AddGoal(StateManager.Instance.Idle.Name, 1, false, 1);
            //_animalAgent.ModifyState(StateManager.Instance.Idle.Name, 0);
            
            _worldPanel.Setup(this);
        }
        
        public override void SpendActionPoints(int actionPointCost)
        {
            
        }

        public override string GetFullName()
        {
            return "";
        }

        public override string GetShortName()
        {
            return "";
        }

        public override Item GetMeleeWeapon()
        {
            return null;
        }

        public override Item GetRangedWeapon()
        {
            return null;
        }

        public override void Damage(GameObject attacker, DamageTypeDefinition damageType, int damage, string vital)
        {
            if (_isAlive == false) return;

            _attributes.ModifyVital("Life", damage, false);
            //Debug.Log(_enemyDefinition.Name + " takes " + damage + " " + vital + " damage");
            //CombatTextHandler.Instance.DisplayCombatText(new CombatText(_combatTextTransform.position, damage.ToString(), "default"));

            if (GetHealth() <= 0)
            {
                Dead();
            }
            
            _worldPanel.SyncData();
        }

        public override void RestoreVital(string vital, int damage)
        {
            
        }

        public override void UseResource(string vital, int damage)
        {
            
        }

        protected override void Dead()
        {
            _isAlive = false;
            AnimalManager.Instance.RemoveAnimal(this);
            //_spawner.EnemyDied(this);
            Destroy(gameObject);
        }

        public override void SyncData()
        {
            //onSyncHero.Invoke(this);
            _worldPanel.SyncData();
        }

        public void TakeDamage(GameObject attacker, int amount, string vital)
        {
            Damage(attacker, null, amount, vital);
        }

        public void SetDestination(Transform destination)
        {
            _pathfinder.SetDestination(destination);    
        }
    }
}