using UnityEngine;

namespace Gameplay.TowerLogic
{
    public class Human : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        [SerializeField] private Transform _fixationPoint;
        public Transform FixationPoint => _fixationPoint;
        [SerializeField] private Transform _footsPoint;
        public Transform FootsPoint => _footsPoint;
    }
}
