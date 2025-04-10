using System;
using UnityEngine;

namespace Ball_Collector
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Range(1.0f, 5.0f)][SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rb;
        private float _minX;
        private float _maxX;
        private float _horizontalInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Camera mainCam = Camera.main;

            if (!mainCam.orthographic)
                Debug.LogError("Camera projection is not set to Orthographic");

            float _objectHalfWidth = _spriteRenderer.bounds.extents.x;
            float _objectHalfHeight = _spriteRenderer.bounds.extents.y;

            Vector3 _screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));

            transform.position = new Vector3(0, -_screenBounds.y + _objectHalfHeight, 0);

            _minX = -_screenBounds.x + _objectHalfWidth;
            _maxX = _screenBounds.x - _objectHalfWidth;

            Debug.Log($"Camera Bounds: minX = {_minX}, maxX = {_maxX}");
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            Vector3 inputVector = new(_horizontalInput, 0.0f, 0.0f);

            Vector3 targetPosition = transform.position + _moveSpeed * Time.fixedDeltaTime * inputVector.normalized;
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minX, _maxX);

            _rb.MovePosition(targetPosition);
        }
    }
}