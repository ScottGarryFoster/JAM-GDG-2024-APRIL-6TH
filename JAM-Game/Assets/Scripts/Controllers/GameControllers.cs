using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class GameControllers : MonoBehaviour
{
    public GameEvents GameEvents;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.PullNewEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
