using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class GameEvents : MonoBehaviour
    {
        [Header("Other References")]
        public GameProject GameProject;
        public PersonalSkills PersonalSkills;
        public DayTracker DayTracker;
        public ReleaseGame ReleaseGame;
        
        public List<SingleEvent> Events;
        public Dictionary<SingleEvent, int> TimesTracker;

        [Header("UI")]
        public TMP_Text Description;
        public TMP_Text Left;
        public TMP_Text Right;
        public TMP_Text CurrentText;
        public GameObject ButtonPanel;
        public GameObject PushforwardPanel;

        [Header("Easy Med Hard")]
        public int EasyLevel;
        public int MedLevel;
        public int HardLevel;
        
        private Queue<SingleEvent> eventsInPlay;
        private SingleEvent currentEvent;

        /// <summary>
        /// List of pending new items in queue;
        /// </summary>
        private List<SingleEvent> toBeReAdded;

        private bool ifLoaded;
        
        private void Start()
        {
            if (!ifLoaded)
            {
                LoadEvents();
            }
        }
        
        public void PullNewEvent()
        {
            if (!ifLoaded)
            {
                LoadEvents();
            }

            if (!this.eventsInPlay.Any())
            {
                RefillQueue();
            }
            
            this.currentEvent = this.eventsInPlay.Dequeue();

            this.Description.text = this.currentEvent.Description;
            this.Left.text = SetOptionButtonText(this.currentEvent.Left);
            this.Right.text = SetOptionButtonText(this.currentEvent.Right);

            if (ShouldReAddToQueue(this.currentEvent))
            {
                toBeReAdded.Add(this.currentEvent);
            }
        }

        private void RefillQueue()
        {
            List<SingleEvent> shuffled = IHateStatics.Shuffle(this.toBeReAdded, new System.Random());
            foreach (SingleEvent se in shuffled)
            {
                this.eventsInPlay.Enqueue(se);
            }
            
            this.toBeReAdded.Clear();
        }

        private bool ShouldReAddToQueue(SingleEvent singleEvent)
        {
            if (singleEvent.RetainInPool)
            {
                return true;
            }
            
            int timesLeftToUse = 0;
            if (TimesTracker.TryGetValue(singleEvent, out int value))
            {
                TimesTracker[singleEvent] = value - 1;
                
                timesLeftToUse = value;
            }
            else
            {
                int getTimesLeft = CalculateTimesBeforeDequeue(singleEvent) - 1;
                TimesTracker.Add(singleEvent, getTimesLeft);

                timesLeftToUse = getTimesLeft;
            }

            return timesLeftToUse > 0;
        }

        private int CalculateTimesBeforeDequeue(SingleEvent singleEvent)
        {
            if (singleEvent.OverrideTimesInPool)
            {
                return singleEvent.OverrideTimesInPoolAmount;
            }
            
            if (singleEvent.Left.Difficulty == DifficultyOfEvent.Hard &&
                singleEvent.Right.Difficulty == DifficultyOfEvent.Hard)
            {
                return 1000;
            }
            if (singleEvent.Left.Difficulty == DifficultyOfEvent.Hard &&
                singleEvent.Right.Difficulty == DifficultyOfEvent.Medium)
            {
                return 1000;
            }
            if (singleEvent.Left.Difficulty == DifficultyOfEvent.Medium &&
                singleEvent.Right.Difficulty == DifficultyOfEvent.Hard)
            {
                return 1000;
            }
            if (singleEvent.Left.Difficulty == DifficultyOfEvent.Medium &&
                singleEvent.Right.Difficulty == DifficultyOfEvent.Medium)
            {
                return 1000;
            }
            
            float leftCalcuation = CalculateTimesBeforeDequeue(singleEvent.Left.Difficulty);
            float rightCalcuation = CalculateTimesBeforeDequeue(singleEvent.Right.Difficulty);

            return (int)(leftCalcuation + rightCalcuation);
        }
        
        private float CalculateTimesBeforeDequeue(DifficultyOfEvent difficulty)
        {
            float times = 0;
            switch (difficulty)
            {
                case DifficultyOfEvent.Guaranteed: times = 0.5f; break;
                case DifficultyOfEvent.Easy: times = 1.5f; break;
                case DifficultyOfEvent.Medium: times = 2.5f; break;
                case DifficultyOfEvent.Hard: times = 4f; break;
            }

            return times * 2;
        }

        private string SetOptionButtonText(EventOption option)
        {
            return $"{option.ButtonDescription} {InsertDifficultyAsText(option.Difficulty)}" +
                   $"{InsertTestStatAsText(option.DifficultyStat)}";
        }

        private string InsertTestStatAsText(PlayerProjectStat optionDifficultyStat)
        {
            switch (optionDifficultyStat)
            {
                case PlayerProjectStat.GameArt: return "<b>Art</b>";
                case PlayerProjectStat.GameBugs: return "<b>Stability</b>";
                case PlayerProjectStat.GameGameplay: return "<b>Gameplay</b>";
                case PlayerProjectStat.GameMarketing: return "<b>Marketing</b>";
                case PlayerProjectStat.PersonalSocialSkills: return "<b>Social</b>";
                case PlayerProjectStat.PersonalEnergyLevels: return "<b>Energy</b>";
                default: throw new NotImplementedException($"{optionDifficultyStat.ToString()} not implemented");
            }
        }

        private string InsertDifficultyAsText(DifficultyOfEvent optionDifficulty)
        {
            switch (optionDifficulty)
            {
                case DifficultyOfEvent.Guaranteed: return "<b><color=#008000>[V.EASY]</color></b>";
                case DifficultyOfEvent.Easy: return "<b><color=#008000>[EASY]</color></b>";
                case DifficultyOfEvent.Medium: return "<b><color=#5865F2>[MED]</color></b>";
                case DifficultyOfEvent.Hard: return "<b><color=#990000>[HARD]</color></b>";
                default: throw new NotImplementedException($"{optionDifficulty.ToString()} not implemented");
            }
            
        }

        private void LoadEvents()
        {
            this.toBeReAdded = new List<SingleEvent>();
            TimesTracker = new Dictionary<SingleEvent, int>();
            this.eventsInPlay = new Queue<SingleEvent>();
            List<SingleEvent> shuffled = IHateStatics.Shuffle(this.Events, new System.Random());
            foreach (SingleEvent eo in shuffled)
            {
                this.eventsInPlay.Enqueue(eo);
                TimesTracker.Add(eo, CalculateTimesBeforeDequeue(eo));
            }

            this.Left.richText = true;
            this.Right.richText = true;

            ifLoaded = true;
            
            ButtonPanel.SetActive(true);
            PushforwardPanel.SetActive(false);
        }

        public void DoLeft()
        {
            DoButton(this.currentEvent.Left);
        }

        public void DoRight()
        {
            DoButton(this.currentEvent.Right);
        }

        public void DoButton(EventOption option)
        {
            ButtonPanel.SetActive(false);
            PushforwardPanel.SetActive(true);

            EffectivenessOfTask effectivenessOfTask = EffectivenessOfTask.Effective;
            if (TestStat(option.Difficulty, option.DifficultyStat))
            {
                effectivenessOfTask = EffectivenessOfTask.Effective;
                CurrentText.text = option.SuccessText;
            }
            else
            {
                effectivenessOfTask = EffectivenessOfTask.Ineffective;
                CurrentText.text = option.FailureText;
            }
            
            
            switch (option.TypeOfEvent)
            {
                case TypeOfEvent.Work: 
                    this.GameProject.WorkOnProject(option.WorkTask, effectivenessOfTask);
                    break;
                case TypeOfEvent.Play: 
                    PersonalSkills.PersonalTask(option.PlayTask, effectivenessOfTask);
                    break;
            }
        }

        private bool TestStat(DifficultyOfEvent optionDifficulty, PlayerProjectStat optionDifficultyStat)
        {
            switch (optionDifficulty)
            {
                case DifficultyOfEvent.Guaranteed: return true;
                case DifficultyOfEvent.Easy: return StatIsAbove(EasyLevel, optionDifficultyStat);
                case DifficultyOfEvent.Medium: return StatIsAbove(MedLevel, optionDifficultyStat);
                case DifficultyOfEvent.Hard: return StatIsAbove(HardLevel, optionDifficultyStat);
                default: throw new NotImplementedException($"{optionDifficulty.ToString()} not implemented");
            }

            return false;
        }

        private bool StatIsAbove(int easyLevel, PlayerProjectStat optionDifficultyStat)
        {
            switch (optionDifficultyStat)
            {
                case PlayerProjectStat.GameArt: return GameProject.art > easyLevel;
                case PlayerProjectStat.GameGameplay: return GameProject.gameplay > easyLevel;
                case PlayerProjectStat.GameMarketing: return GameProject.marketing > easyLevel;
                case PlayerProjectStat.GameBugs: return GameProject.bugs > easyLevel;
                case PlayerProjectStat.PersonalSocialSkills: return PersonalSkills.Social > easyLevel;
                case PlayerProjectStat.PersonalEnergyLevels: return PersonalSkills.Energy > easyLevel;
                default: throw new NotImplementedException($"{optionDifficultyStat.ToString()} not implemented");
            }
        }

        public void PushForwards()
        {
            ButtonPanel.SetActive(true);
            PushforwardPanel.SetActive(false);

            bool moreTime = DayTracker.IncreaseDay();
            if (moreTime)
            {
                PullNewEvent();

            }
            else
            {
                ReleaseGame.DoReleaseGame();
            }

        }

        public void Reset()
        {
            LoadEvents();
            PullNewEvent();
        }
    }
}