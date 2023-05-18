using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.InventoryEngine;
using NaughtyAttributes;
using Cinemachine;
using Yarn.Unity;
using MoreMountains.TopDownEngine;
using System.Linq;

//THis script is for function of pannels and triggering events
public class SceneManageNDisplay : MonoBehaviour
{
    private MapManager MM;
    private WarningGuide WG;
    [ReadOnly]public Scene_Sound SM;
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
    [SerializeField,BoxGroup("TrainMoveStop")] private AudioSource doorAudio, leverAudio, trainAudio;
    [SerializeField,BoxGroup("TrainMoveStop")] private GameObject BGScroll;
    private int doorIsOpen = 0; //0 = close, 1 = open
    [BoxGroup("UI/Others")]public GameObject GameOverScreen_PlayerCaught; //when boss catch u
    [BoxGroup("UI/Others")]public GameObject GameOverScreen_ExitMap;
    [BoxGroup("UI/Others")]public Animator EngineRoomCam;
    [BoxGroup("UI/Others")]public GameObject CutSceneObj;
    [BoxGroup("UI/Others")]public Animator TrainWindowLight;
    [BoxGroup("UI/Others")]public CanvasGroup BackpackInventoryCanvasGroup;

    [BoxGroup("Exit")] public List<string> inventoryNameToCheck;
    [BoxGroup("Exit")] public List<RequiredExitItems> requirements;
    public DialogueRunner dr;

    public GameObject TutorialObject;
    public GameObject ArrowObj;
    public GameObject SkipButton;
    public GameObject QuestDisplay;
  
    [System.Serializable]
    public class RequiredExitItems {
        public string itemRequiredName;
        public int quantity;
        [ReadOnly] public int currentValue = 0;
    }


