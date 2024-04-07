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
    public ReleaseGame ReleaseGame;
    
    // Start is called before the first frame update
    void Start()
    {
        StartANewGame();
    }

    public void StartANewGame()
    {
        ReleaseGame.Reset();
        DayTracker.Reset();
        GameProject.Reset();
        GameEvents.Reset();
        PersonalSkills.Reset();
    }
}
