using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MPTSceneManager : MonoBehaviour
{

    private PlayerInformation playerInfo;
    [SerializeField, BoxGroup("Effect")]private GameObject ScreenEffect;
    public Animator SeAnim;

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInformation>();
        if (playerInfo == null){Debug.Log("player info is null");}
        // SEAnim = ScreenEffect.GetComponent<Animator>();
        ScreenEffect.SetActive(true);
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
    }

    // public void RadiationWarning(){
    //     ScreenEffect.SetActive(true);
    //     SeAnim.SetTrigger("appear");
    // }
    // public void RadiationWarningOff(){
    //     ScreenEffect.SetActive(false);
    //     SeAnim.SetTrigger("disappear");
    // }
    
}
