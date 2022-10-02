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
    public Material material;
    public bool isPlayer;
    public NodeTypes player;
    // Start is called before the first frame update
    void Start()
    {
        if (isPlayer == false)
        {


            node = new Node(nodeType[Random.Range(0, nodeType.Length)]);
            sprite = node.sprite;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            node = new Node(player);
            sprite = node.sprite;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
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
    public void DrawLinesNodes()
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = this.transform.position;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = material;
        lr.SetColors(Color.white, Color.white);
        lr.SetWidth(0.042f, 0.042f);
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, nearbyNodesList[0].transform.position);
    }
}
