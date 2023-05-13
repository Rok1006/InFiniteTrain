using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using MoreMountains.TopDownEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using Yarn.Unity;

//script for box, timer, minigame
public class ResourceBox : MonoBehaviour
{
    [SerializeField,BoxGroup("TDE")] private string playerID, invnetoryName;
    private SideInventoryDisplay sideInventoryDisplay;
    
    [SerializeField, BoxGroup("TDE")] private bool autoSelectInventoryDisplay = true;
    [SerializeField, BoxGroup("TDE")] private CanvasGroup inventoryCanvas;
    [SerializeField, BoxGroup("TDE")] private InventoryDisplay inventoryDisplay;
    [SerializeField, BoxGroup("UI")] private Image radicalBar;
    [SerializeField, BoxGroup("UI")] private GameObject timer;
    [SerializeField, BoxGroup("UI")] private GameObject metalIcon, materialIcon;
    [SerializeField, BoxGroup("Logic")] private bool isLocked = true;
    [SerializeField, BoxGroup("Logic"), ShowIf("isLocked")] private float openBoxSpeed = 0.35f;
    [SerializeField, BoxGroup("Logic")] private bool generateItemOnlyOnce = false;
    [SerializeField, BoxGroup("Invetory")] private List<InventoryItem> itemsToGenerate;
    [SerializeField, BoxGroup("Inventory")] private Info info;
    [SerializeField, BoxGroup("Box")] private Animator boxAnim;
    [SerializeField] GameObject miniGame;
    private bool isOpening = false, isPlayerNear = false, opened = false;

    [SerializeField, BoxGroup("EFFECT")] private GameObject boxStun;
    [SerializeField, BoxGroup("EFFECT")] private GameObject boxOpenEffect;

    [SerializeField] private bool useSkeletonMecanim = true;
    [SerializeField] private SkeletonMecanim B_Skin;
    [SpineSkin] public string[] boxLook = { "Normal", "Wood"};
    public DialogueRunner dr;

    public InventoryDisplay backpackInventoryDisplay;

    //getters & setters
    public string InventoryName {get=>invnetoryName; set=>invnetoryName=value;}
    public GameObject MetalIcon {get=>metalIcon;}
    public GameObject MaterialIcon {get=>materialIcon;}
    public bool IsLocked {get=>isLocked;}
    private bool restart = false;

    public virtual void Start()
    {
        //set up
        if(FindObjectOfType<BackpackInventoryUI>().InventoryDisplay != null)
        {
        backpackInventoryDisplay = FindObjectOfType<BackpackInventoryUI>().InventoryDisplay;

        }
        if (backpackInventoryDisplay == null)
            Debug.Log("Can't find backpack inventory display for " + name);

        if (useSkeletonMecanim) {
            B_Skin = this.transform.GetChild(0).gameObject.GetComponent<SkeletonMecanim>();     
            B_Skin.skeleton.SetSkin(boxLook[Random.Range(0,boxLook.Length)]);
        }

        if (sideInventoryDisplay == null)
            sideInventoryDisplay = FindObjectOfType<SideInventoryDisplay>();
        if (inventoryCanvas == null)
            inventoryCanvas = sideInventoryDisplay.DisplayCanvasGroup;
            
        //-----------------------
        //auto select side inventory display
        if (autoSelectInventoryDisplay) {
            sideInventoryDisplay = FindObjectOfType<SideInventoryDisplay>();
            
            if (sideInventoryDisplay == null)
                Debug.LogWarning("Cannot find side inventory display");
            
                inventoryCanvas = sideInventoryDisplay.DisplayCanvasGroup;
                inventoryDisplay = sideInventoryDisplay.InventoryDisplay;
        }

        //generate inventory items into inventory if there's any in the list
        if (generateItemOnlyOnce) {
            Invoke("FirstTimeGenerateItemsIntoBox", 0.1f);
        } else {
            GenerateItemsIntoBox();
        }


        if (inventoryDisplay == null)
            Debug.LogWarning("Cannot find inventory display");
        
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        
        //things happens when loading scene
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (boxStun != null){boxStun.SetActive(false);}
        if (boxOpenEffect != null){boxOpenEffect.SetActive(false);};
        dr = GameObject.Find("Train Dialogue System").GetComponent<DialogueRunner>();
    }

    public void FirstTimeGenerateItemsIntoBox() {
        if (Info.Instance.isNewGame) {
            foreach(InventoryItem item in itemsToGenerate) {
                GetComponentInChildren<Inventory>().AddItem(item, 1);
            }
        }
    }

    public void GenerateItemsIntoBox() {
        foreach(InventoryItem item in itemsToGenerate) {
                GetComponentInChildren<Inventory>().AddItem(item, 1);
            }
    }
    
