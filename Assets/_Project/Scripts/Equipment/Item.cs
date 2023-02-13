﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using Reclamation.Abilities;
using Reclamation.Core;
using Reclamation.Units;
using Reclamation.Equipment.Enchantments;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Reclamation.Equipment
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] private string _name = "";
        [SerializeField] private int _stackSize = 0;
        
        [SerializeField, FoldoutGroup("Item Data")] private string _description = "";
        [SerializeField, FoldoutGroup("Item Data")] private int _modelIndex = -1;

        [SerializeField, FoldoutGroup("Item Data")] private ItemType _itemType = ItemType.None;
        [SerializeField, FoldoutGroup("Item Data")] private ItemNameFormat _nameFormat = ItemNameFormat.None;
        [SerializeField, FoldoutGroup("Item Data")] private List<RenderSlot> _renderSlots = new List<RenderSlot>();
        [SerializeField, FoldoutGroup("Item Data")] private ItemMaterialAllowed _materialAllowed;

        [SerializeField, FoldoutGroup("Item Data")] private int _itemPower = 0;
        [SerializeField, FoldoutGroup("Item Data")] private int _encumbrance = 0;
        [SerializeField, FoldoutGroup("Item Data")] private int _goldValue = 0;
        [SerializeField, FoldoutGroup("Item Data")] private int _gemValue = 0;
        [SerializeField, FoldoutGroup("Item Data")] private int _usesLeft = 0;

        [SerializeField, FoldoutGroup("Item Attributes")] private string _definitionKey = "";
        [SerializeField, FoldoutGroup("Item Attributes")] private string _rarityKey = "";
        [SerializeField, FoldoutGroup("Item Attributes")] private string _materialKey = "";
        [SerializeField, FoldoutGroup("Item Attributes")] private string _qualityKey = "";
        [SerializeField, FoldoutGroup("Item Attributes")] private string _prefixEnchantKey = "";
        [SerializeField, FoldoutGroup("Item Attributes")] private string _suffixEnchantKey = "";

        public string Name => _name;
        public string Key => _definitionKey;
        public string Description => _description;
        public ItemType ItemType => _itemType;
        public ItemNameFormat NameFormat => _nameFormat;
        public List<RenderSlot> RenderSlots => _renderSlots;
        public ItemMaterialAllowed MaterialAllowed => _materialAllowed;
        public int ModelIndex => _modelIndex;

        public int StackSize
        {
            get => _stackSize;
            set => _stackSize = value;
        }

        public int ItemPower => _itemPower;
        public int Encumbrance => _encumbrance;
        public int GoldValue => _goldValue;
        public int GemValue => _gemValue;
        public int UsesLeft => _usesLeft;
        public int MaxUses => ItemDefinition.UsableData.MaxUses;

        public string RarityKey => _rarityKey;

        public Sprite Icon
        {
            get {
                if (_definitionKey != "")
                {
                    return Database.instance.Items.GetItem(_definitionKey).Icon;
                }
                else
                {
                    return Database.instance.BlankSprite;
                }
            }
        }

        public string MaterialKey => _materialKey;
        public string QualityKey => _qualityKey;
        public string PrefixEnchantKey => _prefixEnchantKey;
        public string SuffixEnchantKey => _suffixEnchantKey;

        public ItemDefinition ItemDefinition => Database.instance.Items.GetItem(_definitionKey);
        public MaterialDefinition MaterialDefinition => Database.instance.Materials.GetMaterial(_materialKey);
        public EnchantmentDefinition PrefixDefinition => Database.instance.Enchants.GetEnchant(_prefixEnchantKey);
        public EnchantmentDefinition SuffixDefinition => Database.instance.Enchants.GetEnchant(_suffixEnchantKey);

        public Item()
        {
            _definitionKey = "";
            _name = "";
            _itemType = ItemType.None;
            _nameFormat = ItemNameFormat.None;
            _renderSlots = new List<RenderSlot>();
            _materialAllowed = ItemMaterialAllowed.None;
            _modelIndex = -1;
            _stackSize = 0;
            _itemPower = 0;
            _encumbrance = 0;
            _rarityKey = "";
            _materialKey = "";
            _qualityKey = "";
            _prefixEnchantKey = "";
            _suffixEnchantKey = "";
        }

        public Item(ItemDefinition definition)
        {
            _definitionKey = definition.Key;
            _name = definition.Name;
            _itemType = definition.ItemType;
            _nameFormat = definition.NameFormat;
            _renderSlots = definition.RenderSlots;
            _materialAllowed = definition.PrimaryMaterialAllowed;
            _modelIndex = definition.ModelIndex;
            _itemPower = definition.BasePower;
            _encumbrance = definition.Encumbrance;
            _usesLeft = definition.UsableData.MaxUses;
            _stackSize = 1;
            _rarityKey = "";
            _materialKey = "";
            _qualityKey = "";
            _prefixEnchantKey = "";
            _suffixEnchantKey = "";
        }

        public Item(Item item)
        {
            if (item == null) return;

            _definitionKey = item.Key;
            _name = item.Name;
            _itemType = item.ItemType;
            _nameFormat = item.NameFormat;
            _renderSlots = item.RenderSlots;
            _materialAllowed = item.MaterialAllowed;
            _modelIndex = item.ModelIndex;

            _stackSize = item._stackSize;
            _itemPower = item._itemPower;
            _encumbrance = item._encumbrance;
            _goldValue = item._goldValue;
            _gemValue = item._gemValue;
            
            _rarityKey = item._rarityKey;
            _materialKey = item.MaterialKey;
            _qualityKey = item.QualityKey;
            _prefixEnchantKey = item.PrefixEnchantKey;
            _suffixEnchantKey = item.SuffixEnchantKey;
            _usesLeft = item.UsesLeft;
        }

        public bool IsEmpty()
        {
            if (_definitionKey == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void AddToStack(int amountToAdd)
        {
            _stackSize += amountToAdd;
        }
        
        public void SetMaterial(MaterialDefinition material)
        {
            if (material == null)
            {
                _materialKey = "";
            }
            else
            {
                _materialKey = material.Key;
                CalculatePower();
                CalculateEncumbrance();
                CalculateRarity();
            }
        }

        public string DisplayName()
        {
            string quality = "";
            string prefix = "";
            string suffix = "";

            if (_qualityKey != "")
            {
                quality = _qualityKey;
            }
            
            if (_prefixEnchantKey != "")
            {
                prefix = " " + _prefixEnchantKey;
            }
            
            if (_suffixEnchantKey != "")
            {
                suffix = " " + _suffixEnchantKey;
            }

            string name = "";

            if (NameFormat == ItemNameFormat.Material_First)
            {
                name = quality + prefix + " " + _materialKey + " " + _name + suffix;
            }
            else if (_nameFormat == ItemNameFormat.Material_Middle)
            {
                name = quality + prefix + " " + _materialKey + " " + _name + suffix;
            }
            else if (_nameFormat == ItemNameFormat.Material_Last)
            {
                name = quality + prefix + _name + " " + _materialKey + suffix;
            }

            return name;
        }

        public void CalculateRarity()
        {
            foreach (RarityDefinition rarity in Database.instance.Rarities.Rarities)
            {
                if (_itemPower >= rarity.MinimumPower && _itemPower <= rarity.MaximumPower)
                {
                    _rarityKey = rarity.Key;
                    break;
                }
            }
            
            if(_rarityKey == "")
                _rarityKey = "Common";
        }

        public Color GetRarityColor()
        {
            if (_rarityKey != "")
            {
                return Database.instance.Rarities.GetRarity(_rarityKey).Color;
            }
            else
            {
                return Color.gray;
            }
        }
        
        public string GetTooltipText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Power: ");
            sb.Append(_itemPower + " " + _rarityKey);
            sb.Append("\n");

            sb.Append("Encumbrance: ");
            sb.Append(_encumbrance);
            sb.Append("\n");

            if (ItemDefinition.MinimumMight > 0)
            {
                sb.Append("Might: ");
                sb.Append(ItemDefinition.MinimumMight);
                sb.Append("\n");
            }

            if (ItemDefinition.MinimumFinesse > 0)
            {
                sb.Append("Finesse: ");
                sb.Append(ItemDefinition.MinimumFinesse);
                sb.Append("\n");
            }

            if (ItemDefinition.MinimumMind > 0)
            {
                sb.Append("Mind: ");
                sb.Append(ItemDefinition.MinimumMind);
                sb.Append("\n");
            }

            if (ItemDefinition.Hands != Hands.None)
            {
                sb.Append("Hands: ");
                sb.Append(ItemDefinition.Hands);
                sb.Append("\n");
            }

            if (ItemDefinition.WeaponData.HasData == true)
            {
                sb.Append("\n" + ItemDefinition.WeaponData.GetTooltipText());
            }

            if (ItemDefinition.WearableData.HasData == true)
            {
                sb.Append("\n" + ItemDefinition.WearableData.GetTooltipText());
            }

            if (ItemDefinition.AccessoryData.HasData == true)
            {
                sb.Append("\n" + ItemDefinition.AccessoryData.GetTooltipText());
            }

            if (ItemDefinition.UsableData.HasData == true)
            {
                sb.Append("\n" + ItemDefinition.UsableData.GetTooltipText());
            }

            sb.Append("\nMaterial:  " + MaterialDefinition.GetTooltipText() + "\n");

             if (_qualityKey != "")
             {
                 sb.Append("Quality:  " + Database.instance.Enchants.GetEnchant(_qualityKey).Name + "\n");
             }
            
             if (_prefixEnchantKey != "")
             {
                 sb.Append("Prefix:  " + Database.instance.Enchants.GetEnchant(_prefixEnchantKey).Name + "\n");
             }
            
             if (_suffixEnchantKey != "")
             {
                 sb.Append("Suffix:  " + Database.instance.Enchants.GetEnchant(_suffixEnchantKey).Name + "\n");
             }

            return sb.ToString();
        }
        
        public string GetItemWidgetText()
        {
            StringBuilder sb = new StringBuilder();

            if (_definitionKey == "") return "";
            
            if (ItemDefinition.WeaponData.HasData == true)
            {
                sb.Append(ItemDefinition.WeaponData.GetItemWidgetText());
            }

            if (ItemDefinition.WearableData.HasData == true)
            {
                sb.Append("\n" + ItemDefinition.WearableData.GetItemWidgetText());
            }

            if (ItemDefinition.AccessoryData.HasData == true)
            {
                sb.Append("\n" + ItemDefinition.AccessoryData.GetItemWidgetText());
            }

            if (ItemDefinition.UsableData.HasData == true)
            {
                sb.Append("\n" + ItemDefinition.UsableData.GetItemWidgetText());
            }

            return sb.ToString();
        }
 
        public void CalculatePower()
        {
            _itemPower = ItemDefinition.BasePower;
            _itemPower += MaterialDefinition.ItemPower;
            
            if (_qualityKey != "")
            {
                //Debug.Log("Quality " + _qualityKey);
                _itemPower += Database.instance.Enchants.GetEnchant(_qualityKey).ItemPower;
            }
            
            if (_prefixEnchantKey != "")
            {
                //Debug.Log("Prefix " + _prefixEnchantKey);
                _itemPower += Database.instance.Enchants.GetEnchant(_prefixEnchantKey).ItemPower;
            }
            
            if (_suffixEnchantKey != "")
            {
                //Debug.Log("Suffix " + _suffixEnchantKey);
                _itemPower += Database.instance.Enchants.GetEnchant(_suffixEnchantKey).ItemPower;
            }
        }

        public void CalculateEncumbrance()
        {
            _encumbrance = ItemDefinition.Encumbrance;
            int encumbranceBonus = (int) ((float) _encumbrance * MaterialDefinition.EncumbranceModifier);
            _encumbrance += encumbranceBonus;

            if (_qualityKey != "")
            {
                int qualityBonus = (int) ((float) _encumbrance * Database.instance.Enchants.GetEnchant(_qualityKey).EncumbranceModifier);
                _encumbrance += qualityBonus;
            }
            
            if (_prefixEnchantKey != "")
            {
                int prefixBonus = (int) ((float) _encumbrance * Database.instance.Enchants.GetEnchant(_prefixEnchantKey).EncumbranceModifier);
                _encumbrance += prefixBonus;
            }
            
            if (_suffixEnchantKey != "")
            {
                int suffixBonus = (int) ((float) _encumbrance * Database.instance.Enchants.GetEnchant(_suffixEnchantKey).EncumbranceModifier);
                _encumbrance += suffixBonus;
            }
        }

        public void SetQualityEnchant(EnchantmentDefinition enchant)
        {
            if (enchant != null)
            {
                _qualityKey = enchant.Key;
                CalculatePower();
                CalculateEncumbrance();
                CalculateRarity();
            }
            else
            {
                _qualityKey = "";
            }
        }

        public void SetPrefixEnchant(Enchantment enchant)
        {
            if (enchant != null)
            {
                _prefixEnchantKey = enchant.Definition.Key;
                CalculatePower();
                CalculateEncumbrance();
                CalculateRarity();
            }
        }
        
        public void SetPrefixEnchant(EnchantmentDefinition definition)
        {
            if (definition != null)
            {
                _prefixEnchantKey = definition.Key;
                CalculatePower();
                CalculateEncumbrance();
                CalculateRarity();
            }
        }
        
        public void SetSuffixEnchant(EnchantmentDefinition definition)
        {
            if (definition != null)
            {
                _suffixEnchantKey = definition.Key;
                CalculatePower();
                CalculateEncumbrance();
                CalculateRarity();
            }
        }

        public void SetSuffixEnchant(Enchantment enchant)
        {
            if (enchant != null)
            {
                _suffixEnchantKey = enchant.Definition.Key;
                CalculatePower();
                CalculateEncumbrance();
                CalculateRarity();
            }
        }

        // public bool Use(GameEntity user)
        // {
        //     bool success = false;
        //     //Debug.Log(pc.Details.Name.ShortName + " uses " + DisplayName());
        //     ItemDefinition definition = Database.instance.Items.GetItem(_definitionKey);
        //
        //     if (definition.UsableData.HasData == true)
        //     {
        //         _usesLeft--;
        //         definition.UsableData.Use(user, new List<GameEntity> { user });
        //     }
        //
        //     return success;
        // }

        //public bool Use(Enemy enemy)
        //{
        //    bool success = false;

        //    Debug.Log(enemy.Details.Name + " uses " + DisplayName());

        //    return success;
        //}

        public int GetBonus()
        {
            int bonus = 0;


            return bonus;
        }

        public WeaponData GetWeaponData()
        {
            if (_definitionKey == "") return null;
            
            if (ItemDefinition.WeaponData != null && ItemDefinition.WeaponData.HasData == true)
            {
                return ItemDefinition.WeaponData;
            }
            else
            {
                return null;
            }
        }

        public WearableData GetWearableData()
        {
            if (_definitionKey == "") return null;
            
            if (ItemDefinition.WearableData != null && ItemDefinition.WearableData.HasData == true)
            {
                return ItemDefinition.WearableData;
            }
            else
            {
                return null;
            }
        }

        public UsableData GetUsableData()
        {
            if (_definitionKey == "") return null;
            
            if (ItemDefinition.UsableData != null && ItemDefinition.UsableData.HasData == true)
            {
                return ItemDefinition.UsableData;
            }
            else
            {
                return null;
            }
        }

        public void CalculateValue()
        {
            ItemDefinition definition = Database.instance.Items.GetItem(_definitionKey);
            _goldValue = definition.GoldValue;
            _gemValue = definition.GemValue;
            
            if (_materialKey != "")
            {
                _goldValue += Database.instance.Materials.GetMaterial(_materialKey).GoldValue;
                _gemValue += Database.instance.Materials.GetMaterial(_materialKey).GemValue;
            }

            if (_qualityKey != "")
            {
                _goldValue += Database.instance.Enchants.GetEnchant(_qualityKey).GoldValue;
                _gemValue += Database.instance.Enchants.GetEnchant(_qualityKey).GemValue;
            }
            
            if (_prefixEnchantKey != "")
            {
                _goldValue += Database.instance.Enchants.GetEnchant(_prefixEnchantKey).GoldValue;
                _gemValue += Database.instance.Enchants.GetEnchant(_prefixEnchantKey).GemValue;
            }
            
            if (_suffixEnchantKey != "")
            {
                _goldValue += Database.instance.Enchants.GetEnchant(_suffixEnchantKey).GoldValue;
                _gemValue += Database.instance.Enchants.GetEnchant(_suffixEnchantKey).GemValue;
            }
        }

        public GameObject SpawnItemModel(Transform parent, int layer)
        {
            ItemDefinition def = ItemDefinition;
            if (def.EquipModel != null)
            {
                GameObject clone = GameObject.Instantiate(def.EquipModel, parent);
                clone.layer = layer;
                
                for (int i = 0; i < clone.transform.childCount; i++)
                {
                    clone.transform.GetChild(i).gameObject.layer = layer;
                }
                
                ResetScaleAndParent(clone.transform, parent);
                
                return clone;
            }
            else
                return null;
        }
        
        public void ResetScaleAndParent(Transform t, Transform parent)
        {
            t.SetParent(null);
            t.localScale = Vector3.one;
            t.SetParent(parent);
        }

        public void Use(Unit user, List<Unit> targets)
        {
            UsableData usableData = GetUsableData();

            if (usableData != null && _usesLeft <= 0)
            {
                usableData.Use(user, targets);
                Use(1);
            }

            WeaponData weaponData = GetWeaponData();
            
            if (weaponData != null)
            {
                PerformAttack(user, targets);
            }
        }

        public void Use(int uses)
        {
            _usesLeft -= uses;
        }

        public bool IsEqual(Item item)
        {
            if (_definitionKey == item._definitionKey && _materialKey == item._materialKey && _prefixEnchantKey == item._prefixEnchantKey && _suffixEnchantKey == item._suffixEnchantKey)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PerformAttack(Unit owner, List<Unit> targets)
        {
            WeaponData weaponData = GetWeaponData();
            
            if (weaponData.Projectile == null)
            {
                foreach (DamageEffect attack in weaponData.DamageEffects)
                {
                    foreach (Unit target in targets)
                    {
                        int damage = Random.Range(attack.MinimumValue, attack.MaximumValue + 1);
                        target.Damage(owner, attack.DamageType, damage, "Life");
                    }
                }
            }
            else
            {
                owner.SpawnProjectile(targets, weaponData);
            }
        }
    }
}