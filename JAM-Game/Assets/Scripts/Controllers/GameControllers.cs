using System.Collections;
using System.Collections.Generic;
using Controllers;
using DefaultNamespace;
using UnityEngine;

public class GameControllers : MonoBehaviour
{
    public GameProject GameProject;
    public GameEvents GameEvents;
    public PersonalSkills PersonalSkills;
    public DayTracker DayTracker;
    
    // Start is called before the first frame update
    void Start()
    {
        StartANewGame();
    }

    public void StartANewGame()
    {
        DayTracker.Reset();
        GameProject.Reset();
        GameEvents.PullNewEvent();
        PersonalSkills.UpdateUI();
    }
}
