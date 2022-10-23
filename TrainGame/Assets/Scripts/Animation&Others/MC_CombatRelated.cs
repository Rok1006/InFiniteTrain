using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
//This script deals with mixing animation and switching weapon skins
public class MC_CombatRelated : MonoBehaviour
{
    [SerializeField] private GameObject FrontPlayer;
    [SerializeField] private GameObject BackPlayer;
    Animator MCFrontAnim;
    Animator MCBackAnim;
    // [SpineAnimation] public string animation;
    // SkeletonMecanim _skeletonMecanim;

    void Start()
    {
        MCFrontAnim = FrontPlayer.GetComponent<Animator>();
        MCBackAnim = BackPlayer.GetComponent<Animator>();
    }

    void Update()
    {
//Testing-----
        if(Input.GetKeyDown(KeyCode.J)){ //
            DisableAllAnimation();
            MCFrontAnim.SetBool("Switch_smallGun", true);
            MCBackAnim.SetBool("Switch_smallGun", true);
        }
        if(Input.GetKeyDown(KeyCode.K)){ //
            DisableAllAnimation();
            MCFrontAnim.SetBool("Switch_bigGun", true);
            MCBackAnim.SetBool("Switch_bigGun", true);
        }
        if(Input.GetKeyDown(KeyCode.L)){ //
            DisableAllAnimation();
            MCFrontAnim.SetBool("Switch_bigSword", true);
            MCBackAnim.SetBool("Switch_bigSword", true);
        }
    }
    public void DisableAllAnimation(){
        MCFrontAnim.SetBool("Switch_smallGun", false);
        MCFrontAnim.SetBool("Switch_bigGun", false);
        MCFrontAnim.SetBool("Switch_bigSword", false);
        MCBackAnim.SetBool("Switch_smallGun", false);
        MCBackAnim.SetBool("Switch_bigGun", false);
        MCBackAnim.SetBool("Switch_bigSword", false);
    }
}
