using UnityEngine;

namespace Ball_Collector
{
    public class RedBall : Ball, ICollectible
    {
        public void Collect()
        {
            Timer.Instance.RemoveSeconds(2);
            ScoreUI.Instance.RemoveScore(1);
            BallPool.Instance.ReturnToPool(this);
        }
    }
}