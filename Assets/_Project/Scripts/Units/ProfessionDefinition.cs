using System.Collections;
using System.Collections.Generic;
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
    }
}