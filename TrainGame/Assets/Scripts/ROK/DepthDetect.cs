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

    public float rayLength = 30f;

    [ReadOnly] public int frontOrderIndex, backOrderIndex;
    

    void Start()
    {
        
    }

//Layer
    void Update(){ //RayCast Detect
        //int layerMask = LayerMask.GetMask("Enemies");
        //back
        Vector3 rayStartPos = new Vector3(transform.position.x, transform.position.y+0.99f,transform.position.z+0.5f);
        if(Physics.Raycast(rayStartPos, Vector3.forward, rayLength)){
            Debug.DrawRay(rayStartPos, new Vector3(0,0,rayLength), Color.green);
            // Debug.Log("yep");
            FrontMC.GetComponent<MeshRenderer>().sortingOrder = 1;
            BackMC.GetComponent<MeshRenderer>().sortingOrder = 1;
        }
        Debug.DrawRay(rayStartPos, new Vector3(0,0,rayLength), Color.red);

        //Front
        //Vector3 rayStartPos1 = new Vector3(transform.position.x, transform.position.y+0.99f,transform.position.z+0.5f);
        if(Physics.Raycast(rayStartPos, -Vector3.forward, rayLength)){
            Debug.DrawRay(rayStartPos, new Vector3(0,0,-rayLength), Color.green);
            //Debug.Log("yep");
            FrontMC.GetComponent<MeshRenderer>().sortingOrder = -1;
            BackMC.GetComponent<MeshRenderer>().sortingOrder = -1;
        }
        Debug.DrawRay(rayStartPos, new Vector3(0,0,-rayLength), Color.red);

        RaycastHit hitL;
        if(Physics.Raycast(rayStartPos, Vector3.left, out hitL, rayLength)){
            CheckLayer(hitL.transform.gameObject);
        }
        RaycastHit hitR;
        if(Physics.Raycast(rayStartPos, -Vector3.left, out hitR, rayLength)){
            CheckLayer(hitR.transform.gameObject);
        }
    
    }
    public void CheckLayer(GameObject obj){
        if(obj.transform.position.z>Main.transform.position.z){ //behind
            FrontMC.GetComponent<MeshRenderer>().sortingOrder = 1;
            BackMC.GetComponent<MeshRenderer>().sortingOrder = 1;
        }else if(obj.transform.position.z<Main.transform.position.z){ //infront
            FrontMC.GetComponent<MeshRenderer>().sortingOrder = -1;
            BackMC.GetComponent<MeshRenderer>().sortingOrder = -1;
        }
    }
}
