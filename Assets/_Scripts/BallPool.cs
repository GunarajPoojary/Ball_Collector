using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ball_Collector
{
    public class BallPool : MonoBehaviour
    {
        public static BallPool Instance { get; private set; }
        [SerializeField] private List<BallType> _ballTypes;
        private float _totalWeight;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InitializePools();
            _totalWeight = _ballTypes.Sum(b => b.SpawnWeight);
        }

        private void InitializePools()
        {
            foreach (var ballType in _ballTypes)
            {
                for (int i = 0; i < ballType.SpawnWeight; i++)
                {
                    Ball ball = Instantiate(ballType.BallPrefab, transform);
                    ball.gameObject.SetActive(false);
                    ballType.Pool.Enqueue(ball);
                }
            }
        }

        public void SpawnBall(Vector3 position)
        {
            BallType selected = GetRandomBallType();
            if (selected.Pool.Count == 0)
            {
                Debug.LogWarning($"No {selected.BallPrefab.name} balls left in pool!");
                return;
            }

            Ball ball = selected.Pool.Dequeue();
            ball.transform.position = position;
            ball.gameObject.SetActive(true);
        }

        private BallType GetRandomBallType()
        {
            float rand = UnityEngine.Random.Range(0f, _totalWeight);
            float runningSum = 0f;

            foreach (var type in _ballTypes)
            {
                runningSum += type.SpawnWeight;
                if (rand <= runningSum)
                    return type;
            }

            return _ballTypes[_ballTypes.Count - 1];
        }

        public void ReturnToPool(Ball ball)
        {
            ball.gameObject.SetActive(false);
            BallType type = _ballTypes.Find(t => t.BallPrefab.GetType() == ball.GetType());
            type?.Pool.Enqueue(ball);
        }
    }
}