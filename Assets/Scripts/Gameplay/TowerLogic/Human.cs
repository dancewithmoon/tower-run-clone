using UnityEngine;

namespace Gameplay.TowerLogic
{
    public class Human : MonoBehaviour
    {
        [SerializeField] private Transform _fixationPoint;
        public Transform FixationPoint => _fixationPoint;
    }
}
