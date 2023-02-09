using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Reclamation.Equipment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Reclamation.Resources
{
    public enum ResourceNodeTypes
    {
        Tree, Bush, Ore, Gem, Number, None
    }
    
    public class ResourceNode : MonoBehaviour
    {
        [SerializeField] private ItemShort _itemShort = null;
        [SerializeField] private ResourceNodeDefinition _definition = null;
        [SerializeField] private ResourceNodeTypes _nodeType = ResourceNodeTypes.None;
        [SerializeField] private bool _isTarget = false;
        [SerializeField] private bool _isMarked = false;
        [SerializeField] private int _currentHealth = 100;
        [SerializeField] private Transform _dropSpawnPoint = null;
        [SerializeField] private LayerMask _layerMask;

        public int CurrentHealth => _currentHealth;
        public ResourceNodeTypes nodeType => _nodeType;

        public ResourceNodeDefinition Definition => _definition;

        public bool IsTarget
        {
            get => _isTarget;
            set => _isTarget = value;
        }

        public bool IsMarked
        {
            get => _isMarked;
            set => _isMarked = value;
        }

        private void Start()
        {
            ResourcesManager.Instance.RegisterResourceNode(this);

            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, _layerMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Tree Node"))
                {
                    Destroy(colliders[i].gameObject);
                    ResourcesManager.Instance.RemoveResourceNode(colliders[i].GetComponent<ResourceNode>());
                }
            }
        }

        public void Harvest(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            int numDrops = Random.Range(_definition.MinDrops, _definition.MaxDrops + 1);
            Vector3 spawnPosition = _dropSpawnPoint.position;
            
            for (int i = 0; i < numDrops; i++)
            {
                spawnPosition.y += 0.5f;
                GameObject clone = Instantiate(_definition.DropPrefab, spawnPosition, Quaternion.identity);
                ItemDrop itemDrop = clone.GetComponent<ItemDrop>();
                itemDrop.Setup(1);
            }
            
            ResourcesManager.Instance.RemoveResourceNode(this);
        }
    }
}
