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
    public GameObject wasdUI;
    public GameObject inventoryUI;
    public GameObject inventoryUI2;
    public GameObject pointAtInventory;

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
        if (player == null)
        {
            player = GameObject.Find("PlayerMC");

        }
        if (text == null)
        {
            //hilary find the text!!! 
        }
        switch (stepIndex)
        {

            case -1:


                break;

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
                        wasdUI.SetActive(true);
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
                if(wasdUI != null)
                {
                    wasdUI.SetActive(false);
                }
                else
                {
                   
                }
                if(text == null)
                {
                    text = GameObject.Find("Quest").transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                }
                text.text = "Check the storage.";
                arrow.gameObject.SetActive(true);
                arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();
                arrow.player = this.player;

                if(Vector3.Distance(player.transform.position , arrow.target.position ) < 5f)
                {
                    
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        arrow.gameObject.SetActive(false);
                        stepIndex++;
                    }
                    
                }
                // Perform running actions
            break;
            case 3:
                text.text = "Head to the Engine and add fuel";
                arrow.gameObject.SetActive(true);
                arrow.target = GameObject.Find("FuelEngine#(P)").transform;
                arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();
                if (Vector3.Distance(player.transform.position, arrow.target.position) < 5f)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        arrow.gameObject.SetActive(false);
                        stepIndex++;
                    }

                }
                // Perform jumping actions
                break;
            case 4:
                text.text = "You must continue moving..";
                arrow.gameObject.SetActive(true);
                arrow.target = GameObject.Find("Desk_low (1)").transform;
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        arrow.gameObject.SetActive(false);
                        stepIndex++;
                    }

                }
                // Perform attacking actions
                break;
            case 5:
                



                break;
        }

    }
    public void config()
    {
        pointAtInventory.SetActive(true);
        inventoryUI.SetActive(true);
        inventoryUI2.SetActive(true);
        stepIndex++;
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
