using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerParameters", menuName = "ScriptableObjects/PlayerParameters", order = 1)]
    public class PlayerParameters : ScriptableObject
    {
        [SerializeField] private float _speed;
        public float Speed => _speed;

        [SerializeField] private float _jumpForce;
        public float JumpForce => _jumpForce;
    }
}
