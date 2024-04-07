using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class DayTracker : MonoBehaviour
    {
        [Header("Game Settings")]
        public int MaxDays = 100;

        [Header("Current Game")]
        public int CurrentDay;

        [Header("User Interface")]
        public TMP_Text DayCounterText;
        
        public void Reset()
        {
            CurrentDay = 0;
            UpdateUI();
        }
        
        /// <returns>True means more time. </returns>
        public bool IncreaseDay()
        {
            ++CurrentDay;
            UpdateUI();
            
            return CurrentDay < MaxDays;
        }

        public void UpdateUI()
        {
            DayCounterText.text = $"{MaxDays-CurrentDay} till release";   
        }
    }
}