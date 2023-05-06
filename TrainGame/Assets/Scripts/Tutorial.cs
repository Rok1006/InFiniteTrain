using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
//This script is for handling tutorial, attached on scene obj, calling frm GAme manager INFO
public class Tutorial : MonoBehaviour
{
    public enum GameState { Tutorial, TutorialEnd }
    public GameState currentState;
    public GameObject informationUI;
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
    public Animator radioAnim;
    public MPTSceneManager msm;
    public DialogueRunner dr;
    private bool gameStarted = false;
    private int state = 0;
    public ResourceBox rb;
    public GameObject fuelbutton;
    public GameObject shelfUI;
    public GameObject deskUI;
    public GameObject craftUI;
    public bool dialoguePlayed = false;
    private float initialDelay = 2.5f;
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

        radioAnim.Play("ScreenEffect_Bad");

        if (MPTSceneManager.state == 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("PlayerMC");
            if(stepIndex == -1)
            {
                player.GetComponent<PlayerManager>().MCFrontAnim.Play("Special/Faint");
            }

        }
        if(gameStarted == false)
        {
            player.GetComponent<Character>().enabled = false;
        }
        
        if (MPTSceneManager.state == 1)
        {
            this.gameObject.SetActive(false);
        }
        initialDelay -= Time.deltaTime;
        if(initialDelay < 0)
        {

          
            if (text == null)
            {
                //hilary find the text!!! 
            }
            switch (stepIndex)
            {

                case -1:
                    if(dr == null)
                    {
                        player.GetComponent<PlayerManager>().MCFrontAnim.Play("Special/FaintWake");
                    }
                    
                    dr = GameObject.Find("Train Dialogue System").GetComponent<DialogueRunner>();
                    dr.onDialogueComplete.AddListener(DialogueConfig);
                    if (dialoguePlayed == false)
                    {

                        dr.StartDialogue("test2");
                        player.GetComponent<Character>().enabled = false;


                    }


                    break;

                case 0:
                    //Player woke up
                    //player radiation reduce, screen effect come up
                    //dialouge box appear: I must have fainted....lower radiation level...
                  
                    if (player == null)
                    {
                        player = GameObject.Find("PlayerMC");

                    }
                    else
                    {
                        UIShit.SetActive(true);
                        informationUI.SetActive(true);
                         text.text = "Lower your radiation level by consuming a food.";
                        player.GetComponent<Character>().enabled = false;
                        if (FindObjectOfType<BackpackInventoryUI>().InventoryDisplay != null)
                        {
                           rb.backpackInventoryDisplay = FindObjectOfType<BackpackInventoryUI>().InventoryDisplay;

                        }
                        player.GetComponent<CharacterInventory>().MainInventory.AddItemAt(carrot, 2, 0);

                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            informationUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Return to the train";
                            pointAtInventory.SetActive(false);
                            wasdUI.SetActive(true);
                            //when we start the level player spanws with 1 carrot or food in inventory position 1
                            stepIndex++;
                            player.GetComponent<Character>().enabled = true;
                            dialoguePlayed = false;
                        }
                    }
               
                
                break;
                case 1: //Going back to train
                    
                    text.text = "Find your way back to the train";

                    //im checking this from Train Trigger Area. you don't need to do anything here

                break;
                case 2:
                    if(dr == null)
                    {
                        dr = GameObject.Find("Train Dialogue System").GetComponent<DialogueRunner>();
                        shelfUI = ItemHolder.instance.shelf;
                        fuelbutton = ItemHolder.instance.fuel;
                        deskUI = ItemHolder.instance.desk;
                        
                    }
                   
                    if(dialoguePlayed == false)
                    {
                        dr.onDialogueComplete.AddListener(DialogueConfig);
                        dr.StartDialogue("test3");
                        player.GetComponent<Character>().enabled = false;
                    

                    }
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
                    arrow.target = GameObject.Find("ShelfParent").transform;

                    if(shelfUI.activeSelf == true)
                    {
                    
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            arrow.target = null;
                            arrow.gameObject.SetActive(false);
                            stepIndex++;
                            dialoguePlayed = false;
                            
                            
                        }
                    
                    }
                    // Perform running actions
                break;
                case 3:
                    
                   
                    if (dialoguePlayed == false)
                    {
                        dr.onDialogueComplete.AddListener(DialogueConfig);
                        dr.StartDialogue("test4");
                        player.GetComponent<Character>().enabled = false;



                    }
                    text.text = "Head to the Engine and add fuel";
                    arrow.gameObject.SetActive(true);
                    if(arrow.target == null)
                    {

                    arrow.target = GameObject.Find("FuelDepo").transform;
                    }
                    arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();
                    if (fuelbutton.activeSelf == true)
                    {
                        
                            arrow.target = null;
                            arrow.gameObject.SetActive(false);
                            stepIndex++;
                        

                    }
                    // Perform jumping actions
                    break;
                case 4:
                   
                    if (dialoguePlayed == false)
                    {
                        dr.onDialogueComplete.AddListener(DialogueConfig);
                        dr.StartDialogue("test5");



                    }
                    text.text = "Choose your next destination on the map.";
                    arrow.gameObject.SetActive(true);
                    if(arrow.target == null)
                    {

                    arrow.target = GameObject.Find("Desk_low (1)").transform;
                    }
                    if (deskUI.activeSelf == true)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            arrow.target = null;
                            arrow.gameObject.SetActive(false);
                            stepIndex++;
                        }

                    }
                    // Perform attacking actions
                    break;
                case 5:
                   

                    if (dialoguePlayed == false)
                    {
                        dr.onDialogueComplete.AddListener(DialogueConfig);
                        dr.StartDialogue("test6");



                    }
                    text.text = "Start the train";
                    
                    
                    if (Vector3.Distance(arrow.target.position , player.transform.position) < 7)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            arrow.gameObject.SetActive(false);
                            stepIndex++;
                            dialoguePlayed = false;
                            text.text = "Once the train stops, head out for some scavenging!";
                        }

                    }


                    // MPTSceneManager.state = 1;


                    break;
                case 6:
                    if(SceneManager.GetActiveScene().name == "MapPoint")
                    {
                        if(dr == null)
                        {
                         dr = GameObject.Find("Train Dialogue System").GetComponent<DialogueRunner>();
                         

                        }
                        if (text == null)
                        {
                            text = GameObject.Find("Quest").transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                        }
                        if (dialoguePlayed == false)
                        {
                            dr.onDialogueComplete.AddListener(DialogueConfig);
                            dr.StartDialogue("test7");



                        }
                        text.text = "Scavenge for resources";
                    }
                    break;
                case 7:
                    if (dr == null)
                    {
                        dr = GameObject.Find("Train Dialogue System").GetComponent<DialogueRunner>();
                        craftUI = ItemHolder.instance.craft;

                    }

                    if (dialoguePlayed == false)
                    {
                        dr.onDialogueComplete.AddListener(DialogueConfig);
                        dr.StartDialogue("test3");
                        player.GetComponent<Character>().enabled = false;


                    }
                    if (text == null)
                    {
                        text = GameObject.Find("Quest").transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                    }
                    text.text = "Use the crafting recipe to make useful items";

                    break;



                    

            }
        }

    }
    public void DialogueConfig()
    {
        dialoguePlayed = true;
        player.GetComponent<Character>().enabled = true;
        if(gameStarted == false)
        {
            gameStarted = true;
        }
        if(stepIndex == 3)
        {
            
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
