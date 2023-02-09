﻿using System;
using System.Collections.Generic;
using Reclamation.Abilities;
using Reclamation.Attributes;
using Reclamation.Build;
using Reclamation.Craft;
using Reclamation.Equipment;
using Reclamation.Equipment.Enchantments;
using Reclamation.Gui;
using Reclamation.Resources;
using UnityEngine;
using Action = Reclamation.AI.Action;

namespace Reclamation.Core
{
    public abstract class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>,
        ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector] private List<TKey> keyData = new List<TKey>();

        [SerializeField, HideInInspector] private List<TValue> valueData = new List<TValue>();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < this.keyData.Count && i < this.valueData.Count; i++)
            {
                this[this.keyData[i]] = this.valueData[i];
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keyData.Clear();
            this.valueData.Clear();

            foreach (var item in this)
            {
                this.keyData.Add(item.Key);
                this.valueData.Add(item.Value);
            }
        }
    }

    [Serializable]
    public class EffectDictionary : SerializedDictionary<string, GameObject>
    {
    }
    
    [Serializable]
    public class AttributeDictionary : SerializedDictionary<string, Attributes.Attribute>
    {
    }
    
    [Serializable]
    public class ResistanceDictionary : SerializedDictionary<string, Resistance>
    {
    }
    
    // [Serializable]
    // public class AttributeWidgetDictionary : SerializedDictionary<string, AttributeWidget>
    // {
    // }
    //
    // [Serializable]
    // public class SkillWidgetDictionary : SerializedDictionary<string, SkillWidget>
    // {
    // }
    //
    // [Serializable]
    // public class VitalWidgetDictionary : SerializedDictionary<string, VitalWidget>
    // {
    // }
    //
    // [Serializable]
    // public class VitalBarDictionary : SerializedDictionary<string, VitalBar>
    // {
    // }
    
    [Serializable]
    public class SkillDictionary : SerializedDictionary<string, Skill>
    {
    }
    
    [Serializable]
    public class AttributeDefinitionDictionary : SerializedDictionary<string, AttributeDefinition>
    {
    }
    
    [Serializable]
    public class SkillDefinitionDictionary : SerializedDictionary<string, SkillDefinition>
    {
    }

    [Serializable]
    public class StartingCharacteristicDictionary : SerializedDictionary<string, StartingCharacteristic>
    {
    }

    [Serializable]
    public class StartingVitalDictionary : SerializedDictionary<string, StartingVital>
    {
    }
    
    [Serializable]
    public class StartingStatisticDictionary : SerializedDictionary<string, StartingStatistic>
    {
    }
    
    [Serializable]
    public class StartingSkillDictionary : SerializedDictionary<string, StartingSkill>
    {
    }
    
    [Serializable]
    public class ProfessionDictionary : SerializedDictionary<string, ProfessionDefinition>
    {
    }
    
    [Serializable]
    public class RaceDictionary : SerializedDictionary<string, RaceDefinition>
    {
    }
    
    [Serializable]
    public class ItemDefinitionDictionary : SerializedDictionary<string, Equipment.ItemDefinition>
    {
    }
    
    [Serializable]
    public class RecipeIngredientDictionary : SerializedDictionary<string, RecipeIngredient>
    {
    }
    
    [Serializable]
    public class BuildingIngredientDictionary : SerializedDictionary<string, BuildingIngredient>
    {
    }
    
    [Serializable]
    public class RecipesDictionary : SerializedDictionary<string, RecipeDefinition>
    {
    }
    
    [Serializable]
    public class BuildingObjectDefinitionDictionary : SerializedDictionary<string, BuildingObjectDefinition>
    {
    }
    
    [Serializable]
    public class MaterialsDictionary : SerializedDictionary<string, MaterialDefinition>
    {
    }
    
    [Serializable]
    public class EnchantsDictionary : SerializedDictionary<string, EnchantmentDefinition>
    {
    }
    
    [Serializable]
    public class DamageTypeDictionary : SerializedDictionary<string, DamageTypeDefinition>
    {
    }
    
    [Serializable]
    public class AbilityDefinitionDictionary : SerializedDictionary<string, AbilityDefinition>
    {
    }
    
    [Serializable]
    public class ResourceNodeDefinitionDictionary : SerializedDictionary<string, ResourceNodeDefinition>
    {
    }
    
    [Serializable]
    public class ResourceDefinitionDictionary : SerializedDictionary<string, ItemDefinition>
    {
    }
    
    [Serializable]
    public class ResourceWidgetDictionary : SerializedDictionary<string, ResourceWidget>
    {
    }
    
    [Serializable]
    public class ActionDictionary : SerializedDictionary<string, Action>
    {
    }
    
    [Serializable]
    public class ResourceNodeDictionary : SerializedDictionary<string, ResourceNode>
    {
    }
    
    [Serializable]
    public class ItemDictionary : SerializedDictionary<string, Item>
    {
    }
}