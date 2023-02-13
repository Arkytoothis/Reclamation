using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Equipment
{
    public class ItemEquipModel : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpawnPoint = null;

        public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
    }
}