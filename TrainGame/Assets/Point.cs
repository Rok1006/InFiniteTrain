using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point : MonoBehaviour
{
    public GameObject[] connectedPoints;
    public Sprite icon;
    public string text;
    public bool isPlayer = false;
    public bool isEnemy = false;
    public bool isMerchant = false;
    public bool isEvent = false;
    public bool isNull = false;

    public void MovePlayer()
    {
        if(isPlayer == true)
        {
            //let player choose a point
            this.isPlayer = false;
            //chose point.isPlayer = true;
        }
    }

    public void MoveEnemy()
    {

    }

    public void MoveMerchant()
    {

    }

    public void ShuffleEnemyInfo()
    {

    }
    
    
}
