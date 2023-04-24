using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is for handling tutorial, attached on scene obj, calling frm GAme manager INFO
public class Tutorial : MonoBehaviour
{
    public enum GameState { Tutorial, TutorialEnd }
    public GameState currentState;
    public int stepIndex;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("PlayerMC");
    }

    void Update()
    {
        switch (stepIndex)
        {
            case 0: //Player woke up
                //player radiation reduce, screen effect come up
                //dialouge box appear: I must have fainted....lower radiation level...
                //after dialogue: quest 1 display: go back to the train
                //player pick up the scatter items around him: could be fuel
            break;
            case 1: //player prepare to leave
                //walking ui show nect to player
                //arrows appear to points toward the exit
            break;
            case 2:
                // Perform running actions
            break;
            case 3:
                // Perform jumping actions
            break;
            case 4:
                // Perform attacking actions
            break;
        }

    }
    public void NextStep(int step)
    {
        stepIndex = step;
    }
}
