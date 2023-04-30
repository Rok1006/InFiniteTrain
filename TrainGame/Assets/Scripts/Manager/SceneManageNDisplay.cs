using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.InventoryEngine;
using NaughtyAttributes;
using Cinemachine;
//THis script is for function of pannels and triggering events
public class SceneManageNDisplay : MonoBehaviour
{
    private MapManager MM;
    private WarningGuide WG;
    [SerializeField, BoxGroup("REF")]private Info ISF;
    [BoxGroup("REF"), ReadOnly]public GameObject player;
    [SerializeField, BoxGroup("General")] private GameObject TrainInfoGuide;
    [SerializeField, BoxGroup("General")] private TextMeshProUGUI TrainInfoGuide_Text;
    [SerializeField, BoxGroup("General")] private GameObject WarningGuide;
    [SerializeField, BoxGroup("General")] private GameObject LocationInfoDisplay;
    [SerializeField, BoxGroup("General")] private TextMeshProUGUI LocationInfoDisplay_Text;

    public string currentCartName; //nt using
    [SerializeField, BoxGroup("Train Info")] private TextMeshProUGUI roomName; //nt using
    [SerializeField, BoxGroup("Train Info")] private GameObject InfoDisplay; //nt using
    //[SerializeField] private GameObject TrainINfoGuide;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject mapCam;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject mapIcon;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject theMap;
    [BoxGroup("InteractableMap")] public GameObject mapCore;
    [SerializeField, BoxGroup("InteractableMap")] private Vector3 mapFuelLocation;
    public bool PanelOn = false;
    [SerializeField, BoxGroup("FuelMachine")] private CanvasGroup FF_Panel; //the whole ui panel
    [SerializeField, BoxGroup("FuelMachine")] private Button FF_CloseButton, FF_AddButton;
    [SerializeField, BoxGroup("FuelMachine")] private GameObject FF_MachineLever;
    [SerializeField, BoxGroup("FuelMachine")] private GameObject TrainFuelBar; //this will also appear in map scene
    [SerializeField, BoxGroup("FuelMachine")] private Vector3 FuelMachineFuelLocation;

    [SerializeField, BoxGroup("DwskView")] private GameObject DeskViewPanel;

    [SerializeField, BoxGroup("TrainMoveStop")] private GameObject Lever; //still a placeholder
    Animator leverAnim;
    [SerializeField, BoxGroup("TrainMoveStop")] private bool IsOn; //train will move, else it stop
    [BoxGroup("TrainMoveStop")] public bool PickedLocation; //did player pick a location, after player come back frm mappt it will auto clear itself
    [BoxGroup("TrainMoveStop")] public bool hasEnoughFuel;  //check if there is enough fuel after start train
    [BoxGroup("TrainMoveStop")] public bool IsMoving; //train start moving, can be stop
    [BoxGroup("TrainMoveStop")] public int fuelCost;  //store the amt of score that need to pay, later add up the previous if skip
    [SerializeField,BoxGroup("TrainMoveStop")] private InteractableIcon trainTrigger;
    [SerializeField, BoxGroup("TrainMoveStop")] private List<GameObject> CMCam = new List<GameObject>();
    [SerializeField, BoxGroup("TrainMoveStop")] private float trainNoiseV;
    public float currentValue, targetValue;
    [BoxGroup("TrainMoveStop"), ReadOnly] public string currentAccess;
    [BoxGroup("TrainMoveStop")] public GameObject door;
    [SerializeField,BoxGroup("TrainMoveStop")] private Animator doorAnim;
    [SerializeField,BoxGroup("TrainMoveStop")] private AudioSource doorAudio;
    [SerializeField,BoxGroup("TrainMoveStop")] private GameObject BGScroll;
    private int doorIsOpen = 0; //0 = close, 1 = open
    [BoxGroup("UI/Others")]public GameObject GameOverScreen;
    [BoxGroup("UI/Others")]public Animator TrainWindowLight;
    [BoxGroup("UI/Others")]public CanvasGroup BackpackInventoryCanvasGroup;
  
//[HideInInspector]

    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("Mehnager").GetComponent<MapManager>();
        WG = this.GetComponent<WarningGuide>();
        ISF = GameObject.Find("GameManager").GetComponent<Info>();
        player = GameObject.FindGameObjectWithTag("Player");
        InfoDisplay.SetActive(false);
        mapIcon.SetActive(false);
        theMap.SetActive(false);
        mapCam.SetActive(false);
        mapCore.SetActive(false);
        TrainFuelBar.SetActive(false);
        // FF_Panel.SetActive(false);
        TrainInfoGuide.SetActive(false);
        WarningGuide.SetActive(false);
        IsOn = false;
        PickedLocation = false;
        hasEnoughFuel = false;
        IsMoving = false;
        leverAnim = Lever.GetComponent<Animator>();
        currentValue = 0; //set initial
        targetValue = 1.5f;
        GameOverScreen.SetActive(false);
        DeskViewPanel.SetActive(false);
        LocationInfoDisplay.SetActive(false);
      
//---------
        UpdateCamNoise(currentValue);
        if(ISF.doorState==0){
            door.SetActive(false);
            doorAnim.SetTrigger("Close");  
        }else{
            door.SetActive(true);
            doorAnim.SetTrigger("Open");
        }
        
