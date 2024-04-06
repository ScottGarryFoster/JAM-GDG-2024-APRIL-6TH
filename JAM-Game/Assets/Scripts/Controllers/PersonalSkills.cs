using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class PersonalSkills : MonoBehaviour
    {
        [Header("Personal Skills")]
        public int Energy;

        public int Contacts;

        public int Social;

        public Scrollbar ScrollbarEnergy;
        public TMP_Text TextEnergy;

        [Header("Other Stats")]
        public GameProject GameProject;
        
        public void PersonalTask(PlayTask playTask, EffectivenessOfTask effectivenessOfTask)
        {
            // Add random events in here
            switch (playTask)
            {
                case PlayTask.PlayGames:
                    Energy += AdjustForEffectivness(Random.Range(5, 10), effectivenessOfTask);
                    RandomPlayGamesEvent(Energy, effectivenessOfTask);
                    break;
                case PlayTask.SocialMedia:
                    Energy += AdjustForEffectivness(Random.Range(1, 20), effectivenessOfTask);
                    RandomSocialMediaEvent(Energy, effectivenessOfTask);
                    break;
                case PlayTask.Socialise:
                    Energy += AdjustForEffectivness(Random.Range(1, 20), effectivenessOfTask);
                    RandomSocialiseEvent(Energy, effectivenessOfTask);
                    break;
            }

            if (Energy > 100)
            {
                Energy = 100;
            }
            
            UpdateUI();
        }
        
        private void RandomSocialMediaEvent(int energy, EffectivenessOfTask effectivenessOfTask)
        {
            int random = CreateRandomRangeToBeatFromEffectiveness(effectivenessOfTask);
            if (energy >= random)
            {
                int gameplayAmount = 0;
                switch (effectivenessOfTask)
                {
                    case EffectivenessOfTask.Ineffective: gameplayAmount = Random.Range(1, 10); break;
                    case EffectivenessOfTask.Effective: gameplayAmount = Random.Range(5, 15); break;
                    case EffectivenessOfTask.CriticalHit: gameplayAmount = Random.Range(10, 25); break;
                }

                GameProject.AddReachBoost(gameplayAmount);
            }

        }

        private void RandomSocialiseEvent(int energy, EffectivenessOfTask effectivenessOfTask)
        {
            int random = CreateRandomRangeToBeatFromEffectiveness(effectivenessOfTask);
            if (energy >= random)
            {
                int gameplayAmount = 0;
                switch (effectivenessOfTask)
                {
                    case EffectivenessOfTask.Ineffective: gameplayAmount = Random.Range(1, 7); break;
                    case EffectivenessOfTask.Effective: gameplayAmount = Random.Range(3, 7); break;
                    case EffectivenessOfTask.CriticalHit: gameplayAmount = Random.Range(3, 15); break;
                }

                GameProject.AddReachBoost(gameplayAmount);
            }
        }

        private void RandomPlayGamesEvent(int energy, EffectivenessOfTask effectivenessOfTask)
        {
            int random = CreateRandomRangeToBeatFromEffectiveness(effectivenessOfTask);
            if (energy >= random)
            {
                int gameplayAmount = 0;
                switch (effectivenessOfTask)
                {
                    case EffectivenessOfTask.Ineffective: gameplayAmount = Random.Range(1, 3); break;
                    case EffectivenessOfTask.Effective: gameplayAmount = Random.Range(1, 5); break;
                    case EffectivenessOfTask.CriticalHit: gameplayAmount = Random.Range(2, 7); break;
                }

                GameProject.AddGameplayBoost(gameplayAmount);
            }

        }

        private int CreateRandomRangeToBeatFromEffectiveness(EffectivenessOfTask effectivenessOfTask)
        {
            switch (effectivenessOfTask)
            {
                case EffectivenessOfTask.Ineffective: return Random.Range(75, 150);
                case EffectivenessOfTask.Effective: return Random.Range(50, 150);
                case EffectivenessOfTask.CriticalHit: return Random.Range(25, 150);
            }

            return 50;
        }

        private int AdjustForEffectivness(int range, EffectivenessOfTask effectivenessOfTask)
        {
            int newRange = range;
            switch (effectivenessOfTask)
            {
                case EffectivenessOfTask.Ineffective: newRange /= 2; break;
                case EffectivenessOfTask.CriticalHit: newRange *= 2; break;
            }

            if (newRange < 0)
            {
                return Random.Range(1, 10);
            }

            return newRange;
        }

        public void UpdateUI()
        {
            ScrollbarEnergy.size = IHateStatics.GetProgressBarValue(Energy, 100);
            TextEnergy.text = $"{Energy} / 100";
        }
    }
}