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

     void Update()
    {
        if(isEnemy == false && isPlayer == false && isMerchant == false && isEvent == false)
        {
            isNull = true;
        }
    }


    public void MovePlayer()
    {
        foreach(GameObject x in connectedPoints)
        {
            var player = x.GetComponent<Point>();
            if (player.isPlayer == true)
            {
                var icon = player.icon;
                var text = player.text;
                if(this.isNull == true)
                {
                    this.isNull = false;
                }
                if(this.isEnemy == true)
                {
                    //envoke battle from here
                    Debug.Log("wow battle");
                }
                this.isPlayer = true;
                this.icon = icon;
                this.text = text;

                x.GetComponent<Point>().Reset();
            }
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
    public void Reset()
    {
        this.icon = null;
        this.text = "";
        this.isPlayer = false;
        this.isEnemy = false;
        this.isEvent = false;
        this.isMerchant = false;
        
    }



}
