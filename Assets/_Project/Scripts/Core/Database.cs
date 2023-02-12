using Reclamation.Abilities;
using Reclamation.Attributes;
using Reclamation.Equipment;
using Reclamation.Equipment.Enchantments;
using Reclamation.Resources;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Reclamation.Core
{
    [CreateAssetMenu(menuName = "Reclamation/Database")]
    public class Database : SingletonScriptableObject<Database>
    {
        [SerializeField] private AbilityDatabase _abilities = null;
        [SerializeField] private AttributeDatabase _attributes = null;
        [SerializeField] private SkillDatabase _skills = null;
        [SerializeField] private RaceDatabase _races = null;
        [SerializeField] private ProfessionDatabase _profession = null;
        [SerializeField] private DamageTypeDatabase _damageTypes = null;
        [SerializeField] private ItemDatabase _items = null;
        [SerializeField] private MaterialDatabase _materials = null;
        [SerializeField] private EnchantmentDatabase _enchants = null;
        [SerializeField] private RarityDatabase _rarities = null;
        [SerializeField] private ResourceNodeDatabase _resourceNodes = null;
        [SerializeField] private BuildingObjectDatabase _buildingObjects = null;
        [SerializeField] private RecipeDatabase _recipes = null;
        [SerializeField] private EnemyDatabase _enemies = null;
        [SerializeField] private AnimalDatabase _animalDatabase = null;
        
        [SerializeField] private Sprite _blankSprite = null;
        [SerializeField] private Sprite _draftedSprite = null;
        [SerializeField] private string _sceneLoadFilePath = "";

        private bool _initialized = false;

        public AbilityDatabase Abilities => _abilities;
        public AttributeDatabase Attributes => _attributes;
        public SkillDatabase Skills => _skills;
        public RaceDatabase Races => _races;
        public ProfessionDatabase Profession => _profession;
        public DamageTypeDatabase DamageTypes => _damageTypes;
        public ItemDatabase Items => _items;
        public MaterialDatabase Materials => _materials;
        public EnchantmentDatabase Enchants => _enchants;
        public RarityDatabase Rarities => _rarities;
        public ResourceNodeDatabase ResourceNodes => _resourceNodes;
        public BuildingObjectDatabase BuildingObjects => _buildingObjects;
        public RecipeDatabase Recipes => _recipes;
        public EnemyDatabase Enemies => _enemies;
        public AnimalDatabase AnimalDatabase => _animalDatabase;

        public Sprite BlankSprite => _blankSprite;
        public Sprite DraftedSprite => _draftedSprite;

        public string SceneLoadFilePath => _sceneLoadFilePath;

        public void Setup()
        {
            if (_initialized == true) return;

            _initialized = true;
            LoadPaths();
        }

        [Button("Load File Paths")]
        private void LoadPaths()
        {
            _sceneLoadFilePath = Application.streamingAssetsPath + "/SaveData/scene_load_data.dat";
        }
    }
}