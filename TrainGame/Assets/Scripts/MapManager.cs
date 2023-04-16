using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    [SerializeField, BoxGroup("REF")]private Info InfoSC;
    [SerializeField, BoxGroup("REF")]private SceneManageNDisplay SMD;
    public static int gameState = 0;
    [BoxGroup("REF")]public GameObject player;
    [BoxGroup("REF")]public GameObject playerResource;
    [SerializeField,BoxGroup("REF")]private GameObject playerTrain;
    [SerializeField,BoxGroup("REF")]private GameObject enemyTrain;
    [SerializeField,BoxGroup("REF")]private GameObject triggerDoorToOutside;

    [BoxGroup("Status")]public bool playerTurn = false;
    [BoxGroup("Status")]public bool enemyTurn = false;
    [BoxGroup("Status")]public int id;
    [BoxGroup("Status")]public int confirmedPlayerTrainLocal = 0;

    [BoxGroup("PointInfo")]public GameObject[] points;
    [BoxGroup("PointInfo")]public List<GameObject> availableDestination = new List<GameObject>();
    
    [BoxGroup("PointInfo")]public List<GameObject> PopUpPoint = new List<GameObject>();
    [SerializeField, BoxGroup("PointInfo")] List<GameObject> Intervals = new List<GameObject>();

    [SerializeField, BoxGroup("Stuff")] TextMeshProUGUI requireText; 
    [BoxGroup("Stuff")] public int TurnPtIndex; 
    [BoxGroup("Stuff")] public int ExitPtIndex;
    [BoxGroup("Stuff")] public int BossTrainAppearTriggerIndex;

    private int sum;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Singleton.Instance == null)
            Debug.Log("singlton is null");
        id = Singleton.Instance.id;
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
        SMD = GameObject.Find("SceneManage&Interactions").GetComponent<SceneManageNDisplay>();
        UpdatePlayerIcon();
        UpdatePlayer();
        UpdateTrainLocation(); //player and enemy
        if(InfoSC.ConfirmedSelectedPt!=0){
            requireText.text = "Select a new location.";
        }else{
            requireText.text = "Select a location.";
        }
        if(InfoSC.ConfirmedSelectedPt!=TurnPtIndex){  //now in turn pt
            UpdateMapPointState();
        }
        if(InfoSC.EnemyAppearState==1&& InfoSC.ConfirmedSelectedPt>=BossTrainAppearTriggerIndex){  //now in turn pt
            enemyTrain.SetActive(true);
        }else{
            enemyTrain.SetActive(false);
        }
    }

    void Update()
    {
        InfoSC.pointID = InfoSC.ConfirmedSelectedPt;
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

        if(InfoSC.EnemyAppearState == 1 && InfoSC.ConfirmedSelectedPt==BossTrainAppearTriggerIndex){  //& after player come back
            enemyTrain.SetActive(true); //it appeared
            //do some visual anim stuff to obviously tell player boss is here
        }
        if(InfoSC.ConfirmedEnemyTrainLocal!= 0 && InfoSC.ConfirmedEnemyTrainLocal == InfoSC.ConfirmedPlayerTrainLocal){ //IF boss train in the same position as player
            //GAME OVER
            SMD.GameOverScreen.SetActive(true);
        }
        if(InfoSC.CurrentEnemyTrainInterval!= InfoSC.ConfirmedEnemyTrainLocal){
            EnemyProceed();
        }
        if(InfoSC.CurrentSelectedPt!=0&&InfoSC.CurrentSelectedPt != InfoSC.ConfirmedSelectedPt){
            MapInformationNotice(sum, SMD.player.GetComponent<PlayerInformation>().FuelAmt);
        }else{
            //requireText.text = "Select a new location.";
        }
        if(InfoSC.EnemyAppearState==1&& InfoSC.ConfirmedSelectedPt>=BossTrainAppearTriggerIndex){  //now in turn pt
            enemyTrain.SetActive(true);
        }else{
            enemyTrain.SetActive(false);
        }
        // if(Input.GetKeyDown(KeyCode.M)){ //Testing
        //     EnemyProceed();
        // }
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
            if ( points[i].GetComponent<Point>().id == InfoSC.pointID)
            {
                Debug.Log("df");
                player = points[i];
                points[i].GetComponent<Point>().isPlayer = true;
                //PopUpPoint.Add(points[i]);
            }
        }
        // if (this.player != null && this.player.GetComponent<Point>().id != 0) 
        // {
        //     Debug.Log("ddf");
        //     player.gameObject.GetComponent<MapPopUp>().ForceChange();
        // }
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
        float pX = Intervals[InfoSC.ConfirmedPlayerTrainLocal].transform.localPosition.x;
        float pY = Intervals[InfoSC.ConfirmedPlayerTrainLocal].transform.localPosition.y;
        float eX = Intervals[InfoSC.CurrentEnemyTrainInterval].transform.localPosition.x;
        float eY = Intervals[InfoSC.CurrentEnemyTrainInterval].transform.localPosition.y;
        playerTrain.transform.localPosition = new Vector3(pX,pY,0);  //set theit location
        enemyTrain.transform.localPosition = new Vector3(eX, eY, 0);
    }
    void EnemyMove(int ToPoint){ InfoSC.CurrentEnemyTrainInterval = ToPoint;} //put the num of destination pt
    public void PlayerMove(int ToPoint){ InfoSC.CurrentPlayerTrainInterval = ToPoint;} //insert the currentSetected Pt
    
    IEnumerator PlayerTrainMoveTowards(bool isMoving, float speed){ //this will be trigger to move player train when ever the currentplayerinterval changes
        //bool isMoving = true;
        GameObject targetPos = Intervals[InfoSC.ConfirmedPlayerTrainLocal];
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
        int currentLocal = InfoSC.ConfirmedPlayerTrainLocal;
        for(int i = currentLocal+1; i<index+1;i++){
            sum += points[i].GetComponent<Point>().fuelAmtNeeded;
        }
        MapInformationNotice(sum, SMD.player.GetComponent<PlayerInformation>().FuelAmt);
        //requireText.text = "Insert " + sum.ToString() + " FUEL at the Fuel Depo. You currently have " + SMD.player.GetComponent<PlayerInformation>().FuelAmt;
        SMD.fuelCost = sum;
    }
    
    public void UpdateMapPointState(){
        for(int i = 1; i < InfoSC.ConfirmedSelectedPt+1;i++){ //excluse start pt
            points[i].GetComponent<MapPopUp>().blocked = true;
            var image = points[i].GetComponent<MapPopUp>().HeadIcon.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = .2f;
            points[i].GetComponent<MapPopUp>().HeadIcon.color = tempColor;
            //other effect or sprite changes
        }
    }
    public void ReEnterLoop(){ //call when player in turn pt confirm to reenter
        for(int i = 3; i < points.Length;i++){
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
        GameObject targetPos = Intervals[InfoSC.CurrentEnemyTrainInterval];
        while (enemyTrain.transform.localPosition != targetPos.transform.localPosition)
       {
           enemyTrain.transform.localPosition = Vector2.MoveTowards(enemyTrain.transform.localPosition, targetPos.transform.localPosition, speed * Time.deltaTime);
           yield return null;
       }
       isMoving = false;
       InfoSC.ConfirmedEnemyTrainLocal = InfoSC.CurrentEnemyTrainInterval;
       yield return new WaitUntil(() => !isMoving);
    }
    public void ETMT(float speed){ //Enemy Train interval: it will move based on the Current enemy interval
        StartCoroutine(EnemyTrainMoveTowards(speed));
    }
    public void EnemyProceed(){ //enemy proceed one point, trigger this when this need to be proceeded
        if(InfoSC.EnemyAppearState==1){
            ETMT(.5f);
            //have some animation bounce or sth to notify that enemy moved when open map
        }
        //trigger some snimation to let player know he is here some pop up box when he come back
    }
}
//order:
//u select a point on the map, u go pull the lever, train start moving and arrive in a while