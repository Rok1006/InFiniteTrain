using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using NaughtyAttributes;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

//This script handle wtever related to Player that is not related to topdown engine
public class PlayerManager : MonoBehaviour, MMEventListener<MMInventoryEvent>
{
    [Header("Assignment")]
    [SerializeField] private GameObject FrontMC;
    [SerializeField] private GameObject BackMC;
    [SerializeField] private GameObject dustPrefab; //the particle system: prefab
    [SerializeField] private GameObject emitPt; //the particle system: prefab
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject depthDetect;
    [SerializeField] private GameObject SpotLight; //assign the player spotlight obj
    [SerializeField] private GameObject PlayerLight;
    public GameObject AttackBlast;
    public bool facingFront = true;   //or side
    [HideInInspector] public List<GameObject> dust = new List<GameObject>();

    [ReadOnly]public Animator MCFrontAnim;
    [ReadOnly]public Animator MCBackAnim;
    [HideInInspector]public float oldPositionX = 0.0f;
    [HideInInspector]public float oldPositionZ = 0.0f;
    public bool down = true;
    [SerializeField] private MMF_Player combineFeedback;

//item using
    [ReadOnly, SerializeField, BoxGroup("Item Using")] private float currentActionTime = 0.0f, totalActionTime = 0.0f;
    [ReadOnly, SerializeField, BoxGroup("Item Using")] private bool isUsingItem = false;

    [SerializeField, Foldout("Item Functions")] private bool canSeeRadiationUI, canSeeMapEnemy, canSeeMetal, canSeeMetalAndMat, canCombineEnemyDetector, canCombineStunner, canCombineSignaler;
    [SerializeField, Foldout("Item Functions"), ReadOnly] private CanvasGroup RadiationUIGroup;

//mouse control------------
    [SerializeField, BoxGroup("Mouse Control")] private LayerMask TargetLayerMask;
    [SerializeField, BoxGroup("Mouse Control")] private GameObject throwDestination, plantDestination;

//ui
    [SerializeField, Foldout("UI")] private GameObject itemUsingContainer;
    [SerializeField, Foldout("UI")] private Image itemUsingBar;

//references------
    private TopDownController3D controller;
    private CharacterHandleWeapon handleWeapon, secondaryHandleWeapon;
    private PlayerWeaponController PWC;
    private CharacterOrientation2D ChOri_2D;
    private CharacterMovement _characterMovement;
    private Character _character;
    private Inventory _backpackInventory;

//weapons----
    [SerializeField] private WeaponCollection weaponCollection;

//getters & setters
    public bool IsUsingItem {get=>isUsingItem;set=>isUsingItem=value;}
    public float TotalActionTime {get=>totalActionTime;set=>totalActionTime=value;}
    public float CurrentActionTime {get=>currentActionTime;private set=> currentActionTime=value;}
    public bool CanCombineEnemyDetector {get=>canCombineEnemyDetector; set => canCombineEnemyDetector = value;}
    public bool CanCombineStunner {get=>canCombineStunner;}
    public MMF_Player CombineFeedback {get=>combineFeedback;}

    #region OnEnable,disable, Start & Updates
    void OnEnable()
    {
        this.MMEventStartListening<MMInventoryEvent>();
    }

    void OnDisable()
    {
        this.MMEventStopListening<MMInventoryEvent>();
    }

    
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MapPoint") 
        {
            PlayerLight.transform.position = new Vector3(PlayerLight.transform.position.x,PlayerLight.transform.position.y, PlayerLight.transform.position.z+2);
            SpotLight.SetActive(true);
        }else{
            SpotLight.SetActive(false);
            PlayerLight.transform.position = new Vector3(PlayerLight.transform.position.x,PlayerLight.transform.position.y, (PlayerLight.transform.position.z-0.8f));
        }
        
