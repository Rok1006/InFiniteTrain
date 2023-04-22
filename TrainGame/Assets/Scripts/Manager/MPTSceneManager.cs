using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using TMPro;

public class MPTSceneManager : MonoBehaviour
{

    private PlayerInformation playerInfo;
    private Info InfoSC;
    [SerializeField, BoxGroup("Effect")]private GameObject ScreenEffect;
    [SerializeField, BoxGroup("Effect")] public Animator SeAnim;

    [SerializeField, BoxGroup("Others")] private GameObject MP_deadlyTimer;
    [SerializeField, BoxGroup("Others")] private TextMeshProUGUI timeCountDown;

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInformation>();
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
        if (playerInfo == null){Debug.Log("player info is null");}
        // SEAnim = ScreenEffect.GetComponent<Animator>();
        ScreenEffect.SetActive(true);
        if(InfoSC.DeadCountDownStart){
            MP_deadlyTimer.SetActive(true);  
        }else{
            MP_deadlyTimer.SetActive(false);  
        }
        
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
        if(InfoSC.DeadCountDownStart){
            if(InfoSC.DeadTime>0){
                InfoSC.DeadTime-=Time.deltaTime;
            }else{
                //InfoSC.DeadTime+=DeadCounterTime;
                //SMD.GameOverScreen.SetActive(true);
                //InfoSC.DeadCountDownStart = false;
            }
        }
        EnemyDeadlyCountDownDisplay(InfoSC.DeadTime);
    }
    public void EnemyDeadlyCountDownDisplay(float displayTime){//If enemy is one unit away frm player
        if(displayTime<0){
            displayTime = 0;
        }
        float minutes = Mathf.FloorToInt(displayTime/60);
        float seconds = Mathf.FloorToInt(displayTime%60);
        timeCountDown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
}
