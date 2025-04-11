using UnityEngine;
using UnityEngine.Events;
using System;

namespace Ball_Collector
{
    /// <summary>
    /// A high-performance timer class for mobile devices.
    /// Tracks time in mm:ss format with ability to extend or reduce time at runtime.
    /// </summary>
    public class Timer : MonoBehaviour
    {
        public static Timer Instance { get; private set; }
        [SerializeField] private int _initialMinutes = 1;
        [SerializeField] private int _initialSeconds = 0;

        // Events
        public UnityEvent<string> OnTimeUpdated;
        public UnityEvent OnTimerComplete;

        private float _remainingSeconds;
        private bool _isRunning;

        // Cached values to avoid string allocations
        private int _minutes;
        private int _seconds;
        private string _cachedTimeDisplay;

        private void Awake()
        {
            Instance = this;
            ResetTimer();
        }

        private void Start()
        {
            StartTimer();
        }

        private void Update()
        {
            if (!_isRunning) return;

            _remainingSeconds -= Time.deltaTime;

            if (_remainingSeconds <= 0)
            {
                _remainingSeconds = 0;
                TimerComplete();
            }

            // Only update the display when the seconds value changes to reduce overhead
            int newSeconds = Mathf.FloorToInt(_remainingSeconds % 60);
            int newMinutes = Mathf.FloorToInt(_remainingSeconds / 60);

            if (newSeconds != _seconds || newMinutes != _minutes)
            {
                _seconds = newSeconds;
                _minutes = newMinutes;
                UpdateTimeDisplay();
            }
        }

        /// <summary>
        /// Resets the timer to its initial value.
        /// </summary>
        public void ResetTimer()
        {
            _remainingSeconds = (_initialMinutes * 60) + _initialSeconds;
            UpdateTimeDisplay();
            _isRunning = false;
        }

        /// <summary>
        /// Starts or resumes the timer.
        /// </summary>
        public void StartTimer()
        {
            if (_remainingSeconds > 0)
            {
                _isRunning = true;
            }
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void PauseTimer()
        {
            _isRunning = false;
        }

        /// <summary>
        /// Adds seconds to the timer.
        /// </summary>
        /// <param name="secondsToAdd">The number of seconds to add.</param>
        public void AddSeconds(int secondsToAdd)
        {
            if (secondsToAdd <= 0) return;

            _remainingSeconds += secondsToAdd;
            UpdateTimeDisplay();
        }

        /// <summary>
        /// Adds minutes to the timer.
        /// </summary>
        /// <param name="minutesToAdd">The number of minutes to add.</param>
        public void AddMinutes(int minutesToAdd)
        {
            if (minutesToAdd <= 0) return;

            _remainingSeconds += minutesToAdd * 60;
            UpdateTimeDisplay();
        }

        /// <summary>
        /// Removes seconds from the timer.
        /// </summary>
        /// <param name="secondsToRemove">The number of seconds to remove.</param>
        public void RemoveSeconds(int secondsToRemove)
        {
            if (secondsToRemove <= 0) return;

            _remainingSeconds = Mathf.Max(0, _remainingSeconds - secondsToRemove);
            UpdateTimeDisplay();

            if (_remainingSeconds <= 0 && _isRunning)
            {
                TimerComplete();
            }
        }

        /// <summary>
        /// Removes minutes from the timer.
        /// </summary>
        /// <param name="minutesToRemove">The number of minutes to remove.</param>
        public void RemoveMinutes(int minutesToRemove)
        {
            if (minutesToRemove <= 0) return;

            _remainingSeconds = Mathf.Max(0, _remainingSeconds - (minutesToRemove * 60));
            UpdateTimeDisplay();

            if (_remainingSeconds <= 0 && _isRunning)
            {
                TimerComplete();
            }
        }

        /// <summary>
        /// Gets the current time display in mm:ss format.
        /// </summary>
        /// <returns>String representation of the current time.</returns>
        public string GetTimeDisplay()
        {
            return _cachedTimeDisplay;
        }

        private void UpdateTimeDisplay()
        {
            _minutes = Mathf.FloorToInt(_remainingSeconds / 60);
            _seconds = Mathf.FloorToInt(_remainingSeconds % 60);

            // Using string.Format instead of string concatenation to reduce garbage collection
            _cachedTimeDisplay = string.Format("{0:00}:{1:00}", _minutes, _seconds);

            // Notify listeners of the time change
            OnTimeUpdated.Invoke(_cachedTimeDisplay);
        }

        private void TimerComplete()
        {
            _isRunning = false;
            _remainingSeconds = 0;
            UpdateTimeDisplay();
            OnTimerComplete.Invoke();
        }
    }
}