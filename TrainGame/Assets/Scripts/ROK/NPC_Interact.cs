using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using MoreMountains.TopDownEngine;

public class NPC_Interact : MonoBehaviour
{
    public DialogueRunner dr;
    private GameObject Player;
    [SerializeField, BoxGroup("General")] private string PanelName;
    [SerializeField, BoxGroup("General")] private KeyCode input_interact;
    [SerializeField, BoxGroup("UI")] private GameObject InteractIcon;
    [SerializeField, BoxGroup("UI")] private GameObject TrainInfoGuide;
    [SerializeField] private Sprite smallIcon_sp; //the sprite of small icon
    [SerializeField] private Image smallIconImageObj;  
    [SerializeField] private TextMeshProUGUI guideDescriptTextObj;
    public string guideDescript;
    Animator iconAnim;

    public int state = 1;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InteractIcon.SetActive(false);
        TrainInfoGuide.SetActive(false);
        guideDescriptTextObj.text = "Talk";
        iconAnim = InteractIcon.GetComponent<Animator>();
    }


    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider col) {
        if(col.gameObject.tag == "Player"){
            if(!InteractIcon.activeSelf){InteractIcon.SetActive(true);}
            if(!TrainInfoGuide.activeSelf){TrainInfoGuide.SetActive(true);}
            //Debug.Log("premerchant");
            if(Input.GetKeyUp(input_interact)&&state==1){
                //Debug.Log("merchang");
                //Player.GetComponent<Character>().enabled = false;
                dr.onDialogueComplete.AddListener(FirstTalkEnd);
                dr.StartDialogue("Merchant_First");
                iconAnim.SetTrigger("disappear");
                //InteractIcon.SetActive(false);
            }
            if(Input.GetKeyUp(input_interact)&&state==2){
                //Debug.Log("merchang");
                //Player.GetComponent<Character>().enabled = false;
                dr.onDialogueComplete.AddListener(SecondTalkEnd);
                dr.StartDialogue("Merchant_Second");
                iconAnim.SetTrigger("disappear");
                //InteractIcon.SetActive(false);
            }
            
        }
    }
    public void FirstTalkEnd(){
        state = 2;
        //Player.GetComponent<Character>().enabled = true;
    }
    public void SecondTalkEnd(){
        state = 2;
    }

    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            TrainInfoGuide.SetActive(false);
            iconAnim.SetTrigger("disappear");
            InteractIcon.SetActive(false);
        }
    }
}
