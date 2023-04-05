using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Point : MonoBehaviour
{
    private Info InfoSC;
    public GameObject[] connectedPoints;
    [BoxGroup("InfoBox")] public Sprite icon;
    [SerializeField,BoxGroup("InfoBox")] private int RadiationLvl; //1 skull is 0.05; How many skull
    [TextArea(15,20)] public string text;
    
    [BoxGroup("Status")] public bool isPlayer = false;
    [BoxGroup("Status")] public bool isEnemy = false;
    [BoxGroup("Status")] public bool isMerchant = false;
    [BoxGroup("Status")] public bool isEvent = false;
    [BoxGroup("Status")] public bool isNull = false;
    [BoxGroup("Info")] public int id;
    [BoxGroup("Info")] public int fuelAmtNeeded;
    [BoxGroup("Info")] public float _radAmt; //Radiation increase amt -0.3

    [SerializeField,BoxGroup("UI")] private GameObject RadiationLvlHolder;
    [SerializeField,BoxGroup("UI")] private GameObject SkullImage;
    
    void Start() {
        for(int i = 0; i < RadiationLvl; i++){
            GameObject skull = Instantiate(SkullImage, RadiationLvlHolder.transform, false);
        }
    }

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
                //this.icon = icon;
                //this.text = text;

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
