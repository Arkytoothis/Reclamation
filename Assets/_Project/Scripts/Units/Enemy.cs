using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Units
{
    public class Enemy : Unit
    {
        [SerializeField] private ActionController _actionController = null;
        [SerializeField] private EnemyAgent _enemyAgent = null;
        [SerializeField] private AgentVisual _agentVisual = null;
        
        public void SetupEnemy(int listIndex)
        {
            // _attributes.Setup(race, profession);
            // _skills.Setup(_attributes, race, profession);
            // _inventory.Setup(_portraitRenderer, _worldRenderer, gender, race, profession);
            _isSelected = false;
            
            _enemyAgent = GetComponent<EnemyAgent>();
            _agentVisual = GetComponent<AgentVisual>();
            
            _actionController.Setup();
            _enemyAgent.AddGoal(StateManager.Instance.HeroAttacked.Name, 1, false, 1);
            _enemyAgent.ModifyState(StateManager.Instance.FindHero.Name, 0);
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
            
        }

        public override void RestoreVital(string vital, int damage)
        {
            
        }

        public override void UseResource(string vital, int damage)
        {
            
        }

        protected override void Dead()
        {
            
        }

        public override void SyncData()
        {
            //onSyncHero.Invoke(this);
            //_worldPanel.SyncData();
        }
    }
}