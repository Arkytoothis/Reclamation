using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Reclamation.AI
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Instance { get; private set; }

        public string ChopWoodActionName = "Chop Wood";
        public string GatherResourceActionName = "Gather Resource";
        public string StoreResourceActionName = "Store Resource";
        public string MineOreActionName = "Mine Ore";
        public string BuildBlueprintActionName = "Build Blueprint";
        public string PickupItemForBlueprintActionName = "Pickup Resource For Blueprint";
        public string PickupItemForCraftActionName = "Pickup Resource For Craft";
        public string DropoffItemForBlueprintActionName = "Dropoff Resource For Blueprint";
        public string DropoffItemForCraftActionName = "Dropoff Resource For Craft";
        public string UseToiletActionName = "Use Toilet";
        public string RestActionName = "Rest";
        public string CraftItemActionName = "Craft Item";
        public string FindRecipeActionName = "Find Recipe To Craft";
        public string FindBlueprintActionName = "Find Blueprint To Build";
        public string HarvestPlantsActionName = "Harvest Plants";
        public string AttackEnemyActionName = "Attack Enemy";
        public string FindEnemyToAttackActionName = "Find Enemy To Attack";
        
        public StateType NeedReliefState = null;
        public StateType Relieved = null;
        public StateType NeedRestState = null;
        public StateType Rested = null;
        public StateType NeedFoodState = null;
        public StateType Satiated = null;
        
        public StateType NeedWaitState = null;
        public StateType Idle = null;
        public StateType ChopWoodState = null;
        public StateType WoodChopped = null;
        public StateType MineOreState = null;
        public StateType OreMined = null;
        public StateType GatherResourceState = null;
        public StateType ResourceGathered = null;
        public StateType StoreResourceState = null;
        public StateType ResourceStored = null;
        public StateType HarvestPlantsState = null;
        public StateType PlantsHarvested = null;
        
        public StateType FindRecipeState = null;
        public StateType RecipeFound = null;
        public StateType PickupItemForCraftState = null;
        public StateType ItemPickedUpForCraft = null;
        public StateType DropoffItemForCraftState = null;
        public StateType ItemDroppedOffForCraft = null;
        public StateType CraftItemState = null;
        public StateType ItemCrafted = null;

        public StateType FindBlueprint = null;
        public StateType BlueprintFound;
        public StateType PickupItemForBlueprintState = null;
        public StateType ItemPickedUpForBlueprint = null;
        public StateType DropoffItemForBlueprintState = null;
        public StateType ItemDroppedOffForBlueprint = null;
        public StateType BuildBlueprintState = null;
        public StateType BlueprintBuilt = null;
        
        public StateType FreeBed = null;
        public StateType FreeToilet = null;
        public StateType FreeRecipe = null;

        public StateType FindHero = null;
        public StateType HeroFound = null;
        public StateType AttackHero = null;
        public StateType HeroAttacked = null;
        
        public StateType FindEnemy = null;
        public StateType EnemyFound = null;
        public StateType AttackEnemy = null;
        public StateType EnemyAttacked = null;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple StateManagers " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}