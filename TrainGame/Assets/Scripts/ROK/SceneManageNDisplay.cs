using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.InventoryEngine;
using NaughtyAttributes;
//THis script is for function of pannels and triggering events
public class SceneManageNDisplay : MonoBehaviour
{
    [SerializeField, BoxGroup("General")] private GameObject TrainInfoGuide;
    [SerializeField, BoxGroup("General")] private TextMeshProUGUI TrainInfoGuide_Text;

    public string currentCartName;
    [SerializeField, BoxGroup("Train Info")] private TextMeshProUGUI roomName;
    [SerializeField, BoxGroup("Train Info")] private GameObject InfoDisplay;
    //[SerializeField] private GameObject TrainINfoGuide;

    [SerializeField, BoxGroup("InteractableMap")] private GameObject mapCam;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject mapIcon;
    [SerializeField, BoxGroup("InteractableMap")] private GameObject theMap;
    [SerializeField, BoxGroup("InteractableMap")] private Vector3 mapFuelLocation;
    public bool PanelOn = false;
    // public InventoryInputManager IIM_Fuel;

    // public List<GameObject> PopUpPoint = new List<GameObject>();

    [SerializeField, BoxGroup("FuelMachine")] private GameObject FF_Panel; //the whole ui panel
    [SerializeField, BoxGroup("FuelMachine")] private Button FF_CloseButton;
    [SerializeField, BoxGroup("FuelMachine")] private GameObject FF_MachineLever;
    [SerializeField, BoxGroup("FuelMachine")] private GameObject TrainFuelBar; //this will also appear in map scene
    [SerializeField, BoxGroup("FuelMachine")] private Vector3 FuelMachineFuelLocation;

    [SerializeField, BoxGroup("TrainMoveStop")] private GameObject Lever; //still a placeholder
    Animator leverAnim;
    [SerializeField, BoxGroup("TrainMoveStop")] private bool IsOn; //train will move, else it stop
    [ReadOnly] public string currentMessage;


//[HideInInspector]

    void Start()
    {
        InfoDisplay.SetActive(false);
        mapIcon.SetActive(false);
        theMap.SetActive(false);
        mapCam.SetActive(false);
        TrainFuelBar.SetActive(false);
        // FF_Panel.SetActive(false);
        TrainInfoGuide.SetActive(false);
        IsOn = false;
        leverAnim = Lever.GetComponent<Animator>();
//Listener ---
        FF_CloseButton.onClick.AddListener(Close_FF);
    }
    void Update()
    {
        roomName.text = '"' + " " + currentCartName.ToString() + " " + '"';

        if(theMap.activeSelf&&PanelOn){
            if(Input.GetKeyDown(KeyCode.Space)){
                CloseMap();
                PanelOn = false;
            }
        }
    }
    public void DisplayCartName(){
        InfoDisplay.SetActive(false);
        InfoDisplay.SetActive(true);
    }
// The Map Part ---------
    public void DisplayMapIcons(){
        mapIcon.SetActive(false);
        mapIcon.SetActive(true);
    }
    public void Open_Map(){
        mapIcon.SetActive(false);
        theMap.SetActive(true);
        Invoke("MapCamSwitch",.5f);
        Invoke("ChangePos",1f);
        TrainInfoGuide.SetActive(false);
    }
    void ChangePos(){
        TrainFuelBar.GetComponent<RectTransform>().anchoredPosition = mapFuelLocation;
        TrainFuelBar.SetActive(true);
    }
    void MapCamSwitch(){
        PanelOn = true;
        mapCam.SetActive(true);
    }
    public void CloseMap(){
        theMap.SetActive(false);
        mapCam.SetActive(false);
        TrainFuelBar.SetActive(false);
    }
//Fuel Machine Part ---------
    public void Close_FF(){
        FF_Panel.GetComponent<InventoryInputManager>().CloseInventory();
        TrainFuelBar.SetActive(false);
        //TrainInfoGuide.SetActive(false);
    }
    public void Open_FuelPanel(){
        PanelOn = true;
        // FF_Panel.SetActive(true);
        FF_Panel.GetComponent<InventoryInputManager>().OpenInventory();
        
        TrainFuelBar.GetComponent<RectTransform>().anchoredPosition = FuelMachineFuelLocation;
        TrainFuelBar.SetActive(true);
        TrainInfoGuide.SetActive(false);
    }
//Train Move Stop Toggle
    public void OpenToggleGuide(){
        TrainInfoGuide.SetActive(true);
    }
    public void CloseToggleGuide(){  //put this in actionCall
        TrainInfoGuide.SetActive(false);
    }
    public void PullLever(){  //put this in actionCall
        if(IsOn){ //make the train stop
            Debug.Log("Train is gonna stop");
            leverAnim.SetTrigger("Off");
            currentMessage = "S T A R T  T R A I N";
                IsOn = false;
            
            //some enviromental change trigger: access to camera, plau audio, some foregrd backgrd
        }else if(!IsOn){ //make the train move
            Debug.Log("Train is gonna move");
            leverAnim.SetTrigger("On");
            currentMessage = "S T O P  T R A I N";
                IsOn = true;
            //some enviromental change trigger: access to camera, plau audio
        }
    }


}