    public virtual void Update()
    {
        //making sure there's reference
        if (autoSelectInventoryDisplay && sideInventoryDisplay == null) {
            sideInventoryDisplay = FindObjectOfType<SideInventoryDisplay>();
            
            if (sideInventoryDisplay == null)
                //Debug.LogWarning("Cannot find side inventory display");
                if(sideInventoryDisplay != null)
                {

                    if(sideInventoryDisplay.DisplayCanvasGroup != null)
                    {
                        inventoryCanvas = sideInventoryDisplay.DisplayCanvasGroup;
                        inventoryDisplay = sideInventoryDisplay.InventoryDisplay;
                    }
                }
               
           
            
        }

        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space) && !isOpening) { //open inventory
            isOpening = true;
            var enemies = Physics.OverlapSphere(transform.position, 30);
            foreach(var colliders in enemies)
            {
                if (colliders.gameObject.GetComponent<ForceUpdate>() != null)
                {
                    colliders.gameObject.GetComponent<ForceUpdate>().stun = true;
                }
               
            }
            
        } else if (isPlayerNear && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) && opened) { //close inventory
            HideInventoryUI();
        }

        /*if (isLocked) { //if player need to open the lock
            if (isOpening)
                radicalBar.fillAmount = Mathf.Min(radicalBar.fillAmount + openBoxSpeed * Time.deltaTime, 1.0f);
                if (boxAnim != null)
                    boxAnim.SetTrigger("opening");
            if (radicalBar.fillAmount >= 1 && isOpening) {
                inventoryDisplay.ChangeTargetInventory(invnetoryName);
                inventoryCanvas.alpha = 1;
                inventoryCanvas.interactable = true;
                inventoryCanvas.blocksRaycasts = true;
                if (boxAnim != null)
                    boxAnim.SetTrigger("open");
            }
        } else { //player are free to open it
            if (isOpening) {
                inventoryDisplay.ChangeTargetInventory(invnetoryName);
                inventoryCanvas.alpha = 1;
                inventoryCanvas.interactable = true;
                inventoryCanvas.blocksRaycasts = true;
                if (boxAnim != null)
                    boxAnim.SetTrigger("open");
            }
        }*/

        if (isLocked) { //if player need to open the lock
            if (isOpening && miniGame.GetComponent<LockPickBarV2>().Complete == false){
                Invoke("DisplayMiniGame", .7f);
                if (boxStun != null){boxStun.SetActive(true);}
                boxAnim.SetTrigger("opening");
                //miniGame.gameObject.GetComponent<CanvasScaler>().scaleFactor = Mathf.Lerp(0.01f, 1f, 0.01f);
            }
            if (miniGame.GetComponent<LockPickBarV2>().Complete && isOpening && !opened) { //the box is ready to open
                
                miniGame.GetComponent<Animator>().SetTrigger("complete");
                boxAnim.SetTrigger("open");
                if (boxOpenEffect != null){boxOpenEffect.SetActive(true);};
                //add sparkles after open
                Invoke("CloseMiniGame", 2f);
                ShowInventoryUI();
            }
        } else { //player are free to open it
            if (isOpening && !opened) {
                ShowInventoryUI();
            }
        }

    }
    void DisplayMiniGame(){
        //miniGame.GetComponent<LockPickBarV2>().enabled = true;
        miniGame.GetComponent<LockPickBarV2>().InGame = true;
        miniGame.gameObject.SetActive(true);
        if(!restart){
            miniGame.GetComponent<LockPickBarV2>().Move();
            restart = true;
        }
        // miniGame.GetComponent<LockPickBarV2>().enabled = false;
        // miniGame.GetComponent<LockPickBarV2>().enabled = true;
        // //make it only do once
    }
    void CloseMiniGame(){
        miniGame.gameObject.SetActive(false);
        if (boxStun != null){boxStun.SetActive(false);}
        restart = false;
    }

    /// when enter trigger area
    /// set inventory display target's name to this resrouce box's name
    /// set inventory canvas to active
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == ("Player")) {
            isPlayerNear = true;
            
            if (isLocked) {
                timer.SetActive(true);
            }
             
            if (miniGame != null) 
                miniGame.GetComponent<LockPickBarV2>().InZone = true;
        }
    }

    /// when exit trigger area
    /// set inventory display target's name to empty
    /// set inventory canvas to inactive
    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag ==("Player")) {
            HideInventoryUI();
            isPlayerNear = false;
            if (miniGame != null) {
                miniGame.GetComponent<LockPickBarV2>().InZone = false;
                if(miniGame.GetComponent<LockPickBarV2>().InGame){
                    boxAnim.SetTrigger("leave");
                    miniGame.GetComponent<Animator>().SetTrigger("complete");
                    miniGame.GetComponent<LockPickBarV2>().ResetMiniGame();
                    miniGame.GetComponent<LockPickBarV2>().InGame = false;
                    Invoke("CloseMiniGame", 2f); 
                }
            }
        }
    }

    public void HideInventoryUI() {
        if(!dr.IsDialogueRunning){
        if (isLocked)
            timer.SetActive(false);
        isOpening = false;
        inventoryCanvas.alpha = 0;
        inventoryCanvas.interactable = false;
        inventoryCanvas.blocksRaycasts = false;
        opened = false;
        Info.Instance.IsViewingInventory = false;
        GameObject.Find("BakcpackDisplay").GetComponent<InventoryDisplay>().SlotContainer[0].Select();

        //let backpack's "target inventory" to be null
        backpackInventoryDisplay.NextInventory = null;
        }
    }

    public void ShowInventoryUI() {
        inventoryDisplay.ChangeTargetInventory(InventoryName);
        if (inventoryCanvas.GetComponentInChildren<InventoryDetails>() != null)
            inventoryCanvas.GetComponentInChildren<InventoryDetails>().TargetInventoryName = InventoryName;
        inventoryCanvas.alpha = 1;
        inventoryCanvas.interactable = true;
        inventoryCanvas.blocksRaycasts = true;
        if (boxAnim != null)
            boxAnim.SetTrigger("open");
        opened = true;
        Info.Instance.IsViewingInventory = true;

        //let backpack's "target inventory" to be this one, so player can double click to move items to this inventory
        backpackInventoryDisplay.NextInventory = this.inventoryDisplay;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    }

    
}