        MCFrontAnim = FrontMC.GetComponent<Animator>();
        MCBackAnim = BackMC.GetComponent<Animator>();
        if(FrontMC!=null){
            FrontMC.transform.localScale = new Vector3(1,1,1);
            BackMC.transform.localScale = new Vector3(0,0,0);
        }
        facingFront = true;
        playerCam = GameObject.FindGameObjectWithTag("PlayerCam");

        PWC = this.gameObject.GetComponent<PlayerWeaponController>();
        ChOri_2D = this.gameObject.GetComponent<CharacterOrientation2D>();

    //setting references up
        if (GetComponent<TopDownController3D>() != null)
            controller = GetComponent<TopDownController3D>();
        else    
            Debug.Log("Can't find top down controller 3d in " + name);
        
        if (GetComponent<CharacterMovement>() != null) {
            _characterMovement = GetComponent<CharacterMovement>();
        } else    
            Debug.Log("Can't find CharacterMovement in " + name);
        
        if (GetComponent<Character>() != null) {
            _character = GetComponent<Character>();
        } else
            Debug.Log("Can't find Character in " + name);
        
        if (Inventory.FindInventory("BackpackInventory", "Player1") != null) {
            _backpackInventory = Inventory.FindInventory("BackpackInventory", "Player1");
        } else
            Debug.Log("Can't find backpack inventory for " + name);

    //item functions
    GameObject radiationBar = GameObject.Find("RadiationBar");
    if (radiationBar != null)
        RadiationUIGroup = radiationBar.GetComponent<CanvasGroup>();
    if (RadiationUIGroup != null && !canSeeRadiationUI)
        RadiationUIGroup.alpha = 0;

    //weapons

