using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;
using MoreMountains.TopDownEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField, BoxGroup("REF")]private SceneManageNDisplay SMD;
    public static int gameState = 0;
    [BoxGroup("REF")]public GameObject player;
    [BoxGroup("REF")]public GameObject playerResource;
    [BoxGroup("REF")]public GameObject playerTrain;
    Animator playerTrainAnim;
    [SerializeField,BoxGroup("REF")]private GameObject triggerDoorToOutside;

    [BoxGroup("Enemy")]public GameObject enemyTrain;
    Animator enemyTrainAnim;
    [SerializeField,BoxGroup("Enemy")]private GameObject DeadlyTimer;
    [SerializeField,BoxGroup("Enemy")]private TextMeshProUGUI timeCountDown;
    [SerializeField,BoxGroup("Enemy")]private int DeadCounterTime;
    [SerializeField, BoxGroup("Others")] private GameObject Corner_deadlyTimer;
    [SerializeField, BoxGroup("Others")] private TextMeshProUGUI Corner_timeCountDown;

    [SerializeField,BoxGroup("MAP")]private Animator MapFrame;
    [SerializeField,BoxGroup("MAP")]private Animator MapCam;
    [SerializeField,BoxGroup("MAP")]private Animator MapCore;

    [BoxGroup("Status")]public bool playerTurn = false;
    [BoxGroup("Status")]public bool enemyTurn = false;
    [BoxGroup("Status")]public int id;
    [BoxGroup("Status")]public int confirmedPlayerTrainLocal = 0;

    [BoxGroup("PointInfo")]public GameObject[] points;
    [BoxGroup("PointInfo")]public List<GameObject> availableDestination = new List<GameObject>();
    
    [BoxGroup("PointInfo")]public List<GameObject> PopUpPoint = new List<GameObject>();
    [SerializeField, BoxGroup("PointInfo")] List<GameObject> Intervals = new List<GameObject>();

    [SerializeField, BoxGroup("Stuff")] TextMeshProUGUI requireText; 
    [BoxGroup("Stuff")] public int PlayerReenterIndex; 
    [BoxGroup("Stuff")] public int TurnPtIndex;  //Exit is now the turn point
    [BoxGroup("Stuff")] public int ExitPtIndex;
    [BoxGroup("Stuff")] public int BossTrainAppearTriggerIndex;

    private int sum;
    void Awake() {
        //UpdateTrainLocation();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Singleton.Instance == null)
            Debug.Log("singlton is null");
        id = Singleton.Instance.id;
        SMD = GameObject.Find("SceneManage&Interactions").GetComponent<SceneManageNDisplay>();
        playerTrainAnim = playerTrain.GetComponent<Animator>();
        enemyTrainAnim = enemyTrain.GetComponent<Animator>();
//Check and initializtion------
        UpdatePlayerIcon();
        UpdatePlayer();
        Invoke("UpdateTrainLocation", 1f);
        //UpdateTrainLocation(); //update player and enemy location on map
        if(Info.Instance.ConfirmedSelectedPt!=0){
            requireText.text = "Select a new location.";
        }else{
            requireText.text = "Select a location.";
        }
        // if(Info.Instance.ConfirmedSelectedPt!=TurnPtIndex){  //now in turn pt
        //     UpdateMapPointState();
        // }
        if(Info.Instance.EnemyAppearState==1&& Info.Instance.ConfirmedSelectedPt>=BossTrainAppearTriggerIndex){  //now in turn pt
            enemyTrain.SetActive(true);
        }else{
            enemyTrain.SetActive(false);
        }
        DeadlyTimer.SetActive(false);
        Corner_deadlyTimer.SetActive(false);
    }

    void Update()
    {
        //UpdateTrainLocation();
        Info.Instance.pointID = Info.Instance.ConfirmedSelectedPt;
        if(gameState == 0)
        {
            playerTurn = true;
        }
        else
        {
            playerTurn = false;
        }
        if(gameState == 1)
        {
            enemyTurn = true;
        }

        if (enemyTurn == true)
        {
            foreach (GameObject x in points)
            {
                if(x.GetComponent<Point>().isEnemy == true)
                {
                    x.GetComponent<Point>().MoveEnemy();
                }
            }
            enemyTurn = false;
            gameState = 2;
            Debug.Log("can pick again");
        }

        if(Info.Instance.EnemyAppearState == 1 && Info.Instance.ConfirmedSelectedPt==BossTrainAppearTriggerIndex){  //& after player come back
            enemyTrain.SetActive(true); //it appeared
        }

//Everytime after appear
        if(SMD.mapCore.activeSelf&&enemyTrain.activeSelf&&Info.Instance.EnemyAppearState > 1){ //FirstAppear
            //Enemy train bounce
            //enemyTrainAnim.SetTrigger("Moved");
        }
        if(Info.Instance.CurrentEnemyTrainInterval!= Info.Instance.ConfirmedEnemyTrainLocal){
            EnemyProceed();
        }
        if(Info.Instance.CurrentSelectedPt!=0&&Info.Instance.CurrentSelectedPt != Info.Instance.ConfirmedSelectedPt){
            MapInformationNotice(sum, SMD.player.GetComponent<PlayerInformation>().FuelAmt);
        }else{//requireText.text = "Select a new location.";
        }
        if(Info.Instance.EnemyAppearState>0&& Info.Instance.ConfirmedSelectedPt>=BossTrainAppearTriggerIndex){  //now in turn pt
            enemyTrain.SetActive(true);
        }else{
            enemyTrain.SetActive(false);
        }
        
//If Enmy is one unit away frm player
        if(Info.Instance.EnemyAppearState == 2 && Info.Instance.ConfirmedEnemyTrainLocal!= 0 && Info.Instance.ConfirmedEnemyTrainLocal == Info.Instance.ConfirmedPlayerTrainLocal-1){
            Info.Instance.DeadCountDownStart = true;
            //Info.Instance.DeadTime = DeadCounterTime;
            DeadlyTimer.SetActive(true);
            //Info.Instance.DeadTime = 120; //2 min
        }else if(Info.Instance.ConfirmedEnemyTrainLocal != Info.Instance.ConfirmedPlayerTrainLocal-1){ //once player moved
            Info.Instance.DeadCountDownStart = false;
            Info.Instance.DeadTime = DeadCounterTime; //reset
            DeadlyTimer.SetActive(false);
        }
        // timeCountDown.text = Info.Instance.DeadTime.ToString();
        EnemyDeadlyCountDownDisplay(timeCountDown, Info.Instance.DeadTime);
        EnemyDeadlyCountDownDisplay(Corner_timeCountDown, Info.Instance.DeadTime);
        if(Info.Instance.DeadCountDownStart){
            Debug.Log("Start dead count");
            Corner_deadlyTimer.SetActive(true);
            if(Info.Instance.DeadTime>0){
                Info.Instance.DeadTime-=Time.deltaTime;
            }else{
                //Info.Instance.DeadTime+=DeadCounterTime;
                SMD.TriggerGameOver();
                Info.Instance.DeadCountDownStart = false;
            }
        }else{
            Corner_deadlyTimer.SetActive(false);
        }
    }
    private void FixedUpdate() {
        
    }
    public bool AvailableToMove(GameObject gm)   //By Joon
    {
        playerResource = GameObject.FindGameObjectWithTag("Player");
        var points = player.GetComponent<Point>();
        for (int i = 0; i < points.connectedPoints.Length ; i++)
        {
            Debug.Log(playerResource.GetComponent<PlayerInformation>().FuelAmt);
            if (points.connectedPoints[i].Equals(gm)){
                if (playerResource.GetComponent<PlayerInformation>().FuelAmt >= gm.GetComponent<Point>().fuelAmtNeeded)
                {
                    playerResource.GetComponent<PlayerInformation>().FuelAmt -= gm.GetComponent<Point>().fuelAmtNeeded;
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsAvailableToMove(GameObject gm) //Changed accord to new map system By Rok
    {
        playerResource = GameObject.FindGameObjectWithTag("Player");
        //var point = gm.GetComponent<Point>();
        // for (int i = 0; i < points.connectedPoints.Length ; i++)
        // {
            Debug.Log(playerResource.GetComponent<PlayerInformation>().FuelAmt);
            // if (points.connectedPoints[i].Equals(gm)){
            if (playerResource.GetComponent<PlayerInformation>().FuelAmt >= gm.GetComponent<Point>().fuelAmtNeeded)
                {
                    //playerResource.GetComponent<PlayerInformation>().FuelAmt -= gm.GetComponent<Point>().fuelAmtNeeded;
                    return true;
                }
        //     }
        // }
        return false;
    }
    public void UpdatePlayer()
    {
        availableDestination.Clear();
        for(int i = 0; i < points.Length; i++)
        {
            if(points[i].GetComponent<Point>().isPlayer == true)
            {
                Debug.Log("fuckkkkkk");
                player = points[i];
            }
        }
        for(int i = 0; i < player.GetComponent<Point>().connectedPoints.Length; i++)
        {
            availableDestination.Add(player.GetComponent<Point>().connectedPoints[i]);
        }
        
    }
    public void UpdatePlayerIcon()
    {
        for(int i = 0; i < points.Length; i++)
        {
            points[i].GetComponent<Point>().isPlayer = false;
            if ( points[i].GetComponent<Point>().id == Info.Instance.pointID)
            {
                Debug.Log("df");
                player = points[i];
                points[i].GetComponent<Point>().isPlayer = true;
                //PopUpPoint.Add(points[i]);
            }
        }
    }
    public void FFC(){
        if (this.player != null && this.player.GetComponent<Point>().id != 0) 
        {
            Debug.Log("ddf");
            this.player.gameObject.GetComponent<MapPopUp>().ForceChange();
        }
    }
    public void Reappear(){
        if(this.player != null){
            player.gameObject.GetComponent<MapPopUp>().ReapperaFlagPt();
        }
    }
    void UpdateTrainLocation(){ //For instant update when come back from map pt scene
        float pX = Intervals[Info.Instance.ConfirmedPlayerTrainLocal].transform.localPosition.x;
        float pY = Intervals[Info.Instance.ConfirmedPlayerTrainLocal].transform.localPosition.y;
        float eX = Intervals[Info.Instance.CurrentEnemyTrainInterval].transform.localPosition.x;
        float eY = Intervals[Info.Instance.CurrentEnemyTrainInterval].transform.localPosition.y;
        playerTrain.transform.localPosition = new Vector3(pX,pY,0);  //set theit location
        enemyTrain.transform.localPosition = new Vector3(eX, eY, 0);
        Debug.Log("update train loc");
        UpdateMapPointState();
    }
    void EnemyMove(int ToPoint){ Info.Instance.CurrentEnemyTrainInterval = ToPoint;} //put the num of destination pt
    public void PlayerMove(int ToPoint){ Info.Instance.CurrentPlayerTrainInterval = ToPoint;} //insert the currentSetected Pt
    
    IEnumerator PlayerTrainMoveTowards(bool isMoving, float speed){ //this will be trigger to move player train when ever the currentplayerinterval changes
        //bool isMoving = true;
        GameObject targetPos = Intervals[Info.Instance.ConfirmedPlayerTrainLocal];
        while (playerTrain.transform.localPosition != targetPos.transform.localPosition)
       {
           playerTrain.transform.localPosition = Vector2.MoveTowards(playerTrain.transform.localPosition, targetPos.transform.localPosition, speed * Time.deltaTime);
           yield return null;
       }
       isMoving = false;
       yield return new WaitUntil(() => !isMoving);
       ///StartCoroutine(PlayerTrainMoveTowards());
    }
    
    public void PTMT(bool isMoving, float speed){ //Player Train interval
        StartCoroutine(PlayerTrainMoveTowards(isMoving,speed));
    }
    
    public void GetTotalFuelNeeded(int index){
        sum = 0;
        int currentLocal = Info.Instance.ConfirmedPlayerTrainLocal;
        for(int i = currentLocal+1; i<index+1;i++){
            sum += points[i].GetComponent<Point>().fuelAmtNeeded;
        }
        MapInformationNotice(sum, SMD.player.GetComponent<PlayerInformation>().FuelAmt);
        //requireText.text = "Insert " + sum.ToString() + " FUEL at the Fuel Depo. You currently have " + SMD.player.GetComponent<PlayerInformation>().FuelAmt;
        SMD.fuelCost = sum;
    }
    
    public void UpdateMapPointState(){
        for(int i = 1; i < Info.Instance.ConfirmedSelectedPt+1;i++){ //excluse start pt
            points[i].GetComponent<MapPopUp>().blocked = true;
            var image = points[i].GetComponent<MapPopUp>().HeadIcon.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = .2f;
            points[i].GetComponent<MapPopUp>().HeadIcon.color = tempColor;
            //other effect or sprite changes
        }
    }
//REENTERING------
    public void ReEnterLoop(){ //call when player in turn pt confirm to reenter
        for(int i = PlayerReenterIndex; i < points.Length;i++){
            points[i].GetComponent<MapPopUp>().blocked = false;
            var image = points[i].GetComponent<MapPopUp>().HeadIcon.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a =  1f;
            points[i].GetComponent<MapPopUp>().HeadIcon.color = tempColor;
        }

    }

    void MapInformationNotice(int fuelCost, int fuelOwn){ //front put the sum num //SMD.player.GetComponent<PlayerInformation>().FuelAmt
        int diff = fuelCost - fuelOwn;
        if(diff>0){
            diff = diff;
        }else{
            diff = 0;
        }
        requireText.text = "Insert " + diff.ToString() + " more FUEL at the Fuel Depo. You currently have " + fuelOwn;
    }
    public void ResetFuelNeedDisplay(){
        requireText.text = "Select a new location.";
    }

//Enemy Monitoring Part------------------------
/* 
- Enemy only start moving when player arrived at thrid point after come back
- Enemy everytime player get kick pack it will proceed one point forward, no more than that
*/
    IEnumerator EnemyTrainMoveTowards(float speed){ //movement for enemy train
        bool isMoving = true;
        GameObject targetPos = Intervals[Info.Instance.CurrentEnemyTrainInterval];
        while (enemyTrain.transform.localPosition != targetPos.transform.localPosition)
       {
           enemyTrain.transform.localPosition = Vector2.MoveTowards(enemyTrain.transform.localPosition, targetPos.transform.localPosition, speed * Time.deltaTime);
           yield return null;
       }
       isMoving = false;
       Info.Instance.ConfirmedEnemyTrainLocal = Info.Instance.CurrentEnemyTrainInterval;
       yield return new WaitUntil(() => !isMoving);
    }
    public void ETMT(float speed){ //Enemy Train interval: it will move based on the Current enemy interval
        StartCoroutine(EnemyTrainMoveTowards(speed));
    }
    public void EnemyProceed(){ //enemy proceed one point, trigger this when this need to be proceeded
        if(Info.Instance.EnemyAppearState>0){
            ETMT(.5f);
            enemyTrainAnim.SetTrigger("Moved");
        }
        //trigger some snimation to let player know he is here some pop up box when he come back
    }
    public void EnemyAppear(){
        StartCoroutine(EnemyFirstAppear()); 
    }
    IEnumerator EnemyFirstAppear(){
        SMD.CutSceneObj.SetActive(true);
        //player.GetComponent<Character>().enabled = false;
        yield return new WaitForSeconds(1f);
        enemyTrainAnim.SetTrigger("Appear");
        SMD.SM.PlaySound("Ba_JumpHit");
        yield return new WaitForSeconds(.3f);
        SMD.SM.PlaySound("Ba_GrdSmash");
        MapFrame.SetTrigger("Shake");
        MapCam.SetTrigger("Shake");
        MapCore.SetTrigger("Shake");
        yield return new WaitForSeconds(.3f);
        SMD.dr.onDialogueComplete.AddListener(AfterFirstAppear);
        SMD.dr.StartDialogue("BossAppear");
    }
    void AfterFirstAppear(){
        SMD.CutSceneObj.GetComponent<Animator>().SetTrigger("Out");
        SMD.player.GetComponent<Character>().enabled = true;
        Info.Instance.EnemyAppearState = 2 ;
    }
    public void EnemyDeadlyCountDownDisplay(TextMeshProUGUI txt, float displayTime){//If enemy is one unit away frm player
        if(displayTime<0){
            displayTime = 0;
        }
        float minutes = Mathf.FloorToInt(displayTime/60);
        float seconds = Mathf.FloorToInt(displayTime%60);
        txt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
//order:
//u select a point on the map, u go pull the lever, train start moving and arrive in a while
//if enemy moved compare to last time, make it so that it will bounce when player open map