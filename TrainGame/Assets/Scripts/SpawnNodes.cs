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
    

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, transform.rotation);
        for (int j = 0; j < spawnPoints.Length; j++)
        {


            for (int i = 0; i < 4; i++)
            {
                var randomX = Random.Range(-4, 4);
                var randomY = Random.Range(-4, 4);
                offset = new Vector3(randomX, randomY, 0);
                var node = Instantiate(nodePrefab, spawnPoints[i].transform.position + offset, transform.rotation);
                nodes.Add(node);


            }
        }
        foreach(GameObject go in nodes)
        {
            go.GetComponent<NodeManager>().ConnectNodes();
            go.GetComponent<NodeManager>().DrawLinesNodes();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
