using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Attributes;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Gui;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Reclamation.Units
{
    public class Hero : Unit, IDamageSystem
    {
        [SerializeField] protected HeroData _heroData = null;
        [SerializeField] private GameObject _worldModel = null;
        [SerializeField] private GameObject _portraitModel = null;
        [SerializeField] private UnitResourceController _resourceController = null;
        [SerializeField] private ActionController _actionController = null;
        [SerializeField] private TaskController _taskController = null;
        [SerializeField] private HeroWorldPanel _worldPanel = null;
        
        [SerializeField] protected HeroEvent onSyncHero = null;

        private BodyRenderer _worldRenderer = null;
        private BodyRenderer _portraitRenderer = null;
        private PortraitMount _portrait = null;
        private PlayerAgent _playerAgent = null;
        private AgentVisual _agentVisual = null;
        
        public GameObject PortraitModel => _portraitModel;
        public BodyRenderer PortraitRenderer => _portraitRenderer;
        public PortraitMount Portrait => _portrait;
        public HeroData HeroData => _heroData;
        public GameObject WorldModel => _worldModel;
        public BodyRenderer WorldRenderer => _worldRenderer;
        public UnitResourceController ResourceController => _resourceController;
        public PlayerAgent PlayerAgent => _playerAgent;

        public void SetupHero(Genders gender, RaceDefinition race, ProfessionDefinition profession, int listIndex)
        {
            _isSelected = false;
            
            _modelParent.ClearTransform();
            _worldModel = Instantiate(race.PrefabMale, _modelParent);
            
            _unitAnimator = GetComponent<UnitAnimator>();
            _unitAnimator.Setup(_worldModel.GetComponent<Animator>());
            
            _worldRenderer = _worldModel.GetComponent<BodyRenderer>();
            _worldRenderer.SetupBody(_unitAnimator, gender, race, profession);

            _portraitModel = Instantiate(race.PrefabMale, null);
            _portraitRenderer = _portraitModel.GetComponent<BodyRenderer>();
            _portraitRenderer.SetupBody(_unitAnimator, _worldRenderer, race, profession);

            _heroData.Setup(gender, race, profession, _worldRenderer, listIndex);
            _attributes.Setup(race, profession);
            _skills.Setup(_attributes, race, profession);
            _inventory.Setup(_portraitRenderer, _worldRenderer, gender, race, profession);
            _attributes.CalculateAttributes();
            _unitEffects.Setup();
            
            _animationEvents = GetComponentInChildren<AnimationEvents>();
            _animationEvents.Setup(_inventory);
            
            _pathfinder.Setup();
            _playerAgent = GetComponent<PlayerAgent>();
            _agentVisual = GetComponent<AgentVisual>();
            profession.SetupActions(_actionController, _playerAgent);
            
            _worldPanel.Setup(this);

            var children = _portraitModel.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = LayerMask.NameToLayer("Portrait Light");
            }
        }

        // public void LoadHero(HeroSaveData saveData, RuntimeAnimatorController animatorController)
        // {
        //     RaceDefinition race = Database.instance.Races.GetRace(saveData.RaceKey);
        //     ProfessionDefinition profession = Database.instance.Profession.GetProfession(saveData.ProfessionKey);
        //     
        //     _isEnemy = false;
        //     _modelParent.ClearTransform();
        //     _worldModel = Instantiate(race.PrefabMale, _modelParent);
        //     _worldRenderer = _worldModel.GetComponent<BodyRenderer>();
        //     _worldRenderer.LoadBody(saveData);
        //
        //     _portraitModel = Instantiate(race.PrefabMale, null);
        //     _portraitRenderer = _portraitModel.GetComponent<BodyRenderer>();
        //     _portraitRenderer.LoadBody(saveData);
        //
        //     _unitAnimator = GetComponent<UnitAnimator>();
        //     _unitAnimator.Setup(_worldModel.GetComponent<Animator>(), animatorController);
        //
        //     _heroData.LoadData(saveData, _worldRenderer);
        //     _attributes.Setup(race, profession);
        //     _attributes.LoadData(saveData.AttributesSaveData);
        //     _skills.LoadData(saveData.SkillsSaveData);
        //     _inventory.LoadData(_worldRenderer, _portraitRenderer, saveData);
        //     _abilities.LoadData(saveData.AbilitySaveData);
        //     
        //     _actionController.SetupActions();
        //     _damageSystem.Setup(this);
        //     _unitEffects.Setup();
        //     _worldPanel.Setup(this);
        //
        //     //_unitAnimator.SetAnimatorOverride(_inventory.GetCurrentWeapon().GetWeaponData());
        //     
        //     var children = _portraitModel.GetComponentsInChildren<Transform>(includeInactive: true);
        //     foreach (var child in children)
        //     {
        //         child.gameObject.layer = LayerMask.NameToLayer("Portrait Light");
        //     }
        // }

        public override string GetFullName()
        {
            return _heroData.Name.FullName;
        }

        public override string GetShortName()
        {
            return _heroData.Name.ShortName;
        }

        public string GetFirstName()
        {
            return _heroData.Name.FirstName;
        }

        public override Item GetMeleeWeapon()
        {
            return _inventory.GetMeleeWeapon();
        }

        public override Item GetRangedWeapon()
        {
            return _inventory.GetRangedWeapon();
        }

        public override void Damage(Unit attacker, DamageTypeDefinition damageType, int damage, string vital)
        {
            if (_isAlive == false) return;

            _attributes.ModifyVital("Life", damage, false);
            //Debug.Log(_heroData.Name.ShortName + " takes " + damage + " " + vital + " damage");
            //CombatTextHandler.Instance.DisplayCombatText(new CombatText(_combatTextTransform.position, damage.ToString(), "default"));

            if (GetHealth() <= 0)
            {
                Dead();
            }

            if (_isSelected)
            {
                onSyncHero.Invoke(this);
            }
            
            _worldPanel.SyncData();
        }

        public override void RestoreVital(string vital, int amount)
        {
            if (_isAlive == false) return;

            if(_isSelected) onSyncHero.Invoke(this);
        }

        public override void UseResource(string vital, int amount)
        {
            if (_isAlive == false) return;

            if(_isSelected) onSyncHero.Invoke(this);
        }

        protected override void Dead()
        {
            _isAlive = false;
            //HeroManager_Combat.Instance.UnitDead(this);
            //Destroy(gameObject);
        }

        public void SetPortrait(PortraitMount portraitMount)
        {
            _portrait = portraitMount;
        }

        public void AddExperience(int experience)
        {
            _heroData.AddExperience(experience);
            if(_isSelected) onSyncHero.Invoke(this);
        }

        public override void SpendActionPoints(int actionPointCost)
        {
            _attributes.ModifyVital("Actions", actionPointCost, true);
            //_worldPanel.UpdateActionPoints();
            if(_isSelected) onSyncHero.Invoke(this);
        }

        public void SetWorldPanelActive(bool active)
        {
            //_worldPanel.gameObject.SetActive(active);
        }

        public override void Select()
        {
            //Debug.Log(_heroData.Name.ShortName + " selected");
            _selectionIndicator.SetActive(true);
            //MasterAudio.PlaySound(_heroData.RaceDefinition.GetSelectSound(_heroData.Gender));
            _isSelected = true;
        }

        public void MoveTo(Vector3 position)
        {
            if (_isSelected == false || _taskController.UnitState == UnitStates.Working) return;
            
            _pathfinder.MoveTo(position);
        }

        public void SetDestination(Transform destination)
        {
            _pathfinder.SetDestination(destination);    
        }
        
        public override void SyncData()
        {
            onSyncHero.Invoke(this);
            _worldPanel.SyncData();
        }

        public void Draft()
        {
            _taskController.SetState(UnitStates.Combat);
            _worldPanel.SyncData();
        }

        public void UnDraft()
        {
            _taskController.SetState(UnitStates.Working);
            _worldPanel.SyncData();
        }

        public void TakeDamage(Unit attacker, DamageTypeDefinition damageType, int amount, string vital)
        {
            Damage(attacker, damageType, amount, vital);
        }
    }
}