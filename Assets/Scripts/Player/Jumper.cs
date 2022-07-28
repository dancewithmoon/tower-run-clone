using Constants;
using Infrastructure;
using ScriptableObjects;
using UnityEngine;
using UserInput;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Jumper : MonoBehaviour
    {
        private IInputService _input;
        private PlayerParameters _playerParameters;
        private Rigidbody _rigidbody;

        private bool _isGrounded;
    
        private void Awake()
        {
            _input = ServiceLocator.Single<IInputService>();
            _playerParameters = ServiceLocator.Single<PlayerParameters>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_input.IsJumpButtonPressed() && _isGrounded)
            {
                _isGrounded = false;
                _rigidbody.AddForce(Vector3.up * _playerParameters.JumpForce);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tags.Road))
            {
                _isGrounded = true;
            }
        }
    }
}
