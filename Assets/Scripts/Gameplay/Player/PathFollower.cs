using Infrastructure;
using PathCreation;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PathFollower : MonoBehaviour
    {
        [SerializeField] private EndOfPathInstruction _endOfPathInstruction;
        
        private PlayerParameters _playerParameters;
        private PathCreator _pathCreator;
        private Rigidbody _rigidbody;
        private float _distanceTraveled;

        private void Awake()
        {
            _playerParameters = ServiceLocator.Single<PlayerParameters>();
            _pathCreator = ServiceLocator.Single<PathCreator>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.MovePosition(GetPointAtTraveledDistance());
        }

        private void Update()
        {
            IncreaseTraveledDistance();
            MoveByTraveledDistance();
        }
    
        private void IncreaseTraveledDistance()
        {
            _distanceTraveled += _playerParameters.Speed * Time.deltaTime;
        }

        private void MoveByTraveledDistance()
        {
            Vector3 nextPoint = GetPointAtTraveledDistanceWithDefaultY();
            _rigidbody.MovePosition(nextPoint);
            transform.LookAt(nextPoint);
        }
    
        private Vector3 GetPointAtTraveledDistance()
        {
            return _pathCreator.path.GetPointAtDistance(_distanceTraveled, _endOfPathInstruction);
        }

        private Vector3 GetPointAtTraveledDistanceWithDefaultY()
        {
            Vector3 nextPoint = GetPointAtTraveledDistance();
            nextPoint.y = transform.position.y;
            return nextPoint;
        }
    }
}