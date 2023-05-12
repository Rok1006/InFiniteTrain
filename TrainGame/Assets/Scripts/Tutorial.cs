using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.TopDownEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//This script is for handling tutorial, attached on scene obj, calling frm GAme manager INFO
public class Tutorial : MonoBehaviour
{
    public enum GameState { Tutorial, TutorialEnd }
    public GameState currentState;
    public GameObject informationUI;
    public int stepIndex;
    public GameObject player;
    public TextMeshProUGUI text;
    public static Tutorial instance;
    public TargetIndicator arrow;
    public GameObject UIShit;
    public InventoryItemPlus herb;
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
    public GameObject mealButton;
    public GameObject[] tutorialArrow;
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
        if (text == null)
        {
            ItemHolder.instance.text.GetComponent<TextMeshProUGUI>();
        }

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
                player.GetComponent<CharacterInventory>().MainInventory.AddItemAt(herb, 1, 0);
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
                        

                        if (player.GetComponent<CharacterInventory>().MainInventory.InventoryContains("Herb").Count == 0)
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
                    if(arrow.gameObject != null)    
                    {
                        arrow.gameObject.SetActive(true);
                        arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();
                        arrow.player = this.player;
                        arrow.target = GameObject.Find("ShelfParent").transform;
                    }
                  

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

                     arrow.target = GameObject.Find("Controls_Low").transform;
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
                    if(SceneManager.GetActiveScene().name == "LeoPlayAround")
                    {
                        arrow.gameObject.SetActive(false);
                        text.text = "Once the train stops, head out for some scavenging!";
                    }
                   
                    if (SceneManager.GetActiveScene().name == "MapPoint")
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
                            player.GetComponent<Character>().enabled = false;


                        }
                        text.text = "Scavenge for resources";
                    }
                    break;
                case 7:
                   
                    if (dr == null)
                    {
                        dr = GameObject.Find("Train Dialogue System").GetComponent<DialogueRunner>();
                        craftUI = ItemHolder.instance.craft;
                        mealButton = ItemHolder.instance.mealButton;
                        tutorialArrow = ItemHolder.instance.tutorialArrow;
                        mealButton.GetComponent<Button>().onClick.AddListener(ChangeGuideArrow);

                    }

                    if (dialoguePlayed == false)
                    {
                        dr.onDialogueComplete.AddListener(DialogueConfig);
                        dr.StartDialogue("test8");
                        player.GetComponent<Character>().enabled = false;


                    }
                    if (text == null)
                    {
                        text = GameObject.Find("Quest").transform.GetChild(2).GetComponent<TextMeshProUGUI>();
                    }
                    text.text = "Use the crafting recipe to make useful items";
                    arrow.gameObject.SetActive(true);
                    
                    if (arrow.target == null)
                    {

                        arrow.target = ItemHolder.instance.desk2.transform;
                        arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();
                        arrow.player = player;
                    }
                    if(craftUI == null)
                    {
                        craftUI = ItemHolder.instance.craft;
                    }
                    if (craftUI.activeSelf == true)
                    {
                       
                            arrow.target = null;
                            arrow.gameObject.SetActive(false);
                            tutorialArrow[0].SetActive(true);


                    }

                    break;
                case 8:
                    tutorialArrow[0].SetActive(false);
                    //tutorialArrow[1].SetActive(false);
                    //tutorialArrow[1].SetActive(false);
                    text.text = "Grab ingredients for the meal";
                    arrow.gameObject.SetActive(true);
                    if (arrow.target == null)
                    {

                        arrow.target = ItemHolder.instance.shelf2.transform;
                        arrow.uiObject = arrow.gameObject.GetComponent<RectTransform>();
                        arrow.player = player;
                    }
                    tutorialArrow[2].SetActive(true);
                    tutorialArrow[3].SetActive(true);

                    if ( player.GetComponent<CharacterInventory>().MainInventory.InventoryContains("Onion").Count != 0){

                        if(player.GetComponent<CharacterInventory>().MainInventory.InventoryContains("Carrot").Count != 0)
                        {
                            stepIndex++;
                        }

                    }


                    break;
                case 9:
                    text.text = "Craft a delicious meal";
                    
                    arrow.gameObject.SetActive(false);
                   // tutorialArrow[4].SetActive(true);
                   // tutorialArrow[5].SetActive(true);
                    tutorialArrow[2].SetActive(false);
                    tutorialArrow[3].SetActive(false);
                    
                    if (player.GetComponent<CharacterInventory>().MainInventory.InventoryContains("Soup").Count != 0)
                    {
                        ItemHolder.instance.final.SetActive(true);

                        Destroy(this.gameObject);
                        tutorialArrow[4].SetActive(false);
                        tutorialArrow[5].SetActive(false);
                        
                    }
                    

                    break;




                    

            }
        }

    }
    public void ChangeGuideArrow()
    {
      
        tutorialArrow[0].SetActive(false);
        tutorialArrow[1].SetActive(true);
        if(stepIndex == 7)
        {
            stepIndex++;
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
