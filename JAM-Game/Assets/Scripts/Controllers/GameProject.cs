using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class GameProject : MonoBehaviour
    {

        
        [Header("Stats")]
        [SerializeField]
        private int art;

        [SerializeField]
        private int gameplay;

        [SerializeField]
        private int marketing;
        
        [SerializeField]
        private int skillsMax;

        [Header("UI elements")]
        public Scrollbar scrollbarLooks;
        public TextMeshProUGUI tmpLooks;
        
        public Scrollbar scrollbarGameplay;
        public TextMeshProUGUI tmpGameplay;
        
        public Scrollbar scrollbarReach;
        public TextMeshProUGUI tmpReach;
        
        private int bugs;
        
        
        public void Reset()
        {
            art = 0;
            gameplay = 0;
            marketing = 0;
        }

        public void WorkOnProject(ProjectTask task)
        {
            ExecuteBugs(task);
            
            MainTasks(task);

            UpdateUI();
        }

        private void MainTasks(ProjectTask task)
        {
            switch (task)
            {
                case ProjectTask.Environment:
                    this.art += (int) (this.art * 0.15f);
                    break;
                case ProjectTask.Shaders:
                    this.art += (int) (this.art * 0.15f);
                    break;
                case ProjectTask.Models:
                    this.art += (int) (this.art * 0.1f);
                    break;
                case ProjectTask.Gameplay:
                    this.gameplay += (int) (this.gameplay * 0.15f);
                    break;
                case ProjectTask.Tools:
                    this.gameplay += (int) (this.gameplay * 0.1f);
                    this.art += (int) (this.gameplay * 0.1f);
                    break;
                case ProjectTask.Conference:
                    this.marketing += (int) (this.art * 0.1f) + (int) (this.gameplay * 0.1f);
                    break;
                case ProjectTask.SocialMedia:
                    this.marketing += (int) (this.art * 0.1f) + (int) (this.gameplay * 0.1f);
                    break;
                case ProjectTask.OnlineAds:
                    this.marketing += (int) (this.art * 0.05f) + (int) (this.gameplay * 0.05f);
                    break;
            }
        }

        private void ExecuteBugs(ProjectTask task)
        {
            switch (task)
            {
                case ProjectTask.Gameplay:
                case ProjectTask.Tools:
                    bugs += Random.Range(1, 7);
                    break;
                case ProjectTask.Environment:
                case ProjectTask.Shaders:
                    bugs += Random.Range(1, 10);
                    break;
            }

            switch (task)
            {
                case ProjectTask.Bugs:
                    bugs -= Random.Range(20, 30);
                    if (bugs < 0)
                    {
                        bugs = 0;
                    }

                    break;
            }
        }

        public void UpdateUI()
        {
            this.scrollbarLooks.size = IHateStatics.GetProgressBarValue(this.art, this.skillsMax);
            this.scrollbarGameplay.size = IHateStatics.GetProgressBarValue(this.gameplay, this.skillsMax);
            this.scrollbarReach.size = IHateStatics.GetProgressBarValue(this.marketing, this.skillsMax);


            this.tmpLooks.text = $"{this.art} / {this.skillsMax}";
            this.tmpLooks.text = $"{this.gameplay} / {this.skillsMax}";
            this.tmpLooks.text = $"{this.marketing} / {this.skillsMax}";
        }


    }
}