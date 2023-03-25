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
        //triggerDoorToOutside.SetActive(false);
        //InitialState
        // InfoSC.CurrentPlayerTrainInterval = 0;
        // InfoSC.CurrentEnemyTrainInterval = 0;
        enemyTrain.SetActive(false);
        //Debug.Log(InfoSC.CurrentPlayerTrainInterval.transform.localPosition);
        UpdateTrainLocation();
        requireText.text = "Select a location.";
        if(InfoSC.ConfirmedSelectedPt!=TurnPtIndex){  //now in turn pt
            UpdateMapPointState();
        }
        // StartCoroutine(PlayerTrainMoveTowards());
    }

    void Update()
    {
        //UpdateTrainLocation();
        //Debug.Log(id);
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

        // if(SMD.IsMoving){
        //     //train sprite start moving slowly
        //     //player train move to target pt
        //     //takes several while (sec)
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
                PopUpPoint.Add(points[i]);
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
    void CheckHaveFuel(int fuelNeeded){
            //check current selected point
            // take into account that what if player wanna skip some points, need to add up the accumulated fuel require
    }
    void UpdateTrainLocation(){ //For instant update when come back from map pt scene
        float pX = Intervals[InfoSC.CurrentPlayerTrainInterval].transform.localPosition.x;
        float pY = Intervals[InfoSC.CurrentPlayerTrainInterval].transform.localPosition.y;
        float eX = Intervals[InfoSC.CurrentEnemyTrainInterval].transform.localPosition.x;
        float eY = Intervals[InfoSC.CurrentEnemyTrainInterval].transform.localPosition.y;
        playerTrain.transform.localPosition = new Vector3(pX,pY,0);  //set theit location
        enemyTrain.transform.localPosition = new Vector3(eX, eY, 0);
    }
    void EnemyMove(int ToPoint){ InfoSC.CurrentEnemyTrainInterval = ToPoint;} //put the num of destination pt
    public void PlayerMove(int ToPoint){ InfoSC.CurrentPlayerTrainInterval = ToPoint;} //insert the currentSetected Pt
    
    IEnumerator PlayerTrainMoveTowards(bool isMoving, float speed){ //this will be trigger to move player train when ever the currentplayerinterval changes
        //bool isMoving = true;
        GameObject targetPos = Intervals[InfoSC.CurrentPlayerTrainInterval];
        while (playerTrain.transform.localPosition != targetPos.transform.localPosition)
       {
           playerTrain.transform.localPosition = Vector2.MoveTowards(playerTrain.transform.localPosition, targetPos.transform.localPosition, speed * Time.deltaTime);
           yield return null;
       }
       isMoving = false;
       yield return new WaitUntil(() => !isMoving);
       ///StartCoroutine(PlayerTrainMoveTowards());
    }
    public void PTMT(bool isMoving, float speed){
        StartCoroutine(PlayerTrainMoveTowards(isMoving,speed));
    }
    public void GetTotalFuelNeeded(int index){
        int sum = 0;
        int currentLocal = InfoSC.ConfirmedPlayerTrainLocal;
        for(int i = currentLocal+1; i<index+1;i++){
            sum += points[i].GetComponent<Point>().fuelAmtNeeded;
        }
        requireText.text = "REQUIRE " + sum.ToString() + " FUEL";
        SMD.fuelCost = sum;
    }
    public void ResetFuelNeedDisplay(){
        requireText.text = "Select a location.";
    }
    public void DisablePreviousLands(){
        // int currentLocal = confirmedPlayerTrainLocal;
        // for(int i = 0; i<currentLocal;i++){
        //     points[i].
        // }
    }
    public void UpdateMapPointState(){
        for(int i = 1; i < InfoSC.ConfirmedSelectedPt;i++){ //excluse start pt
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
    
}
//order:
//u select a point on the map, u go pull the lever, train start moving and arrive in a while