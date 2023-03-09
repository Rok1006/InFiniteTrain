using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
//This script is for picking out message to display when ever sth is not done by player
//create a switching state that diff ones will display different message
public class WarningGuide : MonoBehaviour
{
   public int index;
   [SerializeField, BoxGroup("General")] private GameObject WarningGuideUI;
   [SerializeField, BoxGroup("General")] private TextMeshProUGUI guideText;

   private string message;
   //public string[] GuideCall;
   //train arrived
   //no fuel
   //destinataion not set

    void Start()
    {
        
    }

    void Update()
    {
        GuideState();
        guideText.text = message;

        if(WarningGuideUI.activeSelf){
            Invoke("OFFThis", 10f);
        }
    }
    void OFFThis(){
        WarningGuideUI.SetActive(false);
    }

    void GuideState(){
        switch(index){  //need 4 space
            case 0:
                message = "    Train has arrived.";
            break;
            case 1:
                message = "    Insert Fuel to proceed";
            break;
            case 2:
                message = "    Not enough Fuel";
            break;
            case 3:
                message = "    Destination Not Set";
            break;
            case 4:
                message = "    Destination Set";
            break;
        }
    }
}
