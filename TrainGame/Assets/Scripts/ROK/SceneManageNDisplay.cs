using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.InventoryEngine;
//THis script is for function of pannels and triggering events
public class SceneManageNDisplay : MonoBehaviour
{
    [Header("Train Info")]
    public string currentCartName;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private GameObject InfoDisplay;
    //[SerializeField] private GameObject TrainINfoGuide;

    [Header("InteractableMap")]
    [SerializeField] private GameObject mapCam;
    [SerializeField] private GameObject mapIcon;
    [SerializeField] private GameObject theMap;
    [SerializeField] private Vector3 mapFuelLocation;
    public bool PanelOn = false;
    // public InventoryInputManager IIM_Fuel;

    // public List<GameObject> PopUpPoint = new List<GameObject>();

    [Header("FuelMachine")]
    [SerializeField] private GameObject FF_Panel; //the whole ui panel
    [SerializeField] private Button FF_CloseButton;
    [SerializeField] private GameObject FF_MachineLever;
    [SerializeField] private GameObject TrainFuelBar; //this will also appear in map scene
    [SerializeField] private Vector3 FuelMachineFuelLocation;
    [SerializeField] private GameObject TrainInfoGuide;

//[HideInInspector]

    void Start()
    {
        InfoDisplay.SetActive(false);
        mapIcon.SetActive(false);
        theMap.SetActive(false);
        mapCam.SetActive(false);
        TrainFuelBar.SetActive(false);
        FF_Panel.SetActive(false);
        TrainInfoGuide.SetActive(false);
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
        FF_Panel.SetActive(false);
        TrainFuelBar.SetActive(false);
        //TrainInfoGuide.SetActive(false);
    }
    public void Open_FuelPanel(){
        FF_Panel.SetActive(true);
        FF_Panel.GetComponent<InventoryInputManager>().OpenInventory();
        TrainFuelBar.GetComponent<RectTransform>().anchoredPosition = FuelMachineFuelLocation;
        TrainFuelBar.SetActive(true);
        TrainInfoGuide.SetActive(false);
    }


}
