using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ball_Collector
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        public void DisplayTime(string time)
        {
            _timerText.text = time;
        }
    }
}