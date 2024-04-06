using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    public class GameEvents : MonoBehaviour
    {
        public GameProject GameProject;
        
        public List<SingleEvent> Events;

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
        
        private Stack<SingleEvent> eventsInPlay;
        private SingleEvent currentEvent;

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
            
            this.currentEvent = this.eventsInPlay.Pop();

            this.Description.text = this.currentEvent.Description;
            this.Left.text = SetOptionButtonText(this.currentEvent.Left);
            this.Right.text = SetOptionButtonText(this.currentEvent.Right);

            if (!this.currentEvent.OnlyUseOnce)
            {
                this.eventsInPlay.Push(this.currentEvent);
            }
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
                case DifficultyOfEvent.Easy: return "<color=#008000>[EASY]</color>";
                case DifficultyOfEvent.Medium: return "<color=#FF5733>[MED]</color>";
                case DifficultyOfEvent.Hard: return "<color=#990000>[HARD]</color>";
                default: throw new NotImplementedException($"{optionDifficulty.ToString()} not implemented");
            }
            
        }

        private void LoadEvents()
        {
            this.eventsInPlay = new Stack<SingleEvent>();
            List<SingleEvent> shuffled = IHateStatics.Shuffle(this.Events, new System.Random());
            foreach (SingleEvent eo in shuffled)
            {
                this.eventsInPlay.Push(eo);
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
                    //this.GameProject.WorkOnProject(option.PlayTask);
                    break;
            }
        }

        private bool TestStat(DifficultyOfEvent optionDifficulty, PlayerProjectStat optionDifficultyStat)
        {
            switch (optionDifficulty)
            {
                case DifficultyOfEvent.Easy: return StatIsAbove(EasyLevel, optionDifficultyStat);
                case DifficultyOfEvent.Medium: return StatIsAbove(MedLevel, optionDifficultyStat);
                case DifficultyOfEvent.Hard: return StatIsAbove(HardLevel, optionDifficultyStat);
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
            }

            return false;
        }

        public void PushForwards()
        {
            ButtonPanel.SetActive(true);
            PushforwardPanel.SetActive(false);
            PullNewEvent();
        }
    }
}