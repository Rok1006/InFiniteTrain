using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THis script is for testing gun arm rotation on runtime
public class PlayerGunConstrainTest : MonoBehaviour
{
    public Transform bendArm;
    public Transform target; //the mouse target
    public Vector3 targetPosition;
    public bool canRotate = false;

    void Start()
    {
        
    }


    void Update()
    {

//         Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
//  Debug.Log(Camera.main.ScreenToWorldPoint(vector));
        if(Input.GetKeyDown(KeyCode.G)){
            canRotate = true;
        }
        //get the mouse position and rotate bone towards
    }
    private void FixedUpdate(){ //not detecting mouse for some reason
        Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
 //Debug.Log(Camera.main.ScreenToWorldPoint(vector));
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(vector);
//Debug.Log(worldMousePos);
        Vector3 diff = worldMousePos - bendArm.position;
        diff.Normalize();
        //Debug.Log(diff);
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        if(canRotate){
           bendArm.rotation = Quaternion.Euler(0f,0f, rotationZ); 
        }
        
    }
}
