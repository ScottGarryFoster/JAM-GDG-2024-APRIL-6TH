﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class GameProject : MonoBehaviour
    {


        [Header("Stats")]
        public int art;
        
        public int gameplay;
        
        public int marketing;
        
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
        
        public int bugs;
        
        [Header("OTHER")]
        public PersonalSkills PersonalSkills;
        
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

        public void WorkOnProject(ProjectTask task, EffectivenessOfTask effectivenessOfTask = EffectivenessOfTask.Effective)
        {
            ExecuteBugs(task, effectivenessOfTask);
            
            MainTasks(task, effectivenessOfTask);

            ClampAllStats();
            
            UpdateUI();

            TakeEnergy(effectivenessOfTask);
        }

        private void TakeEnergy(EffectivenessOfTask effectivenessOfTask)
        {
            int energy = 0;
            switch (effectivenessOfTask)
            {
                case EffectivenessOfTask.Ineffective: energy = Random.Range(2, 9); break;
                case EffectivenessOfTask.Effective: energy = Random.Range(1, 5); break;
                case EffectivenessOfTask.CriticalHit: energy = Random.Range(0, 3); break;
            }

            PersonalSkills.TakeEnergy(energy);
        }

        private void ClampAllStats()
        {
            this.art = this.art < this.skillsMax ? this.art : this.skillsMax;
            this.gameplay = this.gameplay < this.skillsMax ? this.gameplay : this.skillsMax;
            this.marketing = this.marketing < this.skillsMax ? this.marketing : this.skillsMax;
            this.hiddenTools = this.hiddenTools < this.skillsMax ? this.hiddenTools : this.skillsMax;
            this.hiddenGameplay = this.hiddenGameplay < this.skillsMax ? this.hiddenGameplay : this.skillsMax;
            this.bugs = this.bugs < this.skillsMax ? this.bugs : this.skillsMax;
        }

        private void MainTasks(ProjectTask task, EffectivenessOfTask effectivenessOfTask)
        {
            FromZeroAdd(task, effectivenessOfTask);

            int boost = 0;
            switch (task)
            {
                case ProjectTask.Environment:
                    this.art += GetBoost((int) (this.art * 0.1f), effectivenessOfTask);
                    break;
                case ProjectTask.Shaders:
                    this.art += GetBoost((int) (this.art * 0.1f), effectivenessOfTask);
                    break;
                case ProjectTask.Models:
                    this.art += GetBoost((int) (this.art * 0.1f), effectivenessOfTask);
                    break;
                case ProjectTask.Gameplay:
                    if (this.hiddenTools > 4 && this.gameplay > 15)
                    {
                        this.gameplay += GetBoost((int) (this.gameplay * 0.2f), effectivenessOfTask);
                    }
                    else
                    {
                        this.gameplay += GetBoost((int) (this.gameplay * 0.1f), effectivenessOfTask);
                    }
                    break;
                case ProjectTask.Tools:
                    this.hiddenTools += Random.Range(0, 2);
                    if (this.hiddenTools > 5)
                    {
                        if (this.gameplay > 10)
                        {
                            this.gameplay += GetBoost((int) (this.gameplay * 0.1f), effectivenessOfTask);
                        }
                        else
                        {
                            this.gameplay += GetBoost(Random.Range(1, 3), effectivenessOfTask);
                        }

                        if (this.art > 10)
                        {
                            this.art += GetBoost((int) (this.art * 0.1f), effectivenessOfTask);
                        }
                        else
                        {
                            this.art += GetBoost(Random.Range(1, 3), effectivenessOfTask);
                        }
                    }
                    else
                    {
                        if (this.gameplay > 15)
                        {
                            this.gameplay += GetBoost((int) (this.gameplay * 0.05f), effectivenessOfTask);
                        }
                        else
                        {
                            this.gameplay += GetBoost(Random.Range(1, 3), effectivenessOfTask);
                        }
                        
                        if (this.art > 15)
                        {
                            this.art += GetBoost((int) (this.art * 0.05f), effectivenessOfTask);
                        }
                        else
                        {
                            this.art += GetBoost(Random.Range(1, 3), effectivenessOfTask);
                        }

                        
                    }
                    break;
                case ProjectTask.Conference:
                    this.marketing += GetBoost((int) (this.art * 0.1f) + (int) (this.gameplay * 0.1f), effectivenessOfTask);
                    break;
                case ProjectTask.SocialMedia:
                    this.marketing += GetBoost((int) (this.art * 0.1f) + (int) (this.gameplay * 0.1f), effectivenessOfTask);
                    break;
                case ProjectTask.OnlineAds:
                    this.marketing += GetBoost((int) (this.art * 0.05f) + (int) (this.gameplay * 0.05f), effectivenessOfTask);
                    break;
            }

            if (this.bugs > 75)
            {
                this.gameplay -= Random.Range(0, 5);
                this.art -= Random.Range(0, 5);
            }
            else if (this.bugs > 50)
            {
                this.gameplay -= Random.Range(0, 3);
                this.art -= Random.Range(0, 3);
            }
        }

        private void FromZeroAdd(ProjectTask task, EffectivenessOfTask effectivenessOfTask)
        {
            switch (task)
            {
                case ProjectTask.Environment:
                case ProjectTask.Shaders:
                case ProjectTask.Models:
                case ProjectTask.Tools:
                    if (this.art == 0)
                    {
                        switch (effectivenessOfTask)
                        {
                            case EffectivenessOfTask.Ineffective: this.art += Random.Range(1, 3); break;
                            case EffectivenessOfTask.Effective: this.art += Random.Range(1, 5); break;
                            case EffectivenessOfTask.CriticalHit: this.art += Random.Range(5, 10); break;
                        }
                    }

                    break;
            }

            switch (task)
            {
                case ProjectTask.Gameplay:
                case ProjectTask.Tools:
                    if (this.gameplay == 0)
                    {
                        switch (effectivenessOfTask)
                        {
                            case EffectivenessOfTask.Ineffective: this.gameplay += Random.Range(1, 3); break;
                            case EffectivenessOfTask.Effective: this.gameplay += Random.Range(1, 5); break;
                            case EffectivenessOfTask.CriticalHit: this.gameplay += Random.Range(5, 10); break;
                        }
                    }

                    break;
            }
        }

        private int GetBoost(int i, EffectivenessOfTask effectivenessOfTask)
        {
            int newValue = i;
            if (i <= 0)
            {
                switch (effectivenessOfTask)
                {
                    case EffectivenessOfTask.Ineffective: newValue = Random.Range(1, 3); break;
                    case EffectivenessOfTask.Effective: newValue =  Random.Range(1, 5); break;
                    case EffectivenessOfTask.CriticalHit: newValue =  Random.Range(5, 10); break;
                }
            }

            switch (effectivenessOfTask)
            {
                case EffectivenessOfTask.Ineffective: newValue =  i / 2; break;
                case EffectivenessOfTask.Effective: newValue =  i; break;
                case EffectivenessOfTask.CriticalHit: newValue =  i * 2; break;
            }

            if (newValue <= 0)
            {
                newValue = Random.Range(1, 3);
            }
            
            return newValue;
        }

        private void ExecuteBugs(ProjectTask task, EffectivenessOfTask effectivenessOfTask)
        {
            int changeOfBugs = 0;
            switch (task)
            {
                case ProjectTask.Gameplay:
                case ProjectTask.Tools:
                    changeOfBugs += Random.Range(1, 7);
                    break;
                case ProjectTask.Environment:
                case ProjectTask.Shaders:
                    changeOfBugs += Random.Range(1, 10);
                    break;
            }
            
            switch (effectivenessOfTask)
            {
                case EffectivenessOfTask.Ineffective: changeOfBugs *= 2; break;
                case EffectivenessOfTask.Effective: break;
                case EffectivenessOfTask.CriticalHit: changeOfBugs -= (int)(changeOfBugs * 0.5f); break;
            }
            this.bugs += bugs;
            

            changeOfBugs = 0;
            switch (task)
            {
                case ProjectTask.Bugs:
                    changeOfBugs = Random.Range(20, 30);
                    break;
            }
            
            switch (effectivenessOfTask)
            {
                case EffectivenessOfTask.Ineffective: changeOfBugs /= 2; break;
                case EffectivenessOfTask.Effective: break;
                case EffectivenessOfTask.CriticalHit: changeOfBugs *= 2; break;
            }
            this.bugs -= bugs;
            
            if (bugs < 0)
            {
                bugs = 0;
            }
        }

        public void UpdateUI()
        {
            ClampAllStats();
            this.scrollbarLooks.size = IHateStatics.GetProgressBarValue(this.art, this.skillsMax);
            this.scrollbarGameplay.size = IHateStatics.GetProgressBarValue(this.gameplay, this.skillsMax);
            this.scrollbarReach.size = IHateStatics.GetProgressBarValue(this.marketing, this.skillsMax);


            this.tmpLooks.text = $"{this.art} / {this.skillsMax}";
            this.tmpGameplay.text = $"{this.gameplay} / {this.skillsMax}";
            this.tmpReach.text = $"{this.marketing} / {this.skillsMax}";
        }

        public void AddGameplayBoost(int amount)
        {
            this.gameplay += amount;
            ClampAllStats();
            this.UpdateUI();
        }
        
        public void AddReachBoost(int amount)
        {
            this.marketing += amount;
            ClampAllStats();
            this.UpdateUI();
        }
    }
}