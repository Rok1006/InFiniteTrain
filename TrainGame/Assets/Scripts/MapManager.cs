using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public GameObject[] points;
   public static int gameState = 0;
    public GameObject player;
    public GameObject playerResource;
    public List<GameObject> availableDestination = new List<GameObject>();
    public bool playerTurn = false;
    public bool enemyTurn = false;
    public int id;

    public List<GameObject> PopUpPoint = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Singleton.Instance == null)
            Debug.Log("singlton is null");
        id = Singleton.Instance.id;
        UpdatePlayerIcon();
        UpdatePlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
    public bool AvailableToMove(GameObject gm)
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
    public void UpdatePlayer()
    {
        availableDestination.Clear();
        for(int i = 0; i < points.Length; i++)
        {
            if(points[i].GetComponent<Point>().isPlayer == true)
            {
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
        Debug.Log("df");
        for(int i = 0; i < points.Length; i++)
        {
            points[i].GetComponent<Point>().isPlayer = false;
            if ( points[i].GetComponent<Point>().id == this.id)
            {
                player = points[i];
                points[i].GetComponent<Point>().isPlayer = true;
                PopUpPoint.Add(points[i]);
            }
        }
        if (this.player != null && this.player.GetComponent<Point>().id != 0) 
        {
            Debug.Log("ddf");
            player.gameObject.GetComponent<MapPopUp>().ForceChange();
        }
    }
    
}
