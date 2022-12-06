using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script is for handling the display of icons and openign of relevant pannels when player approach an object/itel
public class InteractableIcon : MonoBehaviour
{
    [SerializeField] private SceneManageNDisplay SceneMD;
    [SerializeField] private GameObject thisIcon;
    Animator iconAnim;

    void Start()
    {
        thisIcon.SetActive(false);
        iconAnim = thisIcon.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            if(!thisIcon.activeSelf){thisIcon.SetActive(true);}
        }
    }
    private void OnTriggerStay(Collider col) {
        if(col.gameObject.tag == "Player"){
            if(Input.GetKeyDown(KeyCode.Space)){
                SceneMD.DisplayMap();
            }
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            iconAnim.SetTrigger("disappear");
            thisIcon.SetActive(false);
        }
    }
    // private void IconDeactivate(){
    //     thisIcon.SetActive(false);
    // }
}
