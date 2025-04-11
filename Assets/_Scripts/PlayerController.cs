using System;
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
        private float _maxX;
        private float _minX;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            float objectHalfWidth = _spriteRenderer.bounds.extents.x;

            ScreenBounds screenBounds = new(transform.position.z);

            _minX = screenBounds.GetMinHorizontalBound() + objectHalfWidth;
            _maxX = screenBounds.GetMaxHorizontalBound() - objectHalfWidth;
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            Vector3 inputVector = new(_horizontalInput, 0, 0);

            Vector3 targetPosition = transform.position + _moveSpeed * Time.fixedDeltaTime * inputVector.normalized;
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minX, _maxX);

            _rb.MovePosition(targetPosition);
        }
    }
}