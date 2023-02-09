using System;
using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Reclamation.Resources
{
    public class FoodNodeSpawner : MonoBehaviour
    {
        [SerializeField] private float _distanceFromStart = 0f;
        [SerializeField] private float _jitter = 0.5f;
        [SerializeField] private float _size = 0f;
        [SerializeField] private float _minSize = 4f;
        [SerializeField] private float _maxSize = 10f;
        [SerializeField] private float _spawnMultiplier = 2f;
        [SerializeField] private float _spawnHeightOffset = 5f;
        [SerializeField] private int _maxWidth = 10;
        [SerializeField] private int _maxHeight = 10;
        [SerializeField] private int _specialOreSpawnChance = 75;

        private void Start()
        {
            SpawnResourceNodes();
        }

        private void SpawnResourceNodes()
        {
            int radiusX = _maxWidth / 2;
            int radiusY = _maxHeight / 2;
            
            _distanceFromStart = ResourcesManager.Instance.GetDistanceFromStart(transform.position);
            _size = Random.Range(_minSize, _maxSize);

            string mainKey = "Berry Bush";
            string specialKey = "Berry Bush";

            // if(Random.Range(0, 100) > 25)
            // {
            //     if (_distanceFromStart > Database.instance.ResourceNodes.GetResourceNode("Iron Node").MinDistance)
            //     {
            //         if (Random.Range(0, 100) > 25)
            //         {
            //             if (Random.Range(0, 100) < 25)
            //             {
            //                 specialKey = "Iron Node";
            //             }
            //             else
            //             {
            //                 specialKey = "Copper Node";
            //             }
            //         }
            //         else
            //         {
            //             specialKey = "Coal Node";
            //         }
            //     }
            //     else if (_distanceFromStart > Database.instance.ResourceNodes.GetResourceNode("Copper Node").MinDistance)
            //     {
            //         if(Random.Range(0, 100) > 25)
            //             specialKey = "Copper Node";
            //         else
            //             specialKey = "Coal Node";
            //     }
            //     else if (_distanceFromStart > Database.instance.ResourceNodes.GetResourceNode("Coal Node").MinDistance)
            //     {
            //         specialKey = "Coal Node";
            //     }
            // }
            
            ResourceNodeDefinition mainNode = Database.instance.ResourceNodes.GetResourceNode(mainKey);
            ResourceNodeDefinition specialNode = Database.instance.ResourceNodes.GetResourceNode(specialKey);
                
            for (int x = -radiusX; x < radiusX; x++)
            {
                for (int y = -radiusY; y < radiusY; y++)
                {
                    Vector3 spawnPosition = CalculateSpawnPosition(x, y);
                    float distanceFromCenter = Vector3.Distance(transform.position, spawnPosition);
                    if (distanceFromCenter > _size) continue;

                    TrySpawnNode(mainNode.NodePrefab, specialNode.NodePrefab, spawnPosition, distanceFromCenter, specialNode.SpecialNodeChance);
                }
            }
        }

        private Vector3 CalculateSpawnPosition(int x, int y)
        {
            float xJitter = Random.Range(-_jitter, _jitter);
            float yJitter = Random.Range(-_jitter, _jitter);
            Vector3 spawnPosition = new Vector3(transform.position.x + (x * _spawnMultiplier) + xJitter, transform.position.y, transform.position.z + (y * _spawnMultiplier) + yJitter);
            return spawnPosition;
        }

        private void TrySpawnNode(GameObject mainPrefab, GameObject specialPrefab, Vector3 spawnPosition, float distanceFromCenter, int specialChance)
        {
            int spawnChance = 100;
            if (distanceFromCenter >= _size - 1.75f) spawnChance = 33;
            if(Random.Range(0, 100) >= spawnChance) return;

            GameObject clone = null;
            
            if(Random.Range(0, 100) < specialChance)
            {
                clone = Instantiate(mainPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                clone = Instantiate(specialPrefab, spawnPosition, Quaternion.identity); 
            }
            
            clone.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
            clone.GetComponent<PlaceOnGround>().Place();
            clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y - _spawnHeightOffset, clone.transform.position.z);

            //float yOffset = (distanceFromCenter * 0.25f) + 0.5f;
            //float y = clone.transform.position.y - yOffset;
            //if (y < 0.5f) y = 0.5f;
        }

        private GameObject GetNodePrefab()
        {
            GameObject prefab;
            int randomOre = Random.Range(0, 100);
            
            if (randomOre >= 0 && randomOre < _specialOreSpawnChance)
            {
                prefab = Database.instance.ResourceNodes.GetResourceNode("Stone Node").NodePrefab;
            }
            else
            {
                if (_distanceFromStart > Database.instance.ResourceNodes.GetResourceNode("Copper Node").MinDistance)
                {
                    prefab = Database.instance.ResourceNodes.GetResourceNode("Copper Node").NodePrefab;
                }
                else
                {
                    prefab = Database.instance.ResourceNodes.GetResourceNode("Coal Node").NodePrefab;
                }
            }

            return prefab;
        }
    }
}