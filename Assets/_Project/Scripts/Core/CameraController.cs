using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Reclamation.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _minFollowYOffset = 2f;
        [SerializeField] private float _maxFollowYOffset = 12f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _fastMoveSpeed = 10f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _zoomSpeed = 5f;
        [SerializeField] private CinemachineVirtualCamera _vCamera = null;

        private CinemachineTransposer _transposer = null;
        private Vector3 _targetFollowOffset = Vector3.zero;
        private bool _controlsActive = true;
        
        private void Awake()
        {
            _transposer = _vCamera.GetCinemachineComponent<CinemachineTransposer>();
            _targetFollowOffset = _transposer.m_FollowOffset;
        }

        private void Update()
        {
            if (_controlsActive == false) return;
            
            Move();
            Rotate();
            Zoom();
        }

        private void Move()
        {
            Vector2 inputMoveDirection = InputManager.Instance.GetCameraMoveVector();
            Vector3 moveVector = transform.forward * inputMoveDirection.y + transform.right * inputMoveDirection.x;

            if (InputManager.Instance.GetKey(KeyCode.LeftShift))
            {
                transform.position += moveVector * (_fastMoveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += moveVector * (_moveSpeed * Time.deltaTime);
            }
        }

        private void Rotate()
        {
            Vector3 rotationVector  = new Vector3(0,0,0);
            rotationVector.y = InputManager.Instance.GetCameraRotation();
            transform.eulerAngles += rotationVector * (_rotationSpeed * Time.deltaTime);
        }

        private void Zoom()
        {
            _targetFollowOffset.y += InputManager.Instance.GetCameraZoom() * 5f;
            _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, _minFollowYOffset, _maxFollowYOffset);
            _transposer.m_FollowOffset = Vector3.Lerp(_transposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * _zoomSpeed);
        }

        public void OnSetControlsActive(bool controlsActive)
        {
            _controlsActive = controlsActive;
        }
    }
}