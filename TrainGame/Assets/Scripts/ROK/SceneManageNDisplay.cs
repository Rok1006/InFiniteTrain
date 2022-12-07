using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneManageNDisplay : MonoBehaviour
{
    [Header("Train Info")]
    public string currentCartName;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private GameObject InfoDisplay;

    [Header("InteractableMap")]
    [SerializeField] private GameObject mapCam;
    [SerializeField] private GameObject mapIcon;
    [SerializeField] private GameObject theMap;


    void Start()
    {
        InfoDisplay.SetActive(false);
        mapIcon.SetActive(false);
        theMap.SetActive(false);
        mapCam.SetActive(false);
    }
    void Update()
    {
        roomName.text = '"' + " " + currentCartName.ToString() + " " + '"';
    }
    public void DisplayCartName(){
        InfoDisplay.SetActive(false);
        InfoDisplay.SetActive(true);
    }
    public void DisplayMapIcons(){
        mapIcon.SetActive(false);
        mapIcon.SetActive(true);
    }
    public void DisplayMap(){
        mapIcon.SetActive(false);
        theMap.SetActive(true);
        Invoke("MapCamSwitch",1f);
    }
    void MapCamSwitch(){
        mapCam.SetActive(true);
    }
    public void CloseMap(){
        theMap.SetActive(false);
        mapCam.SetActive(false);
    }
}