    void Start()
    {
        MM = GameObject.FindGameObjectWithTag("Mehnager").GetComponent<MapManager>();
        WG = this.GetComponent<WarningGuide>();
        ISF = GameObject.Find("GameManager").GetComponent<Info>();
        SM = GameObject.Find("SceneSoundManager").GetComponent<Scene_Sound>();
        player = GameObject.FindGameObjectWithTag("Player");
        dr = GameObject.Find("Train Dialogue System").GetComponent<DialogueRunner>();
        TutorialObject = GameObject.Find("TUTORIAL");

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
        GameOverScreen_PlayerCaught.SetActive(false);
        GameOverScreen_ExitMap.SetActive(false);
        DeskViewPanel.SetActive(false);
        LocationInfoDisplay.SetActive(false);
        if(TutorialObject.activeSelf){
            SkipButton.SetActive(true);  
        }else{
            SkipButton.SetActive(false);
        }
        if(TutorialObject==null){
            ArrowObj.SetActive(false); 
            QuestDisplay.SetActive(false); 
        }
        //QuestDisplay.SetActive(true);
        
      
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
        CutSceneObj.SetActive(false);
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

        if(theMap.activeSelf&&PanelOn){ //Turn off map
            if(Input.GetKeyUp(KeyCode.Space)&&!dr.IsDialogueRunning){
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
    public void SkipTutorial(){
        if(TutorialObject!=null){
            TutorialObject.SetActive(false);
            ArrowObj.SetActive(false); 
            SkipButton.SetActive(false);
            QuestDisplay.SetActive(false);
            player.GetComponent<Character>().enabled = true;
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
#region Map Stuff
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
        SM.PlaySound("MapOpen");
        mapCore.SetActive(true);
        MM.FFC();
        MM.Reappear();
        //Boss FirstAppear
        if(Info.Instance.EnemyAppearState == 1){ //FirstAppear MM.enemyTrain.activeSelf&&
            Debug.Log("apear");
            MM.EnemyAppear();
            player.GetComponent<Character>().enabled = false; //turn back true in AfterFirstAppear in MM cs
        }
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
#endregion
#region FuelEngine
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
#endregion
#region ItemDesk
    public void Open_ViewDesk(){
        PanelOn = true;
        Debug.Log("opened Desk View");
        DeskViewPanel.SetActive(true);
        TrainInfoGuide.SetActive(false);
    }
    public void Close_ViewDesk(){
        DeskViewPanel.SetActive(false);
    }
#endregion
#region TrainStartStop
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
            //some enviromental change trigger: access to camera, play audio
            trainAudio.Play();
        }
    }
    public void PullLever(){  //put this in actionCall
        CheckIfEnoughFuel();
        player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("InsertFlip");
        if(!PickedLocation){
            WarningGuideCall(4); //picked location
        }else{//picked location
        }
        if(hasEnoughFuel){
            if(ISF.CurrentSelectedPt != ISF.ConfirmedSelectedPt){ //if player isnt already arrived  && ISF.CurrentSelectedPt!=MM.ExitPtIndex
            doorAnim.SetTrigger("Close"); 
            door.SetActive(false);
            doorIsOpen = 0;
            TrainInfoGuide.SetActive(false);
        
            Invoke("Pull", .25f);
            ISF.ConfirmedPlayerTrainLocal = ISF.CurrentPlayerTrainInterval;
            ISF.ConfirmedSelectedPt = ISF.CurrentSelectedPt;
            MM.points[ISF.CurrentSelectedPt].GetComponent<MapPopUp>().clicked = false; //when player pull lever and confirm, flag is plugged
            MM.UpdateMapPointState();
            MM.ResetFuelNeedDisplay();
            //play pull lever audio
            leverAudio.Play();

            }else{
                int ran = Random.Range(4,5);
                WarningGuideCall(ran);
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
        trainAudio.Stop();
        SM.PlaySound("TrainStop");
        WarningGuideCall(0);
        doorAnim.SetBool("Close", false);
        doorAnim.SetTrigger("Open");
        doorIsOpen = 1;
        doorAudio.Play();
        
        BGScroll.SetActive(false);
    }
    void TrainStopMotion(){  //When train arrive at the location, do this after player click pt and on train
        if(ISF.CurrentSelectedPt==MM.ExitPtIndex&&CheckFinalRequiredItem()){  //player can exit
            TriggerExitEnding();
            player.GetComponent<Character>().enabled = false;
        }else if(ISF.CurrentSelectedPt==MM.ExitPtIndex&&!CheckFinalRequiredItem()){ //nt fulfilling requirment, reenter loop
            TriggerPreReEnter();
            player.GetComponent<Character>().enabled = false;
        }
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
        //yield return new WaitForSeconds(5f); its still moving the train and map show too short
    }
#endregion
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
        foreach (string inventoryName in inventoryNameToCheck) {
            foreach (RequiredExitItems requiredExitItem in requirements) {
                requiredExitItem.currentValue += Inventory.FindInventory(inventoryName, "Player1").InventoryContains(requiredExitItem.itemRequiredName).Count;
            }
        }
        
        foreach (RequiredExitItems requiredExitItem in requirements) {
            if (requiredExitItem.currentValue < requiredExitItem.quantity) {
                return false;
            }
        }
        return true;
    }
    void TrainTowardReenterPt(){ //not calling
        doorAnim.SetTrigger("Close"); 
        door.SetActive(false);
        doorIsOpen = 0;
        TrainInfoGuide.SetActive(false);
        Invoke("Pull", .5f);
        ISF.ConfirmedPlayerTrainLocal = MM.PlayerReenterIndex;
        ISF.CurrentSelectedPt = MM.PlayerReenterIndex;
        ISF.ConfirmedSelectedPt = MM.PlayerReenterIndex;
        ISF.CurrentPlayerTrainInterval = MM.PlayerReenterIndex;
        ISF.pointID = MM.PlayerReenterIndex;
        //ISF.ConfirmedSelectedPt = ISF.CurrentSelectedPt;
        MM.points[ISF.CurrentSelectedPt].GetComponent<MapPopUp>().clicked = false; //when player pull lever and confirm, flag is plugged
        MM.UpdateMapPointState();
        MM.ResetFuelNeedDisplay();
    }
    public void TriggerPreReEnter(){
        StartCoroutine(PreReEnter());
    }
    IEnumerator PreReEnter(){ 
        yield return new WaitForSeconds(0f);
        CutSceneObj.SetActive(false);
        CutSceneObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        dr.onDialogueComplete.AddListener(ReEnter);
        dr.StartDialogue("ExitFail");
    }
    public void ReEnter(){ //its either cant exit or u can exit
        StartCoroutine(ReEnterSequence());
    }
    IEnumerator ReEnterSequence(){ 
        yield return new WaitForSeconds(0f);
        Open_Map();
        yield return new WaitForSeconds(3f);
        MM.playerTrain.GetComponent<Animator>().SetTrigger("Reenter");//this not playing
        yield return new WaitForSeconds(3f);
        CloseMap();
        MM.ReEnterLoop(); //reopen the map pts
        TrainTowardReenterPt();
        CutSceneObj.GetComponent<Animator>().SetTrigger("Out");
        player.GetComponent<Character>().enabled = true;
    }
    
#region Ending Sequences
//Boss Caught Ending 1 ------------
    public void TriggerGameOver(){
        StartCoroutine(GameOverFlow());
    }
    IEnumerator GameOverFlow(){ //This will be called in Map manager when dead counter elapsed
        yield return new WaitForSeconds(0f);
        CutSceneObj.SetActive(false);
        CutSceneObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        EngineRoomCam.SetTrigger("BossArrive");
        dr.onDialogueComplete.AddListener(AfterBossCaught);
        dr.StartDialogue("BossCaught");
    }
    void AfterBossCaught(){
        CutSceneObj.GetComponent<Animator>().SetTrigger("Out");
        GameOverScreen_PlayerCaught.SetActive(true);
    }
    public void GameOverRestart(){
        //REload the game or whatever
    }
//Exit Map: Ending 2 ------------
    public void TriggerExitEnding(){
        StartCoroutine(Ending2Flow());
    }
    IEnumerator Ending2Flow(){ //This will be called in Map manager when dead counter elapsed
        yield return new WaitForSeconds(0f);
        CutSceneObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        // EngineRoomCam.SetTrigger("BossArrive");
        dr.onDialogueComplete.AddListener(Ending2);
        dr.StartDialogue("ExitSuccess");
    }
    void Ending2(){
        CutSceneObj.GetComponent<Animator>().SetTrigger("Out");
        GameOverScreen_ExitMap.SetActive(true);
    }
#endregion
}
