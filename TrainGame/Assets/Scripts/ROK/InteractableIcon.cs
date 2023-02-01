using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
//This script is for handling the display of icons and openign of relevant pannels when player approach an object/itel
public class InteractableIcon : MonoBehaviour
{
    [SerializeField] private string PanelName;
    [SerializeField] private KeyCode input_interact;

    [SerializeField] private SceneManageNDisplay SceneMD;
    [SerializeField] private GameObject thisPanel;
    [SerializeField] private GameObject thisIcon; //icon on every interactable, no function
    [SerializeField] private GameObject TrainInfoGuide;
    [SerializeField] private Image smallIcon;  
    [SerializeField] private TextMeshProUGUI G_text;
    [SerializeField] private string guideDescript;
    [SerializeField] private Sprite smallIcon_sp;
    Animator iconAnim;

    public UnityEvent DisplayFunction_Active;
    public UnityEvent DisplayFunction_DeActive;

    void Start()
    {
        thisIcon.SetActive(false);
        iconAnim = thisIcon.GetComponent<Animator>();
        //guideDescript = " ";
        TrainInfoGuide.SetActive(false);
        //thisPanel.SetActive(false);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            G_text.text = guideDescript.ToString();
            smallIcon.sprite = smallIcon_sp;
        }
    }
    private void OnTriggerStay(Collider col) {
        if(col.gameObject.tag == "Player"){
            if(!thisIcon.activeSelf){thisIcon.SetActive(true);}
            if(!TrainInfoGuide.activeSelf){TrainInfoGuide.SetActive(true);}

            if(Input.GetKeyDown(input_interact)){
                DisplayFunction_Active.Invoke();
                iconAnim.SetTrigger("disappear");
            }
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            TrainInfoGuide.SetActive(false);
            iconAnim.SetTrigger("disappear");
            thisIcon.SetActive(false);
            DisplayFunction_DeActive.Invoke();
        }
    }
    // private void IconDeactivate(){
    //     thisIcon.SetActive(false);
    // }
}
