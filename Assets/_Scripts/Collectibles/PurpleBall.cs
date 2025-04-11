using UnityEngine;

namespace Ball_Collector
{
    public class PurpleBall : Ball, ICollectible
    {
        public void Collect()
        {
            Debug.Log("Purple Ball");
        }
    }
}
