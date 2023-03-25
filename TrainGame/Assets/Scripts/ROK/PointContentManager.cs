using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using NaughtyAttributes;
//This script is for managing the randomized content inside the map points & bring up the correct things at the right time
public class PointContentManager : MonoBehaviour
{
    public Info InfoSC;
    public List<GameObject> PointScene = new List<GameObject>();
    public List<GameObject> AllLand = new List<GameObject>();
    void Awake(){
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
        for(int i = 0; i<PointScene.Count;i++){
            PointScene[i].SetActive(false);
        }
        for(int i = 0; i<AllLand.Count;i++){
            AllLand[i].SetActive(false);
        }
    }

    void Start()
    {
        PointScene[InfoSC.pointID].SetActive(true);
    }

    void Update()
    {
        
    }
}
