using UnityEngine;

namespace Ball_Collector.Utility
{
    public class WorldScreenBounds
    {
        private Vector3 _screenBounds;

        public WorldScreenBounds(float zValue)
        {
            Camera mainCam = Camera.main;

            if (!mainCam.orthographic)
                Debug.LogError("Camera projection is not set to Orthographic");

            _screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, zValue));
        }

        public float GetHorizontalBound() => _screenBounds.x;
        public float GetVerticalBound() => _screenBounds.y;
    }
}