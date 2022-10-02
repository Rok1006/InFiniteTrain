using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public Node node;
    public Sprite sprite;
    public NodeTypes[] nodeType;
    public LayerMask layermask;
    public List<GameObject> nearbyNodesList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        node = new Node(nodeType[Random.Range(0,nodeType.Length )]);
        sprite = node.sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectNodes()
    {
        var nearbyNodes = Physics.SphereCastAll(this.transform.position, 5, Vector3.forward, layermask);
        foreach (RaycastHit hit in nearbyNodes)
        {
            Debug.Log("hit");
            nearbyNodesList.Add(hit.collider.gameObject);
        }
    }
}
