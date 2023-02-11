using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Reclamation.Units
{
    public class UnitPathfinder : MonoBehaviour
    {
        [SerializeField] private Seeker _seeker = null;
        [SerializeField] private RichAI _richAi = null;
        [SerializeField] private AIDestinationSetter _destinationSetter = null;

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
            _seeker.CancelCurrentPathRequest();
            _richAi.canMove = false;
            _richAi.canSearch = false;
        }

        public void Restart()
        {
            _richAi.canMove = true;
            _richAi.canSearch = true;
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
    }
}