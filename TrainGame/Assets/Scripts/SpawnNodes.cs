using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodes : MonoBehaviour

{
    public static int id = 0;
    public List<GameObject> nodes = new List<GameObject>();
    public GameObject nodePrefab;
    private Vector3 offset;
    public GameObject[] spawnPoints;
    public GameObject player;
    public List<GameObject> shortestNodes = new List<GameObject>();
    public float timer = 3f;

    


    // Start is called before the first frame update
    void Start()
    {
        
        var playerNode = Instantiate(player, transform.position, transform.rotation);
        playerNode.tag = "Player";

        for (int j = 0; j < spawnPoints.Length; j++)
        {


            for (int i = 0; i < 4; i++)
            {
                for (int n = 0; n < 100; n++)
                {
                    var randomX = Random.Range(-4, 4);
                    var randomY = Random.Range(-4, 4);
                    offset = new Vector3(randomX, randomY, 0);
                    bool isTooClose = false;
                    foreach (GameObject point in nodes)
                    {
                        if (Vector3.Distance(point.transform.position, spawnPoints[j].transform.position + offset) < 4.0f)
                        {
                            Debug.Log(point.transform.position + " is too close");
                            isTooClose = true;
                            break;
                        }
                    }
                    if (!isTooClose)
                        break;
                }
                var node = Instantiate(nodePrefab, spawnPoints[j].transform.position + offset, transform.rotation);
                node.name = id.ToString();
                id++;
                nodes.Add(node);


            }
        }
        foreach (GameObject go in nodes)
        {
            go.GetComponent<NodeManager>().ConnectNodes();
            go.GetComponent<NodeManager>().DrawLinesNodes();
        }

        CalculateDistance(nodes, playerNode);
        playerNode.GetComponent<NodeManager>().ConnectPlayer(shortestNodes);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            NextTurn();
            timer = 10;
        }

    }
    public void CalculateDistance(List<GameObject> nodes, GameObject player)
    {
        float distance = 100, distance2 = 100, distance3 = 100, distance4 = 100;
        var index = 0;
        float temp, temp2, temp3, temp4;
        var list = nodes;
        for (int i = 0; i < 4; i++)
        {
            temp = Vector3.Distance(nodes[i].transform.position, player.transform.position);
            if (temp < distance)
            {
                distance = temp;
                index = i;
            }

        }
        shortestNodes.Add(nodes[index]);
        index = 0;
        for (int i = 4; i < 8; i++)
        {
            temp2 = Vector3.Distance(nodes[i].transform.position, player.transform.position);
            if (temp2 < distance2)
            {
                distance2 = temp2;
                index = i;
            }
        }
        shortestNodes.Add(nodes[index]);
        index = 0;
        for (int i = 8; i < 12; i++)
        {
            temp3 = Vector3.Distance(nodes[i].transform.position, player.transform.position);
            if (temp3 < distance3)
            {
                distance3 = temp3;
                index = i;
            }
        }
        shortestNodes.Add(nodes[index]);
        index = 0;
        for (int i = 12; i < 16; i++)
        {
            temp4 = Vector3.Distance(nodes[i].transform.position, player.transform.position);
            if (temp4 < distance4)
            {
                distance4 = temp4;
                index = i;
            }
        }
        shortestNodes.Add(nodes[index]);
        index = 0;

    }

    public async void NextTurn()
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            var nodeManager = nodes[i].GetComponent<NodeManager>();
            var randomNeighbor = Random.Range(0 , nodeManager.connectedNodeList.Count);
            if (nodes[i].GetComponent<NodeManager>().node.isEnemy == true && nodes[i].GetComponent<NodeManager>().isMoved == false &&  nodeManager.connectedNodeList[randomNeighbor].GetComponent<NodeManager>().isMoved == false )
            {
                
                
                nodeManager.connectedNodeList[randomNeighbor].GetComponent<NodeManager>().node.UpdateNode(nodes[i].GetComponent<NodeManager>().node);
                nodeManager.connectedNodeList[randomNeighbor].GetComponent<NodeManager>().isMoved = true;
                nodes[i].GetComponent<NodeManager>().node.RefreshNode();
                nodes[i].GetComponent<NodeManager>().node.UpdateNode(nodes[i].GetComponent<NodeManager>().nullNode);
                
            }
            
        }

        for(int i = 0; i < nodes.Count; i++){
            nodes[i].GetComponent<NodeManager>().isMoved = false;
        }
    }



}