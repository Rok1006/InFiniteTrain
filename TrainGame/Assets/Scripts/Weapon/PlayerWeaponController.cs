using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using Spine;

public class PlayerWeaponController : MonoBehaviour
{
    private CharacterHandleWeapon handleWeapon;
    private CharacterOrientation2D ChOri_2D;
    [SerializeField] private Weapon smallBlade;

    private TopDownController3D controller;
    private PlayerManager PM;
    [SerializeField] private Animator playerAnim;

    //GunRelated-------
    [Header("Weapons")]
    [SerializeField] private Transform aim; //clamp it certain area
    // [SerializeField] private Transform bigGunArm;
    // [SerializeField] private Transform smallGunArm;
    public enum GunType { NONE, SMALLGUN, BIGGUN, SWORDS};
    public GunType currentGunType = GunType.NONE;

    Vector3 mousePreviousWorld, mouseDeltaWorld;
    Vector3 mouseCurrentWorld;
    Vector3 originalRotation;
	Camera mainCamera;
    private bool left, right, down;

    [Header("Values")]
    public bool canRotate = false;  

    void Start()
    {
        handleWeapon = GetComponent<CharacterHandleWeapon>();

        if (GetComponent<TopDownController3D>() != null)
            controller = GetComponent<TopDownController3D>();
        else    
            Debug.Log("Can't find top down controller 3d in " + name);

        PM = this.gameObject.GetComponent<PlayerManager>();
        ChOri_2D = this.gameObject.GetComponent<CharacterOrientation2D>();
        mainCamera = Camera.main;
        canRotate = false;
        //-----
        left = false;
        right = false;
        down = true;
        // reset_RightBone = smallGunArm.rotation;
       //originalRotation = this.transform.rotation;
    }

