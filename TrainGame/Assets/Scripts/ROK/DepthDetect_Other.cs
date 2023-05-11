using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is a depth detect obj thatattach on each of the character or enviroenment obj
//all thign sshd be on the same layer: Character & Depth
//thing will be in update to constantly detect 

public class DepthDetect_Other : MonoBehaviour
{
    public enum ObjType { ENEMY, ENVIRO }
    public float rayLength = 30f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CheckLayer(GameObject obj){
        

    }
}
