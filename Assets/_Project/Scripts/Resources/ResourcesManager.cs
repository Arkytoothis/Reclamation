using System.Collections;
using System.Collections.Generic;
using Reclamation.Core;
using Reclamation.Equipment;
using Reclamation.Player;
using UnityEngine;

    namespace Reclamation.Resources
    {
        public class ResourcesManager : MonoBehaviour
        {
            public static ResourcesManager Instance { get; private set; }
            
            [SerializeField] private PlayerSpawner _playerSpawner = null;
            [SerializeField] private List<ResourceNode> _resourceNodes = null;
            [SerializeField] private List<ResourceNode> _treeNodes = null;
            [SerializeField] private List<ResourceNode> _bushNodes = null;
            [SerializeField] private List<ResourceNode> _oreNodes = null;
            [SerializeField] private List<ResourceNode> _gemNodes = null;
            [SerializeField] private List<ItemDrop> _itemDrops = null;
            [SerializeField] private Transform _treeNodesParent = null;
            [SerializeField] private Transform _bushNodesParent = null;
            [SerializeField] private Transform _oreNodesParent = null;
            [SerializeField] private Transform _gemNodesParent = null;

            private void Awake()
            {
                if (Instance != null)
                {
                    Debug.LogError("Multiple HeroManagers " + transform + " - " + Instance);
                    Destroy(gameObject);
                    return;
                }
            
                Instance = this;
            }

            public void Setup()
            {
            }
            
            public void RegisterResourceNode(ResourceNode node)
            {
                if (!_resourceNodes.Contains(node))
                {
                    _resourceNodes.Add(node);
                }
                
                if (node.nodeType == ResourceNodeTypes.Tree)
                {
                    if (!_treeNodes.Contains(node))
                    {
                        _treeNodes.Add(node);
                        node.transform.SetParent(_treeNodesParent);
                    }
                }
                else if (node.nodeType == ResourceNodeTypes.Bush)
                {
                    if (!_bushNodes.Contains(node))
                    {
                        _bushNodes.Add(node);
                        node.transform.SetParent(_bushNodesParent);
                    }
                }
                else if (node.nodeType == ResourceNodeTypes.Ore)
                {
                    if (!_oreNodes.Contains(node))
                    {
                        _oreNodes.Add(node);
                        node.transform.SetParent(_oreNodesParent);
                    }
                }
                else if (node.nodeType == ResourceNodeTypes.Gem)
                {
                    if (!_gemNodes.Contains(node))
                    {
                        _gemNodes.Add(node);
                        node.transform.SetParent(_gemNodesParent);
                    }
                }
            }
            
            public void RegisterItemDrop(ItemDrop drop)
            {
                if (!_itemDrops.Contains(drop))
                {
                    _itemDrops.Add(drop);
                }
            }

            public ResourceNode FindClosestResourceNode(Transform searcher)
            {
                _resourceNodes.Sort(delegate(ResourceNode a, ResourceNode b)
                {
                    return Vector3.Distance(searcher.position,a.transform.position)
                        .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
                });
                
                return _resourceNodes[0];
            }

            public ResourceNode FindClosestResourceNotTargeted(Transform searcher)
            {
                _resourceNodes.Sort(delegate(ResourceNode a, ResourceNode b)
                {
                    return Vector3.Distance(searcher.position,a.transform.position)
                    .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
                });

                int index = 0;

                for (int i = 0; i < _resourceNodes.Count; i++)
                {
                    if (_resourceNodes[i].IsTarget == false)
                    {
                        _resourceNodes[i].IsTarget = true;
                        index = i;
                        break;
                    }
                }
                
                return _resourceNodes[index];
            }
            
            public ResourceNode FindClosestBushNode(Transform searcher)
            {
                _bushNodes.Sort(delegate(ResourceNode a, ResourceNode b)
                {
                    return Vector3.Distance(searcher.position,a.transform.position)
                        .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
                });

                int index = -1;

                for (int i = 0; i < _bushNodes.Count; i++)
                {
                    if (_bushNodes[i].IsMarked == false) continue;
                    if (_bushNodes[i].IsTarget == false)
                    {
                        _bushNodes[i].IsTarget = true;
                        index = i;
                        break;
                    }
                }

                if (index != -1)
                {
                    return _bushNodes[index];
                }
                else
                {
                    return null;
                }
            }
            
            public ResourceNode FindClosestTreeNode(Transform searcher)
            {
                _treeNodes.Sort(delegate(ResourceNode a, ResourceNode b)
                {
                    return Vector3.Distance(searcher.position,a.transform.position)
                        .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
                });

                int index = -1;

                for (int i = 0; i < _treeNodes.Count; i++)
                {
                    if (_treeNodes[i].IsMarked == false) continue;
                    if (_treeNodes[i].IsTarget == false)
                    {
                        _treeNodes[i].IsTarget = true;
                        index = i;
                        break;
                    }
                }

                if (index != -1)
                {
                    return _treeNodes[index];
                }
                else
                {
                    return null;
                }
            }
            
            public ResourceNode FindClosestOreNode(Transform searcher)
            {
                _oreNodes.Sort(delegate(ResourceNode a, ResourceNode b)
                {
                    return Vector3.Distance(searcher.position,a.transform.position)
                        .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
                });

                int index = -1;

                for (int i = 0; i < _oreNodes.Count; i++)
                {
                    if (_oreNodes[i].IsMarked == false) continue;
                    if (_oreNodes[i].IsTarget == false)
                    {
                        _oreNodes[i].IsTarget = true;
                        index = i;
                        break;
                    }
                }

                if (index != -1)
                {
                    return _oreNodes[index];
                }
                else
                {
                    return null;
                }
            }

            public void RemoveResourceNode(ResourceNode node)
            {
                node.transform.SetParent(null);
                
                if (_resourceNodes.Contains(node))
                {
                    _resourceNodes.Remove(node);
                }
                
                if (node.nodeType == ResourceNodeTypes.Tree)
                {
                    if (_treeNodes.Contains(node))
                    {
                        _treeNodes.Remove(node);
                    }
                }
                else if (node.nodeType == ResourceNodeTypes.Bush)
                {
                    if (_bushNodes.Contains(node))
                    {
                        _bushNodes.Remove(node);
                    }
                }
                else if (node.nodeType == ResourceNodeTypes.Ore)
                {
                    if (_oreNodes.Contains(node))
                    {
                        _oreNodes.Remove(node);
                    }
                }
                else if (node.nodeType == ResourceNodeTypes.Gem)
                {
                    if (_gemNodes.Contains(node))
                    {
                        _gemNodes.Remove(node);
                    }
                }
                
                Destroy(node.gameObject);
            }

            public void RemoveItemDrop(ItemDrop drop)
            {
                _itemDrops.Remove(drop);
                Destroy(drop.gameObject);
            }
            
            public ItemDrop FindClosestItemDropNotTargeted(Transform searcher)
            {
                if (_itemDrops == null || _itemDrops.Count == 0)
                {
                    return null;
                }
                
                _itemDrops.Sort(delegate(ItemDrop a, ItemDrop b)
                {
                    return Vector3.Distance(searcher.position,a.transform.position)
                        .CompareTo( Vector3.Distance(searcher.position,b.transform.position) );
                });

                int index = -1;

                for (int i = 0; i < _itemDrops.Count; i++)
                {
                    if (_itemDrops[i].IsTarget == false)
                    {
                        _itemDrops[i].IsTarget = true;
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                    return null;
                else
                    return _itemDrops[index];
            }

            public float GetDistanceFromStart(Vector3 position)
            {
                return Vector3.Distance(_playerSpawner.transform.position, position);
            }
        }
    }