        handleWeapon = GetComponent<CharacterHandleWeapon>();
        secondaryHandleWeapon = GetComponent<CharacterHandleSecondaryWeapon>();
        AttackBlast.SetActive(false);
    }
    private void Update() {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MapPoint") 
        {
            SpotLight.SetActive(true);
        }else{
            SpotLight.SetActive(false);
        }

        if (IsUsingItem) {
            if (CurrentActionTime < TotalActionTime) {
                CurrentActionTime += Time.deltaTime;
                itemUsingContainer.SetActive(true);
                itemUsingBar.fillAmount = CurrentActionTime / TotalActionTime;
            }
        } else {
            CurrentActionTime = 0;
            itemUsingContainer.SetActive(false);
            itemUsingBar.fillAmount = 0;
        }

        if(AttackBlast.activeSelf){
            Invoke("DisableAttackBlast", 1.5f);
        }
        /// get wut mouse is hover over
        // // Create a pointer event
        // PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        // // Set the pointer event position to the mouse position
        // pointerEventData.position = Input.mousePosition;

        // // Raycast using the pointer event
        // List<RaycastResult> results = new List<RaycastResult>();
        // EventSystem.current.RaycastAll(pointerEventData, results);

        // // Check if the mouse is hovering over a UI element
        // if (results.Count > 0)
        // {
        //     // Get the name of the UI object
        //     string objectName = results[0].gameObject.name;
        //     Debug.Log("Mouse is hovering over UI object named: " + objectName);
        // }
    }

    void FixedUpdate()
    {
        if (controller.InputMoveDirection.z <= 0 && FrontMC!=null) //Change player gameObject
        {
            facingFront = true;
            FrontMC.transform.localScale = new Vector3(1,1,1);
            BackMC.transform.localScale = new Vector3(0,0,0);
        }else if (controller.InputMoveDirection.z > 0 && FrontMC!=null)
        {
            facingFront = false;
            FrontMC.transform.localScale = new Vector3(0,0,0);
            BackMC.transform.localScale = new Vector3(1,1,1);
            DustEmit();
            DustLayerSort(1);
        }
        if (controller.InputMoveDirection.z < 0 && FrontMC!=null){
            DustEmit();
            DustLayerSort(-1);
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
        
        if(Input.GetKey(KeyCode.S)){
            down = true;
            MCFrontAnim.SetBool("DefaultBigGunPos", true);
        }else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            down = false;
            MCFrontAnim.SetBool("DefaultBigGunPos", false);
        }

     //Combat Related----------
        // if(Input.GetKeyDown(KeyCode.J)){ //
        //     //ChOri_2D.enabled = false;
        //     PWC.currentGunType = PlayerWeaponController.GunType.SMALLGUN;
        //     handleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[0], weaponCollection.MeleeWeapons[0].WeaponName, false);
        //     secondaryHandleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[1], weaponCollection.MeleeWeapons[1].WeaponName, false);
        //     MCFrontAnim.SetBool("IsUsingWeapon", true);
        //     DisableAllWeaponAnimation();
        //     MCFrontAnim.SetBool("UseSmallGun", true);
        //     MCBackAnim.SetBool("UseSmallGun", true);
        //     PWC.canRotate = true;
        // }
        // if(Input.GetKeyDown(KeyCode.K)){ //
        //     //ChOri_2D.enabled = false;
        //     PWC.currentGunType = PlayerWeaponController.GunType.BIGGUN;
        //     handleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[0], weaponCollection.MeleeWeapons[0].WeaponName, false);
        //     secondaryHandleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[1], weaponCollection.MeleeWeapons[1].WeaponName, false);
        //     MCFrontAnim.SetBool("IsUsingWeapon", true);
        //     DisableAllWeaponAnimation();
        //     MCFrontAnim.SetBool("UseBigGun", true);
        //     MCBackAnim.SetBool("UseBigGun", true);
        //     PWC.canRotate = true;
        // }
        if(Input.GetKeyDown(KeyCode.L)){ //need to release bone constrains
            // PWC.ResetBones();
            //ChOri_2D.enabled = true;
            Debug.Log("play"); //for some reason the animation is trigger many time even after just click once
            handleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[0], weaponCollection.MeleeWeapons[0].WeaponName, false);
            secondaryHandleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[1], weaponCollection.MeleeWeapons[1].WeaponName, false);
            MCFrontAnim.SetBool("IsUsingWeapon", true);
            DisableAllWeaponAnimation();
            MCFrontAnim.SetTrigger("UseSmallSword");
            MCBackAnim.SetTrigger("UseSmallSword");
        }
    }
    #endregion

    public void takeOutSword() {
        handleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[0], weaponCollection.MeleeWeapons[0].WeaponName, false);
        // secondaryHandleWeapon.ChangeWeapon(weaponCollection.MeleeWeapons[1], weaponCollection.MeleeWeapons[1].WeaponName, false);
        MCFrontAnim.SetBool("IsUsingWeapon", true);
        DisableAllWeaponAnimation();
        MCFrontAnim.SetTrigger("UseSmallSword");
        // MCBackAnim.SetTrigger("UseSmallSword");
    }

#region dust
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
    public void DisableAttackBlast(){
        AttackBlast.SetActive(false);
    }
