using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class DayTracker : MonoBehaviour
    {
        [Header("Game Settings")]
        public int MaxDaysEasy = 365;
        public int MaxDaysMedium = 180;
        public int MaxDaysHard = 90;

        [Header("Current Game")]
        public int CurrentDay;

        public GameDifficultyLevels CurrentDifficulty = GameDifficultyLevels.Medium;

        [Header("User Interface")]
        public TMP_Text DifficultyText;
        public TMP_Text DayCounterText;

        [Header("Other references")]
        public GameControllers Controllers;
        
        public void Reset()
        {
            int maxDays = GetDaysForDifficulty(CurrentDifficulty);
            DifficultyText.text = $"Make a game in {maxDays}!";
            CurrentDay = 0;
            UpdateUI();
        }

        private int GetDaysForDifficulty(GameDifficultyLevels currentDifficulty)
        {
            switch (currentDifficulty)
            {
                case GameDifficultyLevels.Easy: return MaxDaysEasy;
                case GameDifficultyLevels.Medium: return MaxDaysMedium;
                case GameDifficultyLevels.Hard: return MaxDaysHard;
            }

            throw new NotImplementedException(currentDifficulty.ToString());
        }

        /// <returns>True means more time. </returns>
        public bool IncreaseDay()
        {
            ++CurrentDay;
            UpdateUI();
            
            int maxDays = GetDaysForDifficulty(CurrentDifficulty);
            return CurrentDay < maxDays;
        }

        public void UpdateUI()
        {
            int maxDays = GetDaysForDifficulty(CurrentDifficulty);
            DayCounterText.text = $"{maxDays-CurrentDay} till release";   
        }

        public void SetEasy()
        {
            CurrentDifficulty = GameDifficultyLevels.Easy;
            Controllers.StartANewGame();
        }
        
        public void SetMedium()
        {
            CurrentDifficulty = GameDifficultyLevels.Medium;
            Controllers.StartANewGame();
        }
        
        public void SetHard()
        {
            CurrentDifficulty = GameDifficultyLevels.Hard;
            Controllers.StartANewGame();
        }
    }
}