using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node 
{
    public bool isUsed;
    public GameObject[] connectedNodes;
    public bool isEvent;
    public bool isEnemy;
    public bool isEmpty;
    public Sprite sprite;

    public Node(NodeTypes nodeType)
    {
        this.sprite = nodeType.sprite;
        if(nodeType.isEmpty == true)
        {
            isUsed = false;

        }
        else if(nodeType.isEnemy == true)
        {
            this.isEnemy = true;
        }
        else if(nodeType.isEvent == true)
        {
            this.isEvent = true;
        }
        
        
        
        

    }

    public void UpdateNode()
    {

    }

}
