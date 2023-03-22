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

    public string currentCartName;
    [SerializeField, BoxGroup("Train Info")] private TextMeshProUGUI roomName;
    [SerializeField, BoxGroup("Train Info")] private GameObject InfoDisplay;
    //[SerializeField] private GameObject TrainINfoGuide;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject mapCam;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject mapIcon;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject theMap;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject mapCore;
    [SerializeField, BoxGroup("InteractableMap")] private Vector3 mapFuelLocation;
    public bool PanelOn = false;
    [SerializeField, BoxGroup("FuelMachine")] private CanvasGroup FF_Panel; //the whole ui panel
    [SerializeField, BoxGroup("FuelMachine")] private Button FF_CloseButton, FF_AddButton;
    [SerializeField, BoxGroup("FuelMachine")] private GameObject FF_MachineLever;
    [SerializeField, BoxGroup("FuelMachine")] private GameObject TrainFuelBar; //this will also appear in map scene
    [SerializeField, BoxGroup("FuelMachine")] private Vector3 FuelMachineFuelLocation;

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
    private float currentValue, targetValue;
    [BoxGroup("TrainMoveStop"), ReadOnly] public string currentAccess;
    [BoxGroup("TrainMoveStop")] public GameObject door;
    [SerializeField,BoxGroup("TrainMoveStop")] private Animator doorAnim;
    [SerializeField,BoxGroup("TrainMoveStop")] private AudioSource doorAudio;
    [SerializeField,BoxGroup("TrainMoveStop")] private GameObject BGScroll;
    private int doorIsOpen = 0; //0 = close, 1 = open
    //private CinemachineBasicMultiChannelPerlin m_noise;
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
        UpdateCamNoise(currentValue);
        if(ISF.doorState==0){
            door.SetActive(false);
            doorAnim.SetTrigger("Close");  
        }else{
            door.SetActive(true);
            doorAnim.SetTrigger("Open");
        }
        
        BGScroll.SetActive(false);
        //bgscroll = BGScroll.GetComponent<Animator>();
        //currentTrainStatusMessage = "S T A R T  T R A I N";
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
            currentValue-=0.01f;
            UpdateCamNoise(currentValue);
        }else if(IsOn && currentValue <= targetValue){
            currentValue+=0.01f;
            UpdateCamNoise(currentValue);
        }
        // if(player!=null){
        //     if (player.GetComponent<PlayerInformation>().FuelAmt >= fuelCost)
        //     {
        //         //playerResource.GetComponent<PlayerInformation>().FuelAmt -= gm.GetComponent<Point>().fuelAmtNeeded;
        //         hasEnoughFuel = true;
        //     }else{
        //         hasEnoughFuel = false;
        //     }
        // }
        //Debug.Log(currentTrainStatusMessage);
    }
    public void TrainInforGuide(){
        TrainInfoGuide.SetActive(true);
    }
    public void DisplayCartName(){
        InfoDisplay.SetActive(false);
        InfoDisplay.SetActive(true);
    }
// The Map Part ---------
    public void DisplayMapIcons(){
        mapIcon.SetActive(false);
        mapIcon.SetActive(true);
    }
    public void Open_Map(){
        //
        mapIcon.SetActive(false);
        theMap.SetActive(true);
        
        Invoke("MapCamSwitch",.5f);
        Invoke("ChangePos",1f);
        TrainInfoGuide.SetActive(false);
    }
    void ChangePos(){
        Invoke("CoreAppear",1.5f);
        TrainFuelBar.GetComponent<RectTransform>().anchoredPosition = mapFuelLocation;
        TrainFuelBar.SetActive(true);
    }
    void CoreAppear(){
        mapCore.SetActive(true);
        MM.Reappear();
        MM.FFC();
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
    }
//Fuel Machine Part ---------
    public void Close_FF(){
        FF_Panel.interactable = false;
        FF_Panel.alpha = 0;
        FF_AddButton.gameObject.SetActive(false);
        TrainFuelBar.SetActive(false);
        //TrainInfoGuide.SetActive(false);
    }
    public void Open_FuelPanel(){
        PanelOn = true;
        // FF_Panel.SetActive(true);
        FF_Panel.interactable = true;
        FF_Panel.alpha = 1;
        FF_AddButton.gameObject.SetActive(true);
        Debug.Log("opened fuel panel");
        
        TrainFuelBar.GetComponent<RectTransform>().anchoredPosition = FuelMachineFuelLocation;
        TrainFuelBar.SetActive(true);
        TrainInfoGuide.SetActive(false);
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
        //yield return new WaitForSeconds(.5f);
        // if(IsOn){ //make the train stop
        //     Debug.Log("Train is gonna stop");
        //     leverAnim.SetTrigger("Off");
        //     currentAccess = "S T A R T  T R A I N";
        //     trainTrigger.guideDescript = "S T A R T  T R A I N";
        //     targetValue = 0f;
        //     //TrainStopMotion();
        //     IsOn = false;
                //IsMoving = true;
            //some enviromental change trigger: access to camera, plau audio, some foregrd backgrd
        // }else 
        if(!IsOn&&PickedLocation&&hasEnoughFuel){ //make the train move but need to pick a point
            //doorAnim.SetTrigger("Close");  //close when train started
            //Reduce num of fuel here
            ConsumeFuel();
            Debug.Log("Train is gonna move");
            leverAnim.SetTrigger("On");
            //change icon image
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
        //StartCoroutine("Pull");
        TrainInfoGuide.SetActive(false);
        CheckIfEnoughFuel();
        if(!hasEnoughFuel){
            WarningGuideCall(2); //nt enough fuel
        }
        if(!PickedLocation){
            WarningGuideCall(3); //picked location
        }
        ISF.ConfirmedPlayerTrainLocal = ISF.CurrentPlayerTrainInterval;
        MM.ResetFuelNeedDisplay();
        Invoke("Pull", .5f);
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
        //player do wtever
        yield return new WaitForSeconds(7f);
       //targetValue = 0f;
        //IsMoving = false;
        TrainStopMotion();
        WarningGuideCall(0);
        doorAnim.SetBool("Close", false);
        doorAnim.SetTrigger("Open");
        doorAudio.Play();
        BGScroll.SetActive(false);
        //the anim: moving of bg or foreground
        //object active
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
        WarningGuide.SetActive(false);
        WarningGuide.SetActive(true);
        WG.index = _index;
    }
    void CheckIfEnoughFuel(){
        if (player.GetComponent<PlayerInformation>().FuelAmt >= fuelCost && fuelCost!=0)
            {
                //playerResource.GetComponent<PlayerInformation>().FuelAmt -= gm.GetComponent<Point>().fuelAmtNeeded;
                hasEnoughFuel = true;
            }else{
                hasEnoughFuel = false;
        }
    }
}
//cannot move until fue is enough
//fuel reduce when train pull not when click location
