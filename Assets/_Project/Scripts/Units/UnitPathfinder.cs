using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

namespace Reclamation.Units
{
    public class UnitPathfinder : MonoBehaviour
    {
        [SerializeField] private Seeker _seeker = null;
        [SerializeField] private RichAI _richAi = null;
        [SerializeField] private RVOController _rvoController = null;
        [SerializeField] private AIDestinationSetter _destinationSetter = null;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _richAi = GetComponent<RichAI>();
            _rvoController = GetComponent<RVOController>();
            _destinationSetter = GetComponent<AIDestinationSetter>();
        }

        public void Setup()
        {
            
        }

        public void SetDestination(Transform target)
        {
            _destinationSetter.target = target;
        }

        public void ClearTarget()
        {
            _destinationSetter.target = null;
        }
        
        public void MoveTo(Transform target)
        {
            _richAi.canMove = true;
            _richAi.canSearch = true;
            //Debug.Log("Moving To: " + target.position);
            _seeker.StartPath(transform.position, target.position);
        }

        public void MoveTo(Vector3 position)
        {
            _richAi.canMove = true;
            _richAi.canSearch = true;
            //Debug.Log("Moving To: " + target.position);
            _seeker.StartPath(transform.position, position);
        }

        public void Stop()
        {
            //Debug.Log("Stopping");
            _seeker.CancelCurrentPathRequest();
            ClearTarget();
            _richAi.canMove = false;
            _richAi.canSearch = false;
            //_rvoController.locked = true;
        }

        public void Restart()
        {
            //Debug.Log("Restarting");
            _richAi.canMove = true;
            _richAi.canSearch = true;
            //_rvoController.locked = false;
        }

        public float GetRemainingDistance()
        {
            return _richAi.remainingDistance;
        }

        public float GetEndReadchedDistance()
        {
            return _richAi.endReachedDistance;
        }

        public bool GetPathPending()
        {
            return _richAi.pathPending;
        }

        public bool IsStopped()
        {
            return _richAi.isStopped;
        }

        public void SetEndReachedDistance(float endReachedDistance)
        {
            _richAi.endReachedDistance = endReachedDistance;
        }
    }
}