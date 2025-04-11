using System.Collections.Generic;
using UnityEngine;

namespace Ball_Collector
{
    [System.Serializable]
    public class BallType
    {
        public Ball BallPrefab;
        public float SpawnWeight;
        [HideInInspector] public Queue<Ball> Pool = new();
    }
}