    void Update()
    {
        GunArrangement();
        if(canRotate){
            aim.position = mouseCurrentWorld;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
            handleWeapon.ChangeWeapon(smallBlade, smallBlade.WeaponName, false);

        if(Input.GetKeyDown(KeyCode.A)){
            left = true;
            right = false;
            down = false;
        }else if(Input.GetKeyDown(KeyCode.D)){
            right = true;
            left = false;
            down = false;
        }else if(Input.GetKeyDown(KeyCode.S)){
            down = true;
            right = false;
            left = false;
        }
    }
    public void ResetBones(){
        // bigGunArm.rotation = reset_LeftBone;
        // smallGunArm.rotation = reset_RightBone;
    }

    public void GunArrangement(){
        Vector3 mouseCurrent = Input.mousePosition;
		mouseCurrentWorld = mainCamera.ScreenToWorldPoint(new Vector3(mouseCurrent.x, mouseCurrent.y, -mainCamera.transform.position.z));

		mouseDeltaWorld = mouseCurrentWorld - mousePreviousWorld;
		mousePreviousWorld = mouseCurrentWorld;

        Vector3 pT = this.transform.position;
       // mouseCurrentWorld.Normalize();
        switch(currentGunType){
            case GunType.SMALLGUN:
            if(ChOri_2D.IsRight){
                mouseCurrentWorld.x = Mathf.Clamp(mouseCurrentWorld.x, pT.x, pT.x+5.0f); //Mathf.Clamp(mouseCurrentWorld.x, minX_S, maxX_S);
                mouseCurrentWorld.y = Mathf.Clamp(mouseCurrentWorld.y, pT.y-4.0f, pT.y+7.0f); //Mathf.Clamp(mouseCurrentWorld.y, minY_S, maxY_S);
            }else{
                //Debug.Log("left");
                mouseCurrentWorld.x = Mathf.Clamp(mouseCurrentWorld.x, pT.x-5.0f, pT.x); //Mathf.Clamp(mouseCurrentWorld.x, minX_S, maxX_S);
                mouseCurrentWorld.y = Mathf.Clamp(mouseCurrentWorld.y, pT.y-4.0f, pT.y+7.0f); //Mathf.Clamp(mouseCurrentWorld.y, minY_S, maxY_S);
            }
            break;
            case GunType.BIGGUN:
            if(ChOri_2D.IsRight){
                mouseCurrentWorld.x = Mathf.Clamp(mouseCurrentWorld.x, pT.x+5.5f, pT.x+8.0f);
                mouseCurrentWorld.y = Mathf.Clamp(mouseCurrentWorld.y, pT.y+1.5f, pT.y+6.0f);
            }else if(!ChOri_2D.IsRight){ //make it so that when gun mode it wont flip
                //Debug.Log("left");
                mouseCurrentWorld.x = Mathf.Clamp(mouseCurrentWorld.x, pT.x-8.0f, pT.x-5.5f); //Mathf.Clamp(mouseCurrentWorld.x, minX_S, maxX_S);
                mouseCurrentWorld.y = Mathf.Clamp(mouseCurrentWorld.y, pT.y+1.5f, pT.y+6.0f); //Mathf.Clamp(mouseCurrentWorld.y, minY_S, maxY_S);
            }
            // else if(ChOri_2D.IsRight&&down || !ChOri_2D.IsRight&&down){
            //     mouseCurrentWorld.x = Mathf.Clamp(mouseCurrentWorld.x, 0, 0); //Mathf.Clamp(mouseCurrentWorld.x, minX_S, maxX_S);
            //     mouseCurrentWorld.y = Mathf.Clamp(mouseCurrentWorld.y, 0, 0);
            // }
            break;
        }
        
 
        //Debug.Log(mouseCurrentWorld);

        // if(Input.GetKeyDown(KeyCode.J)|| Input.GetKeyDown(KeyCode.K)){
        //     canRotate = true;
        // }
//Dump
        // switch(currentGunType){
        //     case GunType.NONE:
        //     break;
        //     case GunType.SWORDS: //No rotation
        //         ResetBones();
        //         //bigGunArm.gameObject.GetComponent<SkeletonUtilityBone>().mode = follow;
        //         //release clamp, cus its overrided in scene turn override to follow

        //     break;
        //     case GunType.SMALLGUN:
        //         Vector3 r_s = mouseCurrentWorld - smallGunArm.transform.position;
        //         r_s.Normalize();
        //         float rotationZ_S = Mathf.Atan2(r_s.y, r_s.x) * Mathf.Rad2Deg;
        //         if(canRotate && right){ //when player face Right, PM.transform.position.x >= PM.oldPositionX
        //             smallGunArm.rotation = Quaternion.Euler(0f,0f,Mathf.Clamp(rotationZ_S, LowerRotationBound_S_R, UpperRotationBound_S_R));
        //         }else if(canRotate && left){ //Left, PM.transform.position.x <= PM.oldPositionX
        //             smallGunArm.rotation = Quaternion.Euler(0f,0f,Mathf.Clamp(rotationZ_S, LowerRotationBound_S_L, UpperRotationBound_S_L));
        //         }
        //         Debug.Log(smallGunArm.rotation);
        //     break;
        //     case GunType.BIGGUN:
        //         Vector3 r = mouseCurrentWorld - bigGunArm.transform.position;
        //         r.Normalize();
        //         float rotationZ = Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg;
        //         if(canRotate && right){ //when player face Right, PM.transform.position.x >= PM.oldPositionX
        //             bigGunArm.rotation = Quaternion.Euler(0f,0f,Mathf.Clamp(rotationZ, LowerRotationBound_B_R, UpperRotationBound_B_R));
        //         }else if(canRotate && left){ //Left, PM.transform.position.x <= PM.oldPositionX
        //             bigGunArm.rotation = Quaternion.Euler(0f,0f,Mathf.Clamp(rotationZ, LowerRotationBound_B_L, UpperRotationBound_B_L));
        //         }
        //     break;
        // }
//

    }
}
