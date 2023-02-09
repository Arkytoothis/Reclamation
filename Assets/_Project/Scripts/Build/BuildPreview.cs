using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reclamation.Build
{
    public class BuildPreview : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer = null;
        [SerializeField] private Color _green = Color.green;
        [SerializeField] private Color _red = Color.red;

        public void SetCanBuild(bool canBuild)
        {
            if (canBuild == true)
            {
                _meshRenderer.material.SetColor("_BaseColor", _green);
            }
            else
            {
                _meshRenderer.material.SetColor("_BaseColor", _red);
            }
        }
    }
}