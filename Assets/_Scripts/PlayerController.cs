using System;
using Ball_Collector.Utility;
using UnityEngine;

namespace Ball_Collector
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Range(1, 5)][SerializeField] private float _moveSpeed = 5;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        private float _horizontalInput;
        private float _rightBoundary;
        private float _leftBoundary;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            float objectHalfWidth = _spriteRenderer.bounds.extents.x;

            WorldScreenBounds screenBounds = new(transform.position.z);

            _leftBoundary = -screenBounds.GetHorizontalBound() + objectHalfWidth;
            _rightBoundary = screenBounds.GetHorizontalBound() - objectHalfWidth;
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            Vector3 inputVector = new(_horizontalInput, 0, 0);

            Vector3 targetPosition = transform.position + _moveSpeed * Time.fixedDeltaTime * inputVector.normalized;
            targetPosition.x = Mathf.Clamp(targetPosition.x, _leftBoundary, _rightBoundary);

            _rb.MovePosition(targetPosition);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out ICollectible collectible))
            {
                collectible.Collect();
            }
        }
    }
}