using UnityEngine;

namespace Ball_Collector
{
    public class RedBall : Ball, ICollectible
    {
        public void Collect()
        {
            Debug.Log("Red Ball");
        }
    }
}