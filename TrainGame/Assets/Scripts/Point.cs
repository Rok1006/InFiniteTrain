using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point : MonoBehaviour
{
    public GameObject[] connectedPoints;
    public Sprite icon;
    [TextArea(15,20)] public string text;
    public bool isPlayer = false;
    public bool isEnemy = false;
    public bool isMerchant = false;
    public bool isEvent = false;
    public bool isNull = false;
    public int id;
    public int fuelAmtNeeded;
    public Info InfoSC;

     void Update()
    {
        if(isEnemy == false && isPlayer == false && isMerchant == false && isEvent == false)
        {
            isNull = true;
        }
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
    }
   
    public void SendInfo(){
        InfoSC.pointID = id;
        InfoSC.CurrentSelectedPt = id;
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
        Debug.Log("fdf");
    }

    public void MoveMerchant()
    {

    }
    public void ShuffleEnemyInfo()
    {

    }
    public void Reset()
    {
        //this.icon = null;
        //this.text = "";
        this.isPlayer = false;
        this.isEnemy = false;
        this.isEvent = false;
        this.isMerchant = false;
        
    }



}
