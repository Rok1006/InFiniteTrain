using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using MoreMountains.TopDownEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class RadiationManager : MMMonoBehaviour
{
    private int currentRadiationLevel = 0;
    [HorizontalLine(color: EColor.Red)]
    [Foldout("Level 0"), Label("Enter State")] public UnityEvent enterRad0;
    [Foldout("Level 0"), Label("Update State")] public UnityEvent updateRad0;
    [Foldout("Level 0"), Label("Leave State")] public UnityEvent leaveRad0;
    [HorizontalLine(color: EColor.Red)]
    [Foldout("Level 1"), Label("Enter State")] public UnityEvent enterRad1;
    [Foldout("Level 1"), Label("Update State")] public UnityEvent updateRad1;
    [Foldout("Level 1"), Label("Leave State")] public UnityEvent leaveRad1;
    [HorizontalLine(color: EColor.Red)]
    [Foldout("Level 2"), Label("Enter State")] public UnityEvent enterRad2;
    [Foldout("Level 2"), Label("Update State")] public UnityEvent updateRad2;
    [Foldout("Level 2"), Label("Leave State")] public UnityEvent leaveRad2;

    [ShowNonSerializedField] private Health playerHealth;
    [SerializeField, BoxGroup("UI")] string RadiationBarName;
    [ShowNonSerializedField, BoxGroup("UI")] MMProgressBar radiationBar;
    [SerializeField] private bool isRadiated = false;
    private PlayerInformation playerInfo;


    //getters & setters
    public int CurrentRadiationLevel {get=>currentRadiationLevel; set=>currentRadiationLevel = value;}
    public Health PlayerHealth {get=>playerHealth; set=>playerHealth=value;}
    public bool IsRadiated {get=>isRadiated; set=>isRadiated=value;}

    //FSM
    private RadiationStateBase currentState;
    public RadiationState0 radiationstate0 = new RadiationState0();
    public RadiationState1 radiationstate1 = new RadiationState1();
    public RadiationState2 radiationstate2 = new RadiationState2();

    public void ChangeState(RadiationStateBase newState)
    {
        if (currentState != newState) {
            if (currentState != null)
            {
                currentState.LeaveState(this);
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.EnterState(this);
            }
        }
    }

    void Start()
    {
        currentState = radiationstate0;

        SceneManager.sceneLoaded += OnSceneLoaded;
        playerInfo = FindObjectOfType<PlayerInformation>();
        PlayerHealth = FindObjectOfType<PlayerManager>().GetComponent<Health>();
        radiationBar = GameObject.Find(RadiationBarName).GetComponent<MMProgressBar>();
    }

    void Update()
    {
        currentState.UpdateState(this);  

        if (playerInfo == null)
            playerInfo = FindObjectOfType<PlayerInformation>();
        if (playerHealth == null)
            PlayerHealth = FindObjectOfType<PlayerManager>().GetComponent<Health>();
        if (radiationBar == null)
            radiationBar = GameObject.Find(RadiationBarName).GetComponent<MMProgressBar>();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeState(radiationstate1);

        if (IsRadiated) {
            playerInfo.CurrentRadiationValue += 0.3f * Time.deltaTime;
        }

        radiationBar.UpdateBar(playerInfo.CurrentRadiationValue, playerInfo.MinRadiationValue, playerInfo.MaxRadiationValue);

        if (playerInfo.CurrentRadiationValue >= playerInfo.MaxRadiationValue) {
            playerHealth.Damage(10000f, this.gameObject, 0, 0, Vector3.up, null);
            radiationBar.gameObject.SetActive(false);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        StartCoroutine(waitToRefind());
    }

    public void showRadiationLevel(string str) {
        Debug.Log(str);
    }

    public IEnumerator waitToRefind() {
        yield return new WaitForSeconds(1.0f);
        PlayerHealth = FindObjectOfType<PlayerManager>().GetComponent<Health>();
        radiationBar = GameObject.Find(RadiationBarName).GetComponent<MMProgressBar>();
    }
}
