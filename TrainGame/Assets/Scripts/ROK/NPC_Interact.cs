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
    }


    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            if(!InteractIcon.activeSelf){InteractIcon.SetActive(true);}
            if(!TrainInfoGuide.activeSelf){TrainInfoGuide.SetActive(true);}
            if(Input.GetKeyUp(input_interact)&&state==1){
                Debug.Log("merchang");
                //Player.GetComponent<Character>().enabled = false;
                dr.onDialogueComplete.AddListener(FirstTalkEnd);
                dr.StartDialogue("Merchant First");
                iconAnim.SetTrigger("disappear");
            }
            
        }
    }
    public void FirstTalkEnd(){
        state = 2;
        Player.GetComponent<Character>().enabled = true;
    }

    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            TrainInfoGuide.SetActive(false);
            iconAnim.SetTrigger("disappear");
            InteractIcon.SetActive(false);
        }
    }
}
