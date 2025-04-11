using UnityEngine;

namespace Ball_Collector
{
    public class PurpleBall : Ball, ICollectible
    {
        public void Collect()
        {
            Timer.Instance.AddSeconds(2);
            ScoreUI.Instance.AddScore(1);
            BallPool.Instance.ReturnToPool(this);
        }
    }
}
