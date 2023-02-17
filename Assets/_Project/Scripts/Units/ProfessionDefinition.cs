using System.Collections;
using System.Collections.Generic;
using Reclamation.AI;
using Reclamation.Core;
using Reclamation.Equipment;
using UnityEngine;

namespace Reclamation.Attributes
{
    public enum ProfessionArchetypes { Fighter, Rogue, Caster, Citizen, Number, None }
    
    [CreateAssetMenu(fileName = "Profession Definition", menuName = "Descending/Definition/Profession Definition")]
    public class ProfessionDefinition : ScriptableObject
	{
        [SerializeField] private bool _unlocked = false;
        [SerializeField] private ProfessionArchetypes _archetype = ProfessionArchetypes.None;
        [SerializeField] private string _key = "";
        [SerializeField] private string _nameMale = "";
        [SerializeField] private string _nameFemale = "";
        [SerializeField] private Sprite _icon = null;
        [SerializeField] private JobTypes _defaultJob = JobTypes.Laborer;
        [SerializeField] private StartingCharacteristicDictionary _startingCharacteristics = null;
        [SerializeField] private StartingVitalDictionary _startingVitals = null;
        [SerializeField] private StartingStatisticDictionary _startingStatistic = null;
        [SerializeField] private StartingSkillDictionary _startingSkills = null;
        [SerializeField] private List<ItemShort> _startingItems = null;

        [SerializeField] private int _attributePointsPerLevel = 0;
        [SerializeField] private int _skillPointsPerLevel = 0;
        [SerializeField] private float _mightModifier = 1f;
        [SerializeField] private float _finesseModifier = 1f;
        [SerializeField] private float _spellModifier = 1f;
        [SerializeField] private bool _prefersRanged = false;

        public bool Unlocked => _unlocked;
        public ProfessionArchetypes Archetype => _archetype;
        public string Key => _key;
        public string NameMale => _nameMale;
        public string NameFemale => _nameFemale;
        public Sprite Icon => _icon;
        public JobTypes DefaultJob => _defaultJob;
        public StartingCharacteristicDictionary StartingCharacteristics => _startingCharacteristics;
        public StartingVitalDictionary StartingVitals => _startingVitals;
        public StartingStatisticDictionary StartingStatistic => _startingStatistic;
        public StartingSkillDictionary StartingSkills => _startingSkills;
        public List<ItemShort> StartingItems => _startingItems;
        public int AttributePointsPerLevel => _attributePointsPerLevel;
        public int SkillPointsPerLevel => _skillPointsPerLevel;
        public float MightModifier => _mightModifier;
        public float FinesseModifier => _finesseModifier;
        public float SpellModifier => _spellModifier;

        public bool PrefersRanged => _prefersRanged;

        public string GetGenderName(Genders gender)
        {
            string s = "";

            if (gender == Genders.Male)
            {
                s = _nameMale;
            }
            else if (gender == Genders.Female)
            {
                s = _nameFemale;
            }

            return s;
        }

        [SerializeField] private List<ProfessionActionData> _startingStates = null;
        [SerializeField] private List<ProfessionActionData> _startingGoals = null;
        [SerializeField] private List<ProfessionActionData> _startingBeliefs = null;

        public void SetupActions(ActionController controller, PlayerAgent agent)
        {
            controller.Setup();
            controller.SetActionEnabled(StateManager.Instance.ChopWoodActionName, false);
            controller.SetActionEnabled(StateManager.Instance.MineOreActionName, false);
            controller.SetActionEnabled(StateManager.Instance.GatherResourceActionName, false);
            controller.SetActionEnabled(StateManager.Instance.StoreResourceActionName, false);

            controller.SetActionEnabled(StateManager.Instance.FindRecipeActionName, false);
            controller.SetActionEnabled(StateManager.Instance.PickupItemForCraftActionName, false);
            controller.SetActionEnabled(StateManager.Instance.DropoffItemForCraftActionName, false);
            controller.SetActionEnabled(StateManager.Instance.CraftItemActionName, false);

            controller.SetActionEnabled(StateManager.Instance.FindBlueprintActionName, false);
            controller.SetActionEnabled(StateManager.Instance.PickupItemForBlueprintActionName, false);
            controller.SetActionEnabled(StateManager.Instance.DropoffItemForBlueprintActionName, false);
            controller.SetActionEnabled(StateManager.Instance.BuildBlueprintActionName, false);

            controller.SetActionEnabled(StateManager.Instance.FindEnemyToAttackActionName, false);
            controller.SetActionEnabled(StateManager.Instance.AttackEnemyActionName, false);

            controller.SetActionEnabled(StateManager.Instance.FindAnimalToAttackActionName, false);
            controller.SetActionEnabled(StateManager.Instance.AttackAnimalActionName, false);

            agent.AddGoal(StateManager.Instance.DoneIdle.Name, 0, false, 10);

            foreach (ProfessionActionData state in _startingStates)
            {
                controller.SetActionEnabled(state.StateType.Name, true);
            }
            
            foreach (ProfessionActionData goal in _startingGoals)
            {
                agent.AddGoal(goal.StateType.Name, 0, goal.Remove, goal.Priority);
            }
            
            foreach (ProfessionActionData belief in _startingBeliefs)
            {
                agent.ModifyState(belief.StateType.Name, 0);
            }
            
            // if (_defaultJob == JobTypes.Soldier)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.FindEnemyToAttackActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.AttackEnemyActionName, true);
            //     agent.AddGoal(StateManager.Instance.EnemyAttacked.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.FindEnemy.Name, 0);
            // }
            // else if (_defaultJob == JobTypes.Hunter)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.FindAnimalToAttackActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.AttackAnimalActionName, true);
            //     agent.AddGoal(StateManager.Instance.AnimalAttacked.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.FindAnimal.Name, 0);
            // }
            // else if (_defaultJob == JobTypes.Forester)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.ChopWoodActionName, true);
            //     agent.AddGoal(StateManager.Instance.WoodChopped.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.ChopWoodState.Name, 0);
            // }
            // else if (_defaultJob == JobTypes.Forager)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.HarvestPlantsActionName, true);
            //     agent.AddGoal(StateManager.Instance.PlantsHarvested.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.HarvestPlantsState.Name, 0);
            // }
            // else if (_defaultJob == JobTypes.Laborer)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.GatherResourceActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.StoreResourceActionName, true);
            //     agent.AddGoal(StateManager.Instance.ResourceStored.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.GatherResourceState.Name, 0);
            // }
            // else if (_defaultJob == JobTypes.Miner)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.MineOreActionName, true);
            //     agent.AddGoal(StateManager.Instance.OreMined.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.MineOreState.Name, 0);
            // }
            // else if (_defaultJob == JobTypes.Builder)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.FindBlueprintActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.PickupItemForBlueprintActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.DropoffItemForBlueprintActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.BuildBlueprintActionName, true);
            //     agent.AddGoal(StateManager.Instance.BlueprintBuilt.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.FindBlueprint.Name, 0);
            // }
            // else if (_defaultJob == JobTypes.Crafter)
            // {
            //     controller.SetActionEnabled(StateManager.Instance.FindRecipeActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.PickupItemForCraftActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.DropoffItemForCraftActionName, true);
            //     controller.SetActionEnabled(StateManager.Instance.CraftItemActionName, true);
            //     agent.AddGoal(StateManager.Instance.ItemCrafted.Name, 1, false, 1);
            //     agent.ModifyState(StateManager.Instance.FindRecipeState.Name, 0);
            // }
        }
    }
}