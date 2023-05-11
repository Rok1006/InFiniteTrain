using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using NaughtyAttributes;
//This is a depth detect obj thatattach on each of the character or enviroenment obj
//all thign sshd be on the same layer: Character & Depth
//thing will be in update to constantly detect 

public class DepthDetect_Other : MonoBehaviour
{
    public enum ObjType { NONE, ENEMY, ENVIRO }
    public ObjType CurrentObjType = ObjType.NONE;
    [SerializeField] private float rayLength = 15f;
    [SerializeField] private GameObject ObjWithSortingLayer;
    public int thisObjIndex;
    [ReadOnly] public int OrderIndex = 0;
    public bool CheckCast = true;

    
    void Start()
    {
        
    }

    void Update()
    {
        thisObjIndex = ObjWithSortingLayer.GetComponent<MeshRenderer>().sortingOrder;
        ObjWithSortingLayer.GetComponent<MeshRenderer>().sortingOrder = OrderIndex;
        if(CheckCast){
            RayDetect();   
        }
        
    }
    void RayDetect(){
        //back
        Vector3 rayStartPos = new Vector3(transform.position.x, transform.position.y,transform.position.z);
        RaycastHit hitB;
        if(Physics.BoxCast(rayStartPos,this.transform.localScale / 2.0f, Vector3.forward, out hitB, Quaternion.identity, rayLength)){
            Debug.DrawRay(rayStartPos, new Vector3(0,0,rayLength), Color.green);
            //Debug.Log(hitB.transform.gameObject.name);
            CheckLayer(hitB.transform.gameObject);
        }
        Debug.DrawRay(rayStartPos, this.transform.forward*15, Color.yellow);

        //Front
        //Vector3 rayStartPos1 = new Vector3(transform.position.x, transform.position.y+0.99f,transform.position.z+0.5f);
        RaycastHit hitF;
        if(Physics.BoxCast(rayStartPos, this.transform.localScale / 2.0f,-Vector3.forward, out hitF, Quaternion.identity,  rayLength)){
            Debug.DrawRay(rayStartPos, new Vector3(0,0,-rayLength), Color.green);
            //Debug.Log(hitF.transform.gameObject.name);
            CheckLayer(hitF.transform.gameObject);
        }
        Debug.DrawRay(rayStartPos, new Vector3(0,0,-rayLength), Color.red);
    }

    public void CheckLayer(GameObject obj){
        if(obj.tag == "Environment"){
            //var so_obj = obj.transform.GetChild(1);
            int obj_currentIndex = obj.GetComponent<DepthDetect_Other>().thisObjIndex;
            Debug.Log(obj_currentIndex);
            if(obj.transform.position.z>this.transform.position.z){ //behind
                OrderIndex = obj_currentIndex+1;
            }else if(obj.transform.position.z<this.transform.position.z){ //infront
                OrderIndex = obj_currentIndex-1;
            }
        }

    }
}
