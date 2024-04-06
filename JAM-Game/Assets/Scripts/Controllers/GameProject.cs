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

        private int hiddenGameplay;
        private int hiddenTools;

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

        public void WorkOnPrograming()
        {
            WorkOnProject(ProjectTask.Gameplay);
        }
        
        public void WorkOnTools()
        {
            WorkOnProject(ProjectTask.Tools);
        }
        
        public void WorkOnEnvironment()
        {
            WorkOnProject(ProjectTask.Environment);
        }
        
        public void WorkOnShaders()
        {
            WorkOnProject(ProjectTask.Shaders);
        }
        
        public void WorkOnModels()
        {
            WorkOnProject(ProjectTask.Models);
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
                case ProjectTask.Shaders:
                case ProjectTask.Models:
                case ProjectTask.Tools:
                    if (this.art == 0)
                    {
                        this.art += Random.Range(1, 5);
                    }

                    break;
            }
            
            switch (task)
            {
                case ProjectTask.Gameplay:
                case ProjectTask.Tools:
                    if (this.gameplay == 0)
                    {
                        this.gameplay += Random.Range(1, 5);
                    }

                    break;
            }

            int boost = 0;
            switch (task)
            {
                case ProjectTask.Environment:
                    this.art += GetBoost((int) (this.art * 0.1f));
                    break;
                case ProjectTask.Shaders:
                    this.art += GetBoost((int) (this.art * 0.1f));
                    break;
                case ProjectTask.Models:
                    this.art += GetBoost((int) (this.art * 0.1f));
                    break;
                case ProjectTask.Gameplay:
                    if (this.hiddenTools > 4 && this.gameplay > 15)
                    {
                        this.gameplay += GetBoost((int) (this.gameplay * 0.2f));
                    }
                    else
                    {
                        this.gameplay += GetBoost((int) (this.gameplay * 0.1f));
                    }
                    break;
                case ProjectTask.Tools:
                    this.hiddenTools += Random.Range(0, 2);
                    if (this.hiddenTools > 5)
                    {
                        if (this.gameplay > 10)
                        {
                            this.gameplay += GetBoost((int) (this.gameplay * 0.1f));
                        }
                        else
                        {
                            this.gameplay += Random.Range(1, 3);
                        }

                        if (this.art > 10)
                        {
                            this.art += GetBoost((int) (this.art * 0.1f));
                        }
                        else
                        {
                            this.art += Random.Range(1, 3);
                        }
                    }
                    else
                    {
                        if (this.gameplay > 15)
                        {
                            this.gameplay += GetBoost((int) (this.gameplay * 0.05f));
                        }
                        else
                        {
                            this.gameplay += Random.Range(1, 3);
                        }
                        
                        if (this.art > 15)
                        {
                            this.art += GetBoost((int) (this.art * 0.05f));
                        }
                        else
                        {
                            this.art += Random.Range(1, 3);
                        }

                        
                    }
                    break;
                case ProjectTask.Conference:
                    this.marketing += GetBoost((int) (this.art * 0.1f) + (int) (this.gameplay * 0.1f));
                    break;
                case ProjectTask.SocialMedia:
                    this.marketing += GetBoost((int) (this.art * 0.1f) + (int) (this.gameplay * 0.1f));
                    break;
                case ProjectTask.OnlineAds:
                    this.marketing += GetBoost((int) (this.art * 0.05f) + (int) (this.gameplay * 0.05f));
                    break;
            }
        }

        private int GetBoost(int i)
        {
            if (i <= 0)
            {
                return Random.Range(1, 5);
            }

            return i;
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
            this.tmpGameplay.text = $"{this.gameplay} / {this.skillsMax}";
            this.tmpReach.text = $"{this.marketing} / {this.skillsMax}";
        }


    }
}