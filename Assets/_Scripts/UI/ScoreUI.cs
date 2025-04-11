using UnityEngine;
using TMPro;

namespace Ball_Collector
{
    public class ScoreUI : MonoBehaviour
    {
        public static ScoreUI Instance { get; private set; }

        private int _score;
        private int _purpleCount;

        [SerializeField] private TMP_Text _scoreText;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void AddScore(int amount)
        {
            _score += amount;
            _purpleCount += amount;

            if (_purpleCount >= 10)
            {
                Debug.Log($"Purple Count set to {_purpleCount}");
                Timer.Instance.AddSeconds(10);
                _purpleCount = 0; // reset counter after bonus
            }

            UpdateUI();
        }

        public void RemoveScore(int amount)
        {
            _score = Mathf.Max(0, _score - amount);
            _purpleCount = 0;
            Debug.Log($"Purple Count set to {_purpleCount}");
            UpdateUI();
        }

        private void UpdateUI()
        {
            _scoreText.text = $"Score: {_score}";
        }
    }
}