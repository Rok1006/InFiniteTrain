using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodes : MonoBehaviour

{
    public List<GameObject> nodes = new List<GameObject>();
    public GameObject nodePrefab;
    private Vector3 offset;
    public GameObject[] spawnPoints;
    public GameObject player;
    public List<GameObject> shortestNodes = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        var playerNode = Instantiate(player, transform.position, transform.rotation);

        for (int j = 0; j < spawnPoints.Length; j++)
        {


            for (int i = 0; i < 4; i++)
            {
                var randomX = Random.Range(-4, 4);
                var randomY = Random.Range(-4, 4);
                offset = new Vector3(randomX, randomY, 0);
                var node = Instantiate(nodePrefab, spawnPoints[j].transform.position + offset, transform.rotation);
                nodes.Add(node);


            }
        }
        foreach(GameObject go in nodes)
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
        
    }
    public void CalculateDistance(List<GameObject> nodes , GameObject player)
    {
        float distance = 100 , distance2 = 100, distance3 = 100, distance4 = 100;
        var index = 0;
        float temp , temp2, temp3, temp4;
        var list = nodes;
        for(int i = 0; i < 4; i++)
        {
            temp = Vector3.Distance(nodes[i].transform.position, player.transform.position);
            if(temp < distance)
            {
                distance = temp;
                index = i;
            }
            
        }
        shortestNodes.Add(nodes[index]);
        index = 0;
        for(int i = 4; i < 8; i++)
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


}
