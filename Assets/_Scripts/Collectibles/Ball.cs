using Ball_Collector.Utility;
using UnityEngine;

namespace Ball_Collector
{
    public interface ICollectible
    {
        void Collect();
    }

    public abstract class Ball : MonoBehaviour
    {
        public bool IsUsed { get; set; } = false;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Range(1, 10)][SerializeField] private float _fallSpeed = 5f;
        private float _bottomBoundary;

        protected virtual void Start()
        {
            float objectHalfWidth = _spriteRenderer.bounds.extents.x;

            WorldScreenBounds screenBounds = new(transform.position.z);
            _bottomBoundary = -screenBounds.GetVerticalBound() - objectHalfWidth;
        }

        private void Update()
        {
            transform.Translate(_fallSpeed * Time.deltaTime * Vector3.down);

            if (transform.position.y < _bottomBoundary)
            {
                BallPool.Instance.ReturnToPool(this);
            }
        }
    }
}