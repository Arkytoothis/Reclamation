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
    public class Enemy : Unit, IDamageSystem
    {
        [SerializeField] private EnemyDefinition _enemyDefinition = null;
        [SerializeField] private ActionController _actionController = null;
        [SerializeField] private UnitResourceController _resourceController = null;
        [SerializeField] private EnemyAgent _enemyAgent = null;
        [SerializeField] private AgentVisual _agentVisual = null;
        [SerializeField] private EnemyWorldPanel _worldPanel = null;
        [SerializeField] private EnemySpawner _spawner = null;
        
        public EnemyDefinition EnemyDefinition => _enemyDefinition;
        public EnemyAgent EnemyAgent => _enemyAgent;
        public AgentVisual AgentVisual => _agentVisual;
        public UnitResourceController ResourceController => _resourceController;

        public void SetupEnemy(EnemySpawner spawner, int listIndex)
        {
            _spawner = spawner;
            _attributes.Setup(_enemyDefinition);
            // _skills.Setup(_attributes, race, profession);
            // _inventory.Setup(_portraitRenderer, _worldRenderer, gender, race, profession);
            _isSelected = false;
            
            _enemyAgent = GetComponent<EnemyAgent>();
            _agentVisual = GetComponent<AgentVisual>();
            _resourceController = GetComponent<UnitResourceController>();
            
            _actionController.Setup();
            _enemyAgent.AddGoal(StateManager.Instance.HeroAttacked.Name, 1, false, 1);
            _enemyAgent.ModifyState(StateManager.Instance.FindHero.Name, 0);
            
            _worldPanel.Setup(this);
            
            EnemyManager.Instance.RegisterEnemy(this);
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

        public override void Damage(Unit attacker, DamageTypeDefinition damageType, int damage, string vital)
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
            EnemyManager.Instance.RemoveEnemy(this);
            _spawner.EnemyDied(this);
            
            Vector3 spawnPosition = _dropSpawnPoint.position;
            
            foreach (LootDropEntry lootDrop in _enemyDefinition.LootDrops)
            {
                int numberItemsToSpawn = Random.Range(lootDrop.MinNumberItems, lootDrop.MaxNumberItems + 1);

                for (int i = 0; i < numberItemsToSpawn; i++)
                {
                    GameObject clone = Instantiate(lootDrop.ItemShort.Item.ItemDrop.gameObject, spawnPosition, Quaternion.identity);
                    ItemDrop itemDrop = clone.GetComponent<ItemDrop>();
                    itemDrop.Setup(1);
                }
            }

            Destroy(gameObject);
        }

        public override void SyncData()
        {
            //onSyncHero.Invoke(this);
            _worldPanel.SyncData();
        }

        public void TakeDamage(Unit attacker, DamageTypeDefinition damageType, int amount, string vital)
        {
            Damage(attacker, damageType, amount, vital);
        }

        public void SetDestination(Transform destination)
        {
            _pathfinder.SetDestination(destination);    
        }
    }
}