        BGScroll.SetActive(false);
        DisplayLocationInfo("I N S I D E  T R A I N");
//Listener ---
        FF_CloseButton.onClick.AddListener(Close_FF);
    }
    private void LateUpdate() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        roomName.text = '"' + " " + currentCartName.ToString() + " " + '"';

        if(theMap.activeSelf&&PanelOn){
            if(Input.GetKeyUp(KeyCode.Space)){
                CloseMap();
                PanelOn = false;
            }
        }
        //Train Toggle
        if(!IsOn && currentValue >= targetValue){
            TrainWindowLight.SetTrigger("LightStop");
            currentValue-=0.01f;
            UpdateCamNoise(currentValue);
        }else if(IsOn && currentValue <= targetValue){
            TrainWindowLight.SetTrigger("LightMove");
            currentValue+=0.01f;
            UpdateCamNoise(currentValue);
        }
    }
    public void TrainInforGuide(){
        TrainInfoGuide.SetActive(true);
    }
    public void DisplayLocationInfo(string location){
        LocationInfoDisplay_Text.text = location.ToString();
        LocationInfoDisplay.SetActive(true);
    }
    public void DisplayCartName(){
        // InfoDisplay.SetActive(false);
        // InfoDisplay.SetActive(true);
    }
// The Map Part ---------
    public void DisplayMapIcons(){
        mapIcon.SetActive(false);
        mapIcon.SetActive(true);
    }
    public void Open_Map(){
        mapIcon.SetActive(false);
        theMap.SetActive(true);
        
        Invoke("MapCamSwitch",.5f);
        Invoke("ChangePos",.5f);
        TrainInfoGuide.SetActive(false);
        BackpackInventoryCanvasGroup.alpha = 0;
        BackpackInventoryCanvasGroup.interactable = false;
    }
    void ChangePos(){
        Invoke("CoreAppear",1f);
        TrainFuelBar.GetComponent<RectTransform>().anchoredPosition = mapFuelLocation;
        TrainFuelBar.SetActive(true);
    }
    void CoreAppear(){
        mapCore.SetActive(true);
        MM.FFC();
        MM.Reappear();
    }
    void MapCamSwitch(){
        PanelOn = true;
        mapCam.SetActive(true);
    }
    public void CloseMap(){
        theMap.SetActive(false);
        mapCam.SetActive(false);
        TrainFuelBar.SetActive(false);
        mapCore.SetActive(false);
        BackpackInventoryCanvasGroup.alpha = 1;
        BackpackInventoryCanvasGroup.interactable = true;
    }
//Fuel Machine Part ---------
    public void Close_FF(){
        FF_Panel.interactable = false;
        FF_Panel.alpha = 0;
        FF_AddButton.gameObject.SetActive(false);
        TrainFuelBar.SetActive(false);
        //player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("ActionFinished");
    }
    public void Open_FuelPanel(){
        PanelOn = true;
        // FF_Panel.SetActive(true);
        FF_Panel.interactable = true;
        FF_Panel.alpha = 1;
        FF_AddButton.gameObject.SetActive(true);
        Debug.Log("opened fuel panel");
        //player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("Think");
        
        TrainFuelBar.GetComponent<RectTransform>().anchoredPosition = FuelMachineFuelLocation;
        TrainFuelBar.SetActive(true);
        TrainInfoGuide.SetActive(false);
    }
//Item Craft View Desk
    public void Open_ViewDesk(){
        PanelOn = true;
        Debug.Log("opened Desk View");
        DeskViewPanel.SetActive(true);
        TrainInfoGuide.SetActive(false);
    }
    public void Close_ViewDesk(){
        DeskViewPanel.SetActive(false);
    }
//Train Move Stop Toggle --------------
    public void OnToggle(){
        // if(!IsOn){
        PanelOn = true; 
        TrainInfoGuide.SetActive(false);
        // }
    }
    public void OffToggle(){  //put this in actionCall
        TrainInfoGuide.SetActive(false);
    }
    void Pull(){
        if(!IsOn&&PickedLocation&&hasEnoughFuel){ //make the train move but need to pick a point
            ConsumeFuel();
            Debug.Log("Train is gonna move");
            leverAnim.SetTrigger("On");
            currentAccess = "T R A I N  M O V I N G";
            trainTrigger.guideDescript = "T R A I N  M O V I N G";
            targetValue = 1.5f;
            IsMoving = true;
            StartCoroutine(TrainStartMotion());
            IsOn = true;    
            //some enviromental change trigger: access to camera, plau audio
        }
    }
    public void PullLever(){  //put this in actionCall
        CheckIfEnoughFuel();
        player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("InsertFlip");
        if(ISF.CurrentSelectedPt==MM.ExitPtIndex&&CheckFinalRequiredItem()){  //if now player is in turn pt, abt to go back in loop //curent pt id of turn is 7
            //TrainTowardExit();
            //Then Trigger Dialogue
        }else if(ISF.CurrentSelectedPt==MM.ExitPtIndex&&!CheckFinalRequiredItem()){ //nt fulfilling requirment
            //ExitRequest.SetActive(true);
            //Then Trigger Dialogue
        }

        if(hasEnoughFuel){
            if(ISF.CurrentSelectedPt != ISF.ConfirmedSelectedPt && ISF.CurrentSelectedPt!=MM.ExitPtIndex){ //if player isnt already arrived 
            doorAnim.SetTrigger("Close"); 
            door.SetActive(false);
            doorIsOpen = 0;
            TrainInfoGuide.SetActive(false);
        
            if(!PickedLocation){
                WarningGuideCall(3); //picked location
            }
            Invoke("Pull", .5f);
            ISF.ConfirmedPlayerTrainLocal = ISF.CurrentPlayerTrainInterval;
            ISF.ConfirmedSelectedPt = ISF.CurrentSelectedPt;
            MM.points[ISF.CurrentSelectedPt].GetComponent<MapPopUp>().clicked = false; //when player pull lever and confirm, flag is plugged
            MM.UpdateMapPointState();
            MM.ResetFuelNeedDisplay();
            
            }else{
                WarningGuideCall(5);
            }
        }else{
            WarningGuideCall(2); //nt enough fuel
        }
        
    }
    void ConsumeFuel(){
        if (player.GetComponent<PlayerInformation>().FuelAmt >= fuelCost){
            player.GetComponent<PlayerInformation>().FuelAmt -= fuelCost;
        }
    }
    IEnumerator TrainStartMotion(){
        yield return new WaitForSeconds(0f);
        MM.PTMT(IsMoving, 2f);
        BGScroll.SetActive(true);
        Debug.Log("BDScroll");
        //player do wtever
        yield return new WaitForSeconds(7f);
       //targetValue = 0f;
        //IsMoving = false;
        TrainStopMotion();
        WarningGuideCall(0);
        doorAnim.SetBool("Close", false);
        doorAnim.SetTrigger("Open");
        doorIsOpen = 1;
        doorAudio.Play();
        BGScroll.SetActive(false);
            // if(ISF.ConfirmedSelectedPt==7){
            //     //pop up: reenter?
            // }
    }
    void TrainStopMotion(){  //When train arrive at the location, do this after player click pt and on train
        //yield return new WaitForSeconds(0f);
        if(IsOn){ //make the train stop
            Debug.Log("Train is gonna stop");
            leverAnim.SetTrigger("Off");
            currentAccess = "S T A R T  T R A I N";
            trainTrigger.guideDescript = "S T A R T  T R A I N";
            targetValue = 0f;
            door.SetActive(true);
            ISF.doorState = 1;
        }
        IsOn = false;
        IsMoving = false;
        CheckIfEnoughFuel();
        //yield return new WaitForSeconds(5f);
    }
    void UpdateCamNoise(float value){
        for(int i = 0; i < CMCam.Count; i++){
            //REf: https://stackoverflow.com/questions/66091697/how-to-access-cinemachine-basic-mutlichannel-perlin-noise
            //REf: https://docs.unity3d.com/Packages/com.unity.cinemachine@2.1/api/Cinemachine.CinemachineBasicMultiChannelPerlin.html
            CinemachineBasicMultiChannelPerlin m_noise = CMCam[i].GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
            m_noise.m_AmplitudeGain = value;  //0:not move; 1.5 -  move
            m_noise.m_FrequencyGain = value;
        }
    }
    public void WarningGuideCall(int _index){
        //WarningGuide.SetActive(false);
        WarningGuide.SetActive(true);
        WG.index = _index;
    }
    void CheckIfEnoughFuel(){
        if (player.GetComponent<PlayerInformation>().FuelAmt >= fuelCost )//&& fuelCost!=0
            {
                //playerResource.GetComponent<PlayerInformation>().FuelAmt -= gm.GetComponent<Point>().fuelAmtNeeded;
                hasEnoughFuel = true;
            }else{
                hasEnoughFuel = false;
        }
    }
//Check Re Entering --------
    public bool CheckFinalRequiredItem(){ //if player have these items , constantly checking
        //check
        return true;
    }
    public void ClickReEnter(){ //its either cant exit or u can exit
        MM.ReEnterLoop();
    }
    void TrainTowardExit(){
        doorAnim.SetTrigger("Close"); 
        door.SetActive(false);
        doorIsOpen = 0;
        TrainInfoGuide.SetActive(false);
        Invoke("Pull", .5f);
        ISF.ConfirmedPlayerTrainLocal = ISF.CurrentPlayerTrainInterval;
        ISF.ConfirmedSelectedPt = ISF.CurrentSelectedPt;
        MM.points[ISF.CurrentSelectedPt].GetComponent<MapPopUp>().clicked = false; //when player pull lever and confirm, flag is plugged
        MM.UpdateMapPointState();
        MM.ResetFuelNeedDisplay();
    }
}
