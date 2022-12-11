using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public Node node;
    public Sprite sprite;
    public NodeTypes[] nodeType;
    public LayerMask layermask;
    [SerializeField]
    public List<GameObject> nearbyNodesList = new List<GameObject>();

    public List<GameObject> connectedNodeList = new List<GameObject>();
    public Material material;
    public bool isPlayer;
    public NodeTypes player;
    public NodeTypes nullNodeType;
    public Node nullNode;

    public bool isMoved;
    // Start is called before the first frame update
    void Start()
    {
        nullNode = new Node(nullNodeType);
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
        sprite = node.sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

    }

    public void ConnectNodes()
    {
        var nearbyNodes = Physics.SphereCastAll(this.transform.position, 7, Vector3.forward, layermask);
        foreach (RaycastHit hit in nearbyNodes)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.name != this.gameObject.name)
            {
                Debug.Log(hit.collider.gameObject.name);
                nearbyNodesList.Add(hit.collider.gameObject);
            }
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
        if (nearbyNodesList.Count > 0) {
            lr.SetPosition(1, nearbyNodesList[0].transform.position);
            connectedNodeList.Add(nearbyNodesList[0]);
            nearbyNodesList[0].GetComponent<NodeManager>().connectedNodeList.Add(this.gameObject);
        }
    }


    public void ConnectPlayer(List<GameObject> playerNearbyList)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject myLine = new GameObject();
            myLine.transform.position = this.transform.position;
            myLine.AddComponent<LineRenderer>();
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
            lr.material = material;
            lr.SetColors(Color.white, Color.white);
            lr.SetWidth(0.042f, 0.042f);
            lr.SetPosition(0, this.transform.position);
            lr.SetPosition(1, playerNearbyList[i].transform.position);
        }
    }
    public void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    public void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnMouseDown()
    {
        MapState.gameState = 1;
    }
}