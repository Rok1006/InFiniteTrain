using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THis script is for testing gun arm rotation on runtime
public class PlayerGunConstrainTest : MonoBehaviour
{
    public enum GunType { NONE, SMALLGUN, BIGGUN};
    public GunType currentGunType = GunType.NONE;
    [SerializeField] private Transform bigGunArm;
    [SerializeField] private Transform smallGunArm;
    
    Vector3 mousePreviousWorld, mouseDeltaWorld;
	Camera mainCamera;

    [Header("Values")]
    public bool canRotate = false;
    
    public float LowerRotationBound_R = -120.0f;   
    public float UpperRotationBound_R = -80.0f;

    void Start () {
		mainCamera = Camera.main;
        canRotate = true;
	}
    //Detect left Right of player

	void Update () {
		Vector3 mouseCurrent = Input.mousePosition;
		Vector3 mouseCurrentWorld = mainCamera.ScreenToWorldPoint(new Vector3(mouseCurrent.x, mouseCurrent.y, -mainCamera.transform.position.z));

		mouseDeltaWorld = mouseCurrentWorld - mousePreviousWorld;
		mousePreviousWorld = mouseCurrentWorld;

        Debug.Log(mouseCurrentWorld);

        if(Input.GetKeyDown(KeyCode.G)){
            canRotate = true;
        }

        switch(currentGunType){
               case GunType.NONE:
                //do things
               break;
               case GunType.SMALLGUN:
                //do things
               break;
               case GunType.BIGGUN:
                    Vector3 r = mouseCurrentWorld - bigGunArm.transform.position;
                    r.Normalize();
                    //Debug.Log(diff);
                    float rotationZ = Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg;
                    if(canRotate){
                        //    bendArm.rotation = Quaternion.Euler(0f,0f, rotationZ); 
                        bigGunArm.rotation = Quaternion.Euler(0f,0f,Mathf.Clamp(rotationZ, LowerRotationBound_R, UpperRotationBound_R));
                    }
               break;
           }

        
        
	}

private void FixedUpdate(){ //not detecting mouse for some reason
//         Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
//  //Debug.Log(Camera.main.ScreenToWorldPoint(vector));
//         Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(vector);
// //Debug.Log(worldMousePos);
        // Vector3 diff = mouseCurrentWorld - bendArm.position;
        // diff.Normalize();
        // //Debug.Log(diff);
        // float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        // if(canRotate){
        //    bendArm.rotation = Quaternion.Euler(0f,0f, rotationZ); 
        // }
        
    }
}
