using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace NPC
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Friction : MonoBehaviour
    {
        [SerializeField] private float amount = 1.25f;
        [SerializeField] private float stopThreshold = 0.25f;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
        }

        private void FixedUpdate()
        {
            var velocity = _rigidbody.velocity;
            velocity += velocity * (-1f * amount * Time.deltaTime);
            if (Mathf.Abs(velocity.magnitude) <= stopThreshold)
                _rigidbody.velocity = Vector2.zero;
            else
                _rigidbody.velocity = velocity;
        }
    }
}