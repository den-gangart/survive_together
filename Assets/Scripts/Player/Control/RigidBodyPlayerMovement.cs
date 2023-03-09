using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidBodyPlayerMovement : MonoBehaviour, IPlayerMovement
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 velocity)
        {
            _rigidbody.velocity = velocity * _speed;
        }
    }
}