using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;
//This script is for handling tutorial, attached on scene obj, calling frm GAme manager INFO
public class Tutorial : MonoBehaviour
{
    public enum GameState { Tutorial, TutorialEnd }
    public GameState currentState;
    public int stepIndex;
    private GameObject player;
    public TextMeshProUGUI text;
    public static Tutorial instance;
    public TargetIndicator arrow;
    public GameObject UIShit;
    public InventoryItemPlus carrot;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
        arrow.player = player;
        arrow.gameObject.SetActive(false);
       
        
       
    }

    void Update()
    {
        if(text == null)
        {
            //hilary find the text!!! 
        }
        switch (stepIndex)
        {
            case 0:
                //Player woke up
                //player radiation reduce, screen effect come up
                //dialouge box appear: I must have fainted....lower radiation level...
                if(player == null)
                {
                    player = GameObject.Find("PlayerMC");

                }
                else
                {

                     text.text = "Lower your radiation level by consuming a food.";
                    player.GetComponent<Character>().enabled = false;
                    player.GetComponent<CharacterInventory>().MainInventory.AddItemAt(carrot, 1, 0);

                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        //when we start the level player spanws with 1 carrot or food in inventory position 1
                        stepIndex++;
                        player.GetComponent<Character>().enabled = true;
                    }
                }
               
                
            break;
            case 1: //Going back to train
                text.text = "Find your way back to the train";
                //im checking this from Train Trigger Area. you don't need to do anything here

            break;
            case 2:
                text.text = "Check the storage.";
                arrow.gameObject.SetActive(true);
               
                arrow.target = GameObject.Find("Shelf3").transform;
                arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();


                // Perform running actions
            break;
            case 3:
                text.text = "Head to the Engine and add fuel";
               
                arrow.target = GameObject.Find("FuelEngine#(P)").transform;
                arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shelf" && this.stepIndex == 2)
        {
            stepIndex++;
        }
    }

    public void OnButtonPress()
    {

    }
}
