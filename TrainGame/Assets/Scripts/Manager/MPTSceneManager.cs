using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MPTSceneManager : MonoBehaviour
{

    private PlayerInformation playerInfo;
    [SerializeField, BoxGroup("Effect")]private GameObject ScreenEffect;
    Animator SEAnim;

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerInformation>();
        if (playerInfo == null) Debug.Log("player info is null");
        SEAnim = ScreenEffect.GetComponent<Animator>();
        ScreenEffect.SetActive(false);
    }

    void FixedUpdate()
    {
        if (playerInfo.CurrentRadiationValue >= playerInfo.MaxRadiationValue/4.0f*3.0f) {
            RadiationWarning();
        }else{
            RadiationWarningOff();
        }
    }

    public void RadiationWarning(){
        ScreenEffect.SetActive(true);
        SEAnim.SetTrigger("appear");
    }
    public void RadiationWarningOff(){
        ScreenEffect.SetActive(false);
        SEAnim.SetTrigger("disappear");
    }
    
}
