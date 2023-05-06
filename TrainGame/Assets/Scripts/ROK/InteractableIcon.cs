using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
//This script is for handling the display of icons and openign of relevant pannels when player approach an object/itel
public class InteractableIcon : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private string PanelName;
    [SerializeField] private KeyCode input_interact;

    [SerializeField] private SceneManageNDisplay SceneMD;
    [SerializeField] private GameObject thisPanel;
    [SerializeField] private GameObject thisIcon; //icon on every interactable, no function
    [SerializeField] private GameObject TrainInfoGuide;
    [SerializeField] private Image smallIconImageObj;  
    [SerializeField] private TextMeshProUGUI guideDescriptTextObj;
    public string guideDescript;
    private string currentAccess;
    [SerializeField] private Sprite smallIcon_sp;
    Animator iconAnim;
    public bool PanelOn = false;

    public UnityEvent DisplayFunction_Active;   //wtever On call
    public UnityEvent DisplayFunction_DeActive; //wtever off call
    public UnityEvent ActionCall; //wtever off call
    public UnityEvent TrainInfoGuideCall; //wtever off call

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        thisIcon.SetActive(false);
        iconAnim = thisIcon.GetComponent<Animator>();
        TrainInfoGuide.SetActive(false);
        SceneMD = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManageNDisplay>();
    }

    void Update()
    {

        if(SceneMD.PanelOn||Info.Instance.IsViewingInventory){
            TrainInfoGuide.SetActive(false);
        }
        guideDescriptTextObj.text = SceneMD.currentAccess.ToString();
    }

  

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Player"){
            SceneMD.currentAccess = this.guideDescript;
            //Debug.Log(this.guideDescript);
            smallIconImageObj.sprite = smallIcon_sp;
        }
    }
    private void OnTriggerStay(Collider col) {
        if(col.gameObject.tag == "Player"){
            if(!thisIcon.activeSelf){thisIcon.SetActive(true);}
            if(!TrainInfoGuide.activeSelf&&!SceneMD.PanelOn){TrainInfoGuide.SetActive(true);}
            //TrainInfoGuideCall.Invoke();  //need more edit
            if(Input.GetKeyUp(input_interact)&&!SceneMD.PanelOn){   
                // Player.GetComponent<PlayerManager>().MCFrontAnim.SetTrigger("Think");
                DisplayFunction_Active.Invoke();
                ActionCall.Invoke();
                iconAnim.SetTrigger("disappear");
                //TrainInfoGuide.SetActive(false);
                //Debug.Log("yes");
            }
        }
    }
    private void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "Player"){
            SceneMD.PanelOn = false;
            TrainInfoGuide.SetActive(false);
            iconAnim.SetTrigger("disappear");
            thisIcon.SetActive(false);
            DisplayFunction_DeActive.Invoke();
            Debug.Log("no");
        }
    }
    public void IconActive()
    {
        thisIcon.SetActive(true);
    }
    // private void IconDeactivate(){
    //     thisIcon.SetActive(false);
    // }
}
