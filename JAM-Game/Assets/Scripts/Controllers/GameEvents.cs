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
            this.Left.text = this.currentEvent.Left.ButtonDescription;
            this.Right.text = this.currentEvent.Right.ButtonDescription;

            if (!this.currentEvent.OnlyUseOnce)
            {
                this.eventsInPlay.Push(this.currentEvent);
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
            
            switch (option.TypeOfEvent)
            {
                case TypeOfEvent.Work: 
                    this.GameProject.WorkOnProject(option.WorkTask);
                    break;
                case TypeOfEvent.Play: 
                    //this.GameProject.WorkOnProject(option.PlayTask);
                    break;
            }

            CurrentText.text = option.SuccessText;
        }

        public void PushForwards()
        {
            ButtonPanel.SetActive(true);
            PushforwardPanel.SetActive(false);
            PullNewEvent();
        }
    }
}