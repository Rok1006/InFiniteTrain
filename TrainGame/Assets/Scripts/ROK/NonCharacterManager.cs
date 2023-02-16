using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this on all non character object, including spawned object/depth detect
//depthdetect between this and ither same type
public class NonCharacterManager : MonoBehaviour
{
    public bool isOverlappWithMain;  //frm environment
    void Start()
    {
        isOverlappWithMain = false;
    }

    void Update()
    {
    }
    public void OnTriggerEnter(Collider col) {
        if(col.gameObject.layer == 26){ //MainObject:Box
            isOverlappWithMain=true;
            Debug.Log("Overlapped");
        }
    }
}
