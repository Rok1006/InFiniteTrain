using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private CharacterHandleWeapon handleWeapon;
    [SerializeField] private Weapon smallBlade;

    private TopDownController3D controller;
    private PlayerManager PM;

    //GunRelated-------
    [Header("Weapons")]
    [SerializeField] private Transform bigGunArm;
    [SerializeField] private Transform smallGunArm;
    public enum GunType { NONE, SMALLGUN, BIGGUN};
    public GunType currentGunType = GunType.NONE;

    Vector3 mousePreviousWorld, mouseDeltaWorld;
	Camera mainCamera;

    [Header("Values")]
    public bool canRotate = false;  
    public float UpperRotationBound_R = -80.0f;
    public float LowerRotationBound_R = -120.0f;

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
    }

    void Update()
    {
        GunArrangement();

        if (Input.GetKeyDown(KeyCode.Alpha0))
            handleWeapon.ChangeWeapon(smallBlade, smallBlade.WeaponName, false);
    }

    public void GunArrangement(){
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
                if(canRotate && PM.transform.position.x >= PM.oldPositionX){ //when player face Right
                        //    bendArm.rotation = Quaternion.Euler(0f,0f, rotationZ); 
                    bigGunArm.rotation = Quaternion.Euler(0f,0f,Mathf.Clamp(rotationZ, LowerRotationBound_R, UpperRotationBound_R));
                }else if(canRotate && PM.transform.position.x <= PM.oldPositionX){ //Left
                    bigGunArm.rotation = Quaternion.Euler(0f,0f,Mathf.Clamp(rotationZ, 130.0f, 70.0f));
                }
            break;
        }


    }
}
