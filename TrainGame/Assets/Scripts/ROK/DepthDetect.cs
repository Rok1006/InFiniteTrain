using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using NaughtyAttributes;
//Depth Dect script for player only
//all thign sshd be on the same layer: Character & Depth
public class DepthDetect : MonoBehaviour
{
    [SerializeField] private GameObject Main; //the main player object
    [SerializeField] private GameObject FrontMC;
    [SerializeField] private GameObject BackMC;

    public float rayLength = 15f;

    [ReadOnly] public int OrderIndex = 0;
    

    void Start()
    {
        
    }

//Layer
    void Update(){ //RayCast Detect
        //int layerMask = LayerMask.GetMask("Enemies");
        FrontMC.GetComponent<MeshRenderer>().sortingOrder = OrderIndex;
        BackMC.GetComponent<MeshRenderer>().sortingOrder = OrderIndex;
        //back
        Vector3 rayStartPos = new Vector3(transform.position.x, transform.position.y,transform.position.z);
        RaycastHit hitB;
        if(Physics.Raycast(rayStartPos, Vector3.forward, out hitB, rayLength)){
            Debug.DrawRay(rayStartPos, new Vector3(0,0,rayLength), Color.green);
            //Debug.Log(hitB.transform.gameObject.name);
            CheckLayer(hitB.transform.gameObject);
        }
        Debug.DrawRay(rayStartPos, new Vector3(0,0,rayLength), Color.red);

        //Front
        //Vector3 rayStartPos1 = new Vector3(transform.position.x, transform.position.y+0.99f,transform.position.z+0.5f);
        RaycastHit hitF;
        if(Physics.Raycast(rayStartPos, -Vector3.forward, out hitF, rayLength)){
            Debug.DrawRay(rayStartPos, new Vector3(0,0,-rayLength), Color.green);
            //Debug.Log(hitF.transform.gameObject.name);
            CheckLayer(hitF.transform.gameObject);
        }
        Debug.DrawRay(rayStartPos, new Vector3(0,0,-rayLength), Color.red);

        // RaycastHit hitL;
        // if(Physics.Raycast(rayStartPos, Vector3.left, out hitL, rayLength)){
        //     CheckLayer(hitL.transform.gameObject);
        //     //Debug.Log(hitL.transform.gameObject.name);
        // }
        // RaycastHit hitR;
        // if(Physics.Raycast(rayStartPos, -Vector3.left, out hitR, rayLength)){
        //     CheckLayer(hitR.transform.gameObject);
        //     //Debug.Log(hitL.transform.gameObject.name);
        // }
    
    }
    public void CheckLayer(GameObject obj){
        if(obj.tag == "Environment"){
            //var so_obj = obj.transform.GetChild(1);
            int obj_currentIndex = obj.GetComponent<DepthDetect_Other>().thisObjIndex;
            Debug.Log(obj_currentIndex);
            if(obj.transform.position.z>Main.transform.position.z){ //behind
                OrderIndex = obj_currentIndex+1;
            }else if(obj.transform.position.z<Main.transform.position.z){ //infront
                OrderIndex = obj_currentIndex-1;
            }
        }
        
    }
}
