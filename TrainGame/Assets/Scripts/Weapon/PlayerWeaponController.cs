using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using Spine;

public class PlayerWeaponController : MonoBehaviour
{
    private CharacterHandleWeapon handleWeapon;
    [SerializeField] private Weapon smallBlade;

    private TopDownController3D controller;
    private PlayerManager PM;

    //GunRelated-------
    //[Header("Weapons")]
    // [SerializeField] private Transform bigGunArm;
    // [SerializeField] private Transform smallGunArm;
    public enum GunType { NONE, SMALLGUN, BIGGUN, SWORDS};
    public GunType currentGunType = GunType.NONE;

    Vector3 mousePreviousWorld, mouseDeltaWorld;
	Camera mainCamera;
    private bool left, right;

    [Header("Values")]
    public bool canRotate = false;  
    [SerializeField] private float UpperRotationBound_B_R, LowerRotationBound_B_R;
    [SerializeField] private float UpperRotationBound_B_L, LowerRotationBound_B_L;
    [SerializeField] private float UpperRotationBound_S_R, LowerRotationBound_S_R;
    [SerializeField] private float UpperRotationBound_S_L, LowerRotationBound_S_L;

    [SerializeField] private Quaternion reset_LeftBone;
    [SerializeField] private Quaternion reset_RightBone;

    void Start()
    {
        handleWeapon = GetComponent<CharacterHandleWeapon>();

        if (GetComponent<TopDownController3D>() != null)
            controller = GetComponent<TopDownController3D>();
        else    
            Debug.Log("Can't find top down controller 3d in " + name);

        PM = this.gameObject.GetComponent<PlayerManager>();
        mainCamera = Camera.main;
        canRotate = true;
        //-----
        left = false;
        right = false;

        UpperRotationBound_B_R = -80.0f;
        LowerRotationBound_B_R = -120.0f;
        UpperRotationBound_B_L = 70.0f;
        LowerRotationBound_B_L = 130.0f;

        UpperRotationBound_S_R = -100.0f;
        LowerRotationBound_S_R = 50.0f;
        UpperRotationBound_S_L = -20.0f;
        LowerRotationBound_S_L = 90.0f;
        // reset_LeftBone = bigGunArm.rotation;
        // reset_RightBone = smallGunArm.rotation;
    }

    void Update()
    {
        GunArrangement();

        if (Input.GetKeyDown(KeyCode.Alpha0))
            handleWeapon.ChangeWeapon(smallBlade, smallBlade.WeaponName, false);

        if(Input.GetKeyDown(KeyCode.A)){
            left = true;
            right = false;
        }else if(Input.GetKeyDown(KeyCode.D)){
            right = true;
            left = false;
        }
    }
    public void ResetBones(){
        // bigGunArm.rotation = reset_LeftBone;
        // smallGunArm.rotation = reset_RightBone;
    }

    public void GunArrangement(){
        // Vector3 mouseCurrent = Input.mousePosition;
		// Vector3 mouseCurrentWorld = mainCamera.ScreenToWorldPoint(new Vector3(mouseCurrent.x, mouseCurrent.y, -mainCamera.transform.position.z));

		// mouseDeltaWorld = mouseCurrentWorld - mousePreviousWorld;
		// mousePreviousWorld = mouseCurrentWorld;
        // Debug.Log(mouseCurrentWorld);

        // if(Input.GetKeyDown(KeyCode.G)){
        //     canRotate = true;
        // }

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


    }
}
