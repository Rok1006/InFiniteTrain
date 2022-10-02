using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNodes : MonoBehaviour

{
    public List<GameObject> nodes = new List<GameObject>();
    public GameObject nodePrefab;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            var randomX = Random.Range(-10, 10);
            var randomY = Random.Range(-10, 10);
            offset = new Vector3(randomX, randomY, 0);
            var node =Instantiate(nodePrefab, transform.position + offset, transform.rotation);
            nodes.Add(node);
           

        }
        foreach(GameObject go in nodes)
        {
            go.GetComponent<NodeManager>().ConnectNodes();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
