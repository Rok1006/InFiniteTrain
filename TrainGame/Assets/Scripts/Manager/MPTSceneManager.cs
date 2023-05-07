using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class MPTSceneManager : MonoBehaviour
{

    public static int state = 0;
    public PlayerInformation playerInfo;
    private PointContentManager PCM;
    [SerializeField, BoxGroup("Effect")]private GameObject ScreenEffect;
    [SerializeField, BoxGroup("Effect")] public Animator SeAnim;

    [SerializeField, BoxGroup("Others")] private GameObject MP_deadlyTimer;
    [SerializeField, BoxGroup("Others")] private TextMeshProUGUI timeCountDown;

    [SerializeField, BoxGroup("General")] private GameObject LocationInfoDisplay;
    [SerializeField, BoxGroup("General")] private TextMeshProUGUI LocationInfoDisplay_Text;

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInformation>();
        PCM = FindObjectOfType<PointContentManager>();
        if (playerInfo == null){Debug.Log("player info is null");}
        // SEAnim = ScreenEffect.GetComponent<Animator>();
        ScreenEffect.SetActive(true);
        Invoke("DeadlyTimerOnOFF",.5f);
        // if(Info.Instance.DeadCountDownStart){
        //     MP_deadlyTimer.SetActive(true);  
        // }else{
        //     MP_deadlyTimer.SetActive(false);  
        // }
        LocationInfoDisplay.SetActive(false);

        DisplayLocationInfo(PCM.PointScene[Info.Instance.pointID].GetComponent<PointContent>().LandTitle);
    }

    void FixedUpdate()
    {
        if(playerInfo.CurrentRadiationValue < playerInfo.MaxRadiationValue/4.0f*2.0f) { //Low
            SeAnim.SetTrigger("Low");
            SeAnim.SetBool("Medium", false);
            SeAnim.SetBool("High", false);
        }else if(playerInfo.CurrentRadiationValue >= playerInfo.MaxRadiationValue/4.0f*2.0f&&playerInfo.CurrentRadiationValue <= playerInfo.MaxRadiationValue/4.0f*3.0f){ //medium
            SeAnim.SetTrigger("Medium");
            SeAnim.SetBool("Low", false);
            SeAnim.SetBool("High", false);
        }else if(playerInfo.CurrentRadiationValue > playerInfo.MaxRadiationValue/4.0f*3.0f){ //high
            SeAnim.SetTrigger("High");
            SeAnim.SetBool("Low", false);
            SeAnim.SetBool("Medium", false);
        }
        // else if(playerInfo.CurrentRadiationValue > playerInfo.MaxRadiationValue){
        //     ScreenEffect.SetActive(false);  
        // }
    }
    void Update() {
        if(playerInfo.CurrentRadiationValue > playerInfo.MaxRadiationValue){
            ScreenEffect.SetActive(false);  
        }
        if(Info.Instance.DeadCountDownStart){
            if(Info.Instance.DeadTime>0){
                Info.Instance.DeadTime-=Time.deltaTime;
            }else{
                //Info.Instance.DeadTime+=DeadCounterTime;
                //SMD.GameOverScreen.SetActive(true);
                //Info.Instance.DeadCountDownStart = false;
            }
        }
        EnemyDeadlyCountDownDisplay(Info.Instance.DeadTime);
    }
    public void EnemyDeadlyCountDownDisplay(float displayTime){//If enemy is one unit away frm player
        if(displayTime<0){
            displayTime = 0;
        }
        float minutes = Mathf.FloorToInt(displayTime/60);
        float seconds = Mathf.FloorToInt(displayTime%60);
        timeCountDown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DisplayLocationInfo(string location){
        LocationInfoDisplay_Text.text = location.ToString();
        LocationInfoDisplay.SetActive(true);
    }
    void DeadlyTimerOnOFF(){
        if(Info.Instance.DeadCountDownStart){
            MP_deadlyTimer.SetActive(true);  
        }else{
            MP_deadlyTimer.SetActive(false);  
        }
    }
    
}
