using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Interactables;
using UnityEngine;

namespace Reclamation.Build
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance { get; private set; }
        
        [SerializeField] private List<StorageObject> _storageObjects = null;
        [SerializeField] private List<CraftingStation> _craftingStations = null;
        [SerializeField] private List<Blueprint> _blueprints = null;
        [SerializeField] private List<BuildingObject> _buildingObjects = null;
        [SerializeField] private Transform _blueprintsParent = null;
        [SerializeField] private Transform _objectsParent = null;
        [SerializeField] private GameObject _idleSpot = null;

        private BuildModes _buildMode = BuildModes.None;

        public BuildModes BuildMode => _buildMode;
        public GameObject IdleSpot => _idleSpot;
        public List<StorageObject> StorageObjects => _storageObjects;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple BuildingManagers " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        public void Setup()
        {
            
        }
        
        public void RegisterStorageObject(StorageObject storageObject)
        {
            if (!_storageObjects.Contains(storageObject))
            {
                _storageObjects.Add(storageObject);
            }
        }
        
        public void RegisterCraftingStation(CraftingStation craftingStation)
        {
            if (!_craftingStations.Contains(craftingStation))
            {
                _craftingStations.Add(craftingStation);
            }
        }
        
        public void RegisterBuildBlueprint(Blueprint blueprint)
        {
            if (!_blueprints.Contains(blueprint))
            {
                _blueprints.Add(blueprint);
                blueprint.transform.SetParent(_blueprintsParent);
            }
        }
        
        public void RegisterBuildingObject(BuildingObject buildingObject)
        {
            if (!_buildingObjects.Contains(buildingObject))
            {
                _buildingObjects.Add(buildingObject);
                buildingObject.transform.SetParent(_objectsParent);
            }
        }

        public StorageObject FindClosestStorageObject(Transform searcher, ItemCategory itemCategory)
        {
            List<StorageObject> storageObjects = new List<StorageObject>();
            for (int i = 0; i < _storageObjects.Count; i++)
            {
                if (_storageObjects[i].CanStoreCategory(itemCategory))
                {
                    storageObjects.Add(_storageObjects[i]);
                }
            }
            
            storageObjects.Sort(delegate(StorageObject a, StorageObject b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            return storageObjects[0];
        }

        public StorageObject FindClosestStorageObjectWithItemType(Transform searcher, ItemType itemType, int numberOfItems)
        {
            List<StorageObject> storageObjects = new List<StorageObject>();

            foreach (StorageObject storageObject in _storageObjects)
            {
                if (storageObject.HasItemType(itemType, numberOfItems))
                {
                    storageObjects.Add(storageObject);
                }
            }
            
            storageObjects.Sort(delegate(StorageObject a, StorageObject b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            return storageObjects[0];
        }

        public CraftingStation FindClosestCraftingStation(Transform searcher)
        {
            _craftingStations.Sort(delegate(CraftingStation a, CraftingStation b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            return _craftingStations[0];
        }

        public CraftingStation FindClosestCraftingStationWithRecipe(Transform searcher)
        {
            List<CraftingStation> craftingStations = new List<CraftingStation>();

            for (int i = 0; i < _craftingStations.Count; i++)
            {
                if (_craftingStations[i].RecipeOrders > 0 && StockpileManager.Instance.CanCraftRecipe(_craftingStations[i].CurrentRecipe))
                {
                    craftingStations.Add(_craftingStations[i]);
                }
            }
            
            craftingStations.Sort(delegate(CraftingStation a, CraftingStation b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            if(craftingStations.Count > 0)
                return craftingStations[0];
            else
            {
                return null;
            }
        }
        

        public Blueprint FindClosestBlueprint(Transform searcher)
        {
            if (_blueprints.Count == 0) return null;
            
            _blueprints.Sort(delegate(Blueprint a, Blueprint b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });
                
            return _blueprints[0];
        }

        public Blueprint FindClosestBlueprintThatNeedsResources(Transform searcher)
        {
            if (_blueprints.Count == 0)
            {
                return null;
            }

            List<Blueprint> blueprints = new List<Blueprint>();

            for (int i = 0; i < _blueprints.Count; i++)
            {
                if (_blueprints[i].HasIngredients() == false)
                {
                    blueprints.Add(_blueprints[i]);
                }
            }

            blueprints.Sort(delegate(Blueprint a, Blueprint b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });

            if (blueprints.Count > 0)
                return blueprints[0];
            else 
                return null;
        }

        public Blueprint FindClosestBlueprintReadyToBuild(Transform searcher)
        {
            if (_blueprints.Count == 0) return null;

            List<Blueprint> blueprints = new List<Blueprint>();

            for (int i = 0; i < _blueprints.Count; i++)
            {
                if (_blueprints[i].HasIngredients() == true)
                {
                    blueprints.Add(_blueprints[i]);
                }
            }

            blueprints.Sort(delegate(Blueprint a, Blueprint b)
            {
                return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
            });

            if (blueprints.Count > 0)
                return blueprints[0];
            else 
                return null;
        }

        public Blueprint FindFirstBlueprint()
        {
            if (_blueprints.Count == 0) return null;
                
            return _blueprints[0];
        }

        public void RemoveBlueprint(Blueprint blueprint)
        {
            if (_blueprints.Contains(blueprint))
            {
                _blueprints.Remove(blueprint);
                Destroy(blueprint.gameObject);
            }
        }

        public void SetBuildMode(BuildModes buildMode)
        {
            _buildMode = buildMode;
        }
    }
}