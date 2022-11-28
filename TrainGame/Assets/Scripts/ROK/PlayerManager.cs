using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MoreMountains.TopDownEngine;

//This script handle wtever related to Player that is not related to topdown engine
public class PlayerManager : MonoBehaviour
{
    [Header("Assignment")]
    [SerializeField] private GameObject FrontMC;
    [SerializeField] private GameObject BackMC;
    [SerializeField] private GameObject dustPrefab; //the particle system: prefab
    [SerializeField] private GameObject emitPt; //the particle system: prefab
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject depthDetect;
    public bool facingFront = true;   //or side
    public List<GameObject> dust = new List<GameObject>();

    Animator MCFrontAnim;
    Animator MCBackAnim;
    float x;
    float y;
    float z;
    private float oldPositionX = 0.0f;
    private float oldPositionZ = 0.0f;
    private bool isAttacking = false;

    //references
    private TopDownController3D controller;
    private CharacterHandleWeapon handleWeapon, secondaryHandleWeapon;

    //weapons
    [SerializeField] private WeaponCollection weaponCollection;
    private Weapon secondaryWeapon;

    void Start()
    {
        MCFrontAnim = FrontMC.GetComponent<Animator>();
        MCBackAnim = BackMC.GetComponent<Animator>();
        if(FrontMC!=null){
            FrontMC.transform.localScale = new Vector3(1,1,1);
            BackMC.transform.localScale = new Vector3(0,0,0);
        }
        facingFront = true;
        playerCam = GameObject.FindGameObjectWithTag("PlayerCam");
        if(playerCam!=null){
            var sc = SceneManage.Instance;
            var confiner = playerCam.GetComponent<CinemachineConfiner>();
            confiner.InvalidatePathCache();
            confiner.m_BoundingVolume = sc.MCTrainConfiner[0].GetComponent<Collider>();
        }

        //setting references up
        if (GetComponent<TopDownController3D>() != null)
            controller = GetComponent<TopDownController3D>();
        else    
            Debug.Log("Can't find top down controller 3d in " + name);

        //weapons
        secondaryWeapon = GetComponent<CharacterHandleSecondaryWeapon>().CurrentWeapon;
        if (secondaryWeapon == null)
            Debug.Log("cant find secondary weapon in " + gameObject.name);

        handleWeapon = GetComponent<CharacterHandleWeapon>();
        secondaryHandleWeapon = GetComponent<CharacterHandleSecondaryWeapon>();
    }

    void FixedUpdate()
    {
        if (controller.InputMoveDirection.z <= 0 && FrontMC!=null) //Change player gameObject
        {
            facingFront = true;
            FrontMC.transform.localScale = new Vector3(1,1,1);
            BackMC.transform.localScale = new Vector3(0,0,0);
            DustEmit();
            DustLayerSort(-1);
        }else if (controller.InputMoveDirection.z > 0 && FrontMC!=null)
        {
            facingFront = false;
            FrontMC.transform.localScale = new Vector3(0,0,0);
            BackMC.transform.localScale = new Vector3(1,1,1);
            DustEmit();
            DustLayerSort(1);
        }
        if (transform.position.x > oldPositionX) //Rotate the anim object instead of main
        {
            if(facingFront){DustLayerSort(-1);}else{DustLayerSort(1);};
            DustEmit();
        }else if (transform.position.x < oldPositionX)
        {
            if(facingFront){DustLayerSort(-1);}else{DustLayerSort(1);};
            DustEmit();
        }
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;

        //Combat Related
        if(Input.GetKeyDown(KeyCode.J)){ //
            DisableAllWeaponAnimation();
            MCFrontAnim.SetBool("Switch_smallGun", true);
            MCBackAnim.SetBool("Switch_smallGun", true);
        }
        if(Input.GetKeyDown(KeyCode.K)){ //
            DisableAllWeaponAnimation();
            MCFrontAnim.SetBool("Switch_bigGun", true);
            MCBackAnim.SetBool("Switch_bigGun", true);
        }
        if(Input.GetKeyDown(KeyCode.L)){ //
            Debug.Log("play"); //for some reason the animation is trigger many time even after just click once
            handleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[0], weaponCollection.MeleeWeapons[0].WeaponName, false);
            secondaryHandleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[1], weaponCollection.MeleeWeapons[1].WeaponName, false);
            MCFrontAnim.SetBool("IsUsingWeapon", true);
            DisableAllWeaponAnimation();
            //MCFrontAnim.SetBool("Switch_bigSword", true);//its always playing this so it wont go out of it
            MCFrontAnim.SetTrigger("UseBigSword");
            MCBackAnim.SetTrigger("UseBigSword");
            //MCBackAnim.SetBool("Switch_bigSword", true);
        }

    }
    public void DustEmit(){
        if(Time.frameCount%10 == 0 && emitPt!=null){
            GameObject d = Instantiate(dustPrefab, emitPt.transform.position, Quaternion.identity) as GameObject;
            dust.Add(d);
            Invoke("DestroyDust", .9f);
        }
    }
    private void DestroyDust(){
        Destroy(dust[0]);
        dust.RemoveAt(0);
    }
    private void DustLayerSort(int order){
        for(int i = 0; i < dust.Count; i++){
                dust[i].GetComponent<ParticleSystemRenderer>().sortingOrder = order;
        }
    }

    // IEnumerator TraverseBetweenTrain(){
    //     yield return new WaitForSeconds(0);
    // }
    void OnTriggerEnter(Collider col) {
        // var sc = SceneManage.Instance;
        // for(int i = 0; i<sc.MCTrainConfiner.Count;i++){
        //    if(col.gameObject.name == "TrainCar"+i && playerCam!=null){ 
        //     var confiner = playerCam.GetComponent<CinemachineConfiner>();
        //     confiner.InvalidatePathCache();
        //     confiner.m_BoundingVolume = sc.MCTrainConfiner[i].GetComponent<Collider>();
        //    }
        // }
    }

    public void DisableAllWeaponAnimation(){
        MCFrontAnim.SetBool("Switch_smallGun", false);
        MCFrontAnim.SetBool("Switch_bigGun", false);
        MCFrontAnim.SetBool("Switch_bigSword", false);
        MCBackAnim.SetBool("Switch_smallGun", false);
        MCBackAnim.SetBool("Switch_bigGun", false);
        MCBackAnim.SetBool("Switch_bigSword", false);
    }

    public void test() {
        // ComboWeapon secondaryCombo = secondaryWeapon.GetComponent<ComboWeapon>();
        // secondaryCombo.WeaponStarted(secondaryWeapon);
        // secondaryCombo.FlipUnusedWeapons();
        Debug.Log("Testing");
    }

}
