using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public GameObject[] points;
    public int gameState = 0;
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

        if(playerTurn = true)
        {

        }
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
