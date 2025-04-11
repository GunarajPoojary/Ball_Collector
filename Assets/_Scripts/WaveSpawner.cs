using System.Collections;
using Ball_Collector.Utility;
using UnityEngine;

namespace Ball_Collector
{
    public class WaveSpawner : MonoBehaviour
    {
        [Range(0.5f, 1f)][SerializeField] private float _sideOffset = 1f;
        [Range(0.5f, 1f)][SerializeField] private float _topOffset = 1f;

        private float _leftBoundary;
        private float _rightBoundary;
        [SerializeField] private BallPool _ballPool;
        [SerializeField] private float _maxSpawnDelay = 2f;
        [SerializeField] private float _minSpawnDelay = 2f;
        private float _spawnTimer = 0f;
        private float _nextSpawnDelay = 1f;

        private WorldScreenBounds _screenBounds;
        private float _topBoundary;

        private void Awake()
        {
            _screenBounds = new WorldScreenBounds(transform.position.z);
            _topBoundary = _screenBounds.GetVerticalBound() + _topOffset;
            _rightBoundary = _screenBounds.GetHorizontalBound() - _sideOffset;
            _leftBoundary = -_rightBoundary;

            transform.position = new Vector3(0, _topBoundary, 0);
        }

        private void Start()
        {
            SetNextDelay();
        }

        private void Update()
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _nextSpawnDelay)
            {
                SpawnBall();
                SetNextDelay();
                _spawnTimer = 0f;
            }
        }

        private void SetNextDelay()
        {
            _nextSpawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        }

        private void SpawnBall()
        {
            Vector3 spawnOffset = new(Random.Range(_leftBoundary, _rightBoundary), 0f, 0f);
            Vector3 spawnPosition = transform.position + spawnOffset;

            _ballPool.SpawnBall(spawnPosition);
        }
    }
}