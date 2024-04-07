using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class GameControllers : MonoBehaviour
{
    public GameProject GameProject;
    public GameEvents GameEvents;
    public PersonalSkills PersonalSkills;
    
    // Start is called before the first frame update
    void Start()
    {
        StartANewGame();
    }

    public void StartANewGame()
    {
        GameProject.Reset();
        GameEvents.PullNewEvent();
        PersonalSkills.UpdateUI();
    }
}
