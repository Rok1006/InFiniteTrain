using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public GameObject[] points;
   public static int gameState = 0;
    public GameObject player;
    public List<GameObject> availableDestination = new List<GameObject>();
    private bool playerTurn = false;
    private bool enemyTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == 0)
        {
            playerTurn = true;
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
            gameState = 0;
            Debug.Log("can pick again");
        }
    }
    public bool AvailableToMove(GameObject gm)
    {
        var points = player.GetComponent<Point>();
        for (int i = 0; i < points.connectedPoints.Length ; i++)
        {
            Debug.Log("comparing" + points.connectedPoints[i].name);
            if (points.connectedPoints[i].Equals(gm)){
                Debug.Log("tru");
                return true;
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

    
}
