using System;
using UnityEngine;

namespace Ball_Collector
{
    public class PlayerController : MonoBehaviour
    {
        [Range(1.0f, 5.0f)][SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private float _minX;
        private float _maxX;

        private void Start()
        {
            float _objectHalfWidth = _spriteRenderer.bounds.extents.x;
            float _objectHalfHeight = _spriteRenderer.bounds.extents.y;

            Vector3 _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));

            transform.position = new Vector3(0, -_screenBounds.y + _objectHalfHeight, 0);

            _minX = -_screenBounds.x + _objectHalfWidth;
            _maxX = _screenBounds.x - _objectHalfWidth;

            Debug.Log($"Camera Bounds: minX = {_minX}, maxX = {_maxX}");
        }

        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 inputVector = new(horizontalInput, 0.0f, 0.0f);

            Vector3 targetPosition = transform.position + _moveSpeed * Time.deltaTime * inputVector.normalized;
            targetPosition.x = Mathf.Clamp(targetPosition.x, _minX, _maxX);

            transform.position = targetPosition;
        }
    }
}