#endregion

    void OnTriggerEnter(Collider col) {

    }
    public void DisableAllWeaponAnimation(){
        MCFrontAnim.SetBool("Switch_smallGun", false);
        MCFrontAnim.SetBool("Switch_bigGun", false);
        MCFrontAnim.SetBool("Switch_bigSword", false);
        // MCBackAnim.SetBool("Switch_smallGun", false);
        // MCBackAnim.SetBool("Switch_bigGun", false);
        // MCBackAnim.SetBool("Switch_bigSword", false);
    }

    public void test() {
        Debug.Log("Testing");
    }

    public IEnumerator MovementBoost(float lastingTime, float boostValue, float originalMovementSpeed) {
        _characterMovement.WalkSpeed = _characterMovement.WalkSpeed + boostValue;
        _characterMovement.ResetSpeed();
        yield return new WaitForSeconds(lastingTime);
        _characterMovement.WalkSpeed = originalMovementSpeed;
        _characterMovement.ResetSpeed();
    }

    public void RestrictMovement() {
        // _characterMovement.SetMovement(Vector2.zero);
        _character.Freeze();
        _characterMovement.AbilityPermitted = false;
    }

    public void ReleaseMovement() {
        _character.UnFreeze();
        _characterMovement.AbilityPermitted = true;
    }

    public virtual GameObject CreateThrowDestination()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return null;


        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0)) return null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    #if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    #endif
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, TargetLayerMask))
        {
            GameObject Des = Instantiate(throwDestination, Vector3.zero, Quaternion.identity);
            Des.transform.position = hitInfo.point;
            return Des;
        } else {
            Physics.Raycast(ray, out var hitInfo1, Mathf.Infinity, Physics.AllLayers);
            Debug.Log("hitted " + hitInfo1.transform.gameObject.name);
        }
        return null;
    }

    public virtual GameObject CreatePlantDestination()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return null;

        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0)) return null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    #if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    #endif
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, TargetLayerMask))
        {   
            GameObject Des = Instantiate(plantDestination, Vector3.zero, Quaternion.identity);
            Des.transform.position = hitInfo.point;
            return Des;
        }
        return null;
    }

    #region item functions
    public virtual void OnMMEvent(MMInventoryEvent inventoryEvent)
    {
        if (_backpackInventory == null)
            return;

        canSeeRadiationUI = canSeeMapEnemy = canSeeMetal = canSeeMetalAndMat = false;

        foreach (InventoryItem item in _backpackInventory.Content) {
            if (item == null)
                continue;
            if (item.ItemName.Equals("Metal Detector"))
                canSeeMetal = true;
            else if (item.ItemName.Equals("Multi-Use Detector"))
                canSeeMetalAndMat = true;
            else if (item.ItemName.Equals("Radiation Detector"))
                canSeeRadiationUI = true;
            else if (item.ItemID.Equals("Stunner_Blueprint"))
                canCombineStunner = true;
            else if (item.ItemID.Equals("Signaler_Blueprint"))
                canCombineSignaler = true;
            else if (item.ItemID.Equals("Radiation_Eliminator_Blueprint"))
                canCombineEnemyDetector = true;
            
        }

        if (canSeeRadiationUI)
            StartShowingRadiationUI();
        else
            EndShowingRadiationUI();

        if (canSeeMetal)
            StartDetectMetal();
        else
            EndDetectMetal();
        
        if (canSeeMetalAndMat)
            StartDetectMetalAndMat();
        else if (canSeeMetalAndMat && !canSeeMetal)
            EndDetectMetalAndMat();
    }

    public void StartDetectMetal() {
        foreach (ResourceBox box in FindObjectsOfType<ResourceBox>()) {
            if (!box.IsLocked)
                continue;
            foreach (InventoryItem item in Inventory.FindInventory(box.InventoryName, "Player1").Content) {
                if (item as MechanismItem != null) {
                    box.MetalIcon.SetActive(true);
                    break;
                }
            }
        }
    }

    public void EndDetectMetal() {
        foreach (ResourceBox box in FindObjectsOfType<ResourceBox>()) {
            if (!box.IsLocked)
                continue;
            box.MetalIcon.SetActive(false);
        }
    }

    public void StartDetectMetalAndMat() {
        foreach (ResourceBox box in FindObjectsOfType<ResourceBox>()) {
            if (!box.IsLocked)
                continue;
            foreach (InventoryItem item in Inventory.FindInventory(box.InventoryName, "Player1").Content) {
                if (item as MechanismItem != null) {
                    box.MetalIcon.SetActive(true);
                }
                if (item as MaterialItem != null) {
                    box.MaterialIcon.SetActive(true);
                }
            }
        }
    }

    public void EndDetectMetalAndMat() {
        foreach (ResourceBox box in FindObjectsOfType<ResourceBox>()) {
            if (!box.IsLocked)
                continue;
            box.MetalIcon.SetActive(false);
            box.MaterialIcon.SetActive(false);
        }
    }

    public void StartShowingRadiationUI() {
        RadiationUIGroup.alpha = 1;
    }

    public void EndShowingRadiationUI() {
        RadiationUIGroup.alpha = 0;
    }
    #endregion
}
