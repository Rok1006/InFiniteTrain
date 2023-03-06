using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapPopUp : MonoBehaviour
{
    [SerializeField] private GameObject PopUpObj;
    Animator PUAnim;
    private SceneManageNDisplay SceneManageNDisplay;
    public bool clicked = false;
    public Point point;
    public GameObject icon;
    public GameObject text;
    private MapManager mm;
    [SerializeField] GameObject RequirementPanel;
    //public GameObject door;
    [SerializeField]private Info InfoSC;
    //[HideInInspector]public List<GameObject> PopUpPoint = new List<GameObject>();

    void Start()
    {
        point = GetComponent<Point>();
        PUAnim = PopUpObj.GetComponent<Animator>();
        SceneManageNDisplay = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManageNDisplay>();
        mm = GameObject.FindGameObjectWithTag("Mehnager").GetComponent<MapManager>();
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
    }
    
    void Update()
    {
        icon.GetComponent<Image>().sprite = point.icon;
        text.GetComponent<TextMeshProUGUI>().text = point.text;

        if(Input.GetKeyDown(KeyCode.Space)&&clicked){ //make it appear after close and open again
            PUAnim.SetTrigger("SetLocation");
            //SceneManageNDisplay.PopUpPoint.Add(this.gameObject);
            ResetAnim(0);
        }
    }
    public void EnterPtIcon(){ //when player hover on green sq; on the Icon
        //if(SceneManageNDisplay.PopUpPoint.Count==0)
            PUAnim.SetTrigger("Hover");
    }
    public void ExitPtIcon(){ //when player hover on green sq; on the Icon
        //if(SceneManageNDisplay.PopUpPoint.Count==0)
            PUAnim.SetTrigger("Off");
    }
    public void ForceChange() 
    {
        clicked = true;
        if(clicked){
            PUAnim.SetTrigger("SetLocation");
            mm.PopUpPoint.Add(this.gameObject);
            ResetAnim(0);
        }
    }
    public void ClickPtIcon(){ //Click the Icon, set the destination; after this player go pull the lever
        if(MapManager.gameState == 0) {
           if( mm.AvailableToMove(this.gameObject) == true) //available to move Accord to fuelAmt
           {
                if (mm.PopUpPoint.Count > 0)
                    ResetPoint();
                if (!clicked)
                {
                    this.GetComponent<Point>().SendInfo(); //send selcted pt
                    clicked = true;
                    PUAnim.SetTrigger("SetLocation");
                    mm.PopUpPoint.Add(this.gameObject);
                    ResetAnim(0);
                    MapManager.gameState = 1;
                    SceneManageNDisplay.PickedLocation = true; //this shd turn false when train arrive at location
                    mm.PlayerMove(this.GetComponent<Point>().id);  //sent desitination inteval
                    //if(SceneManageNDisplay.PopUpPoint.Count>0)
                    //ResetAnim(0);
                    //SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<MapPopUp>().clicked = false;
                }
                MapManager.gameState = 1;
                GetComponent<Point>().MovePlayer();
                mm.UpdatePlayer();
                //door.SetActive(true);
           }else{
                SceneManageNDisplay.WarningGuideCall(2);
           }
        }
    }
    public void ResetPoint(){ //reset the status of point
        //PUAnim.SetTrigger("PlugFlag");
        //if(SceneManageNDisplay.PopUpPoint.Count==1)
            mm.PopUpPoint[0].GetComponent<MapPopUp>().clicked = false;
            mm.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("PlugFlag");
            if(mm.PopUpPoint.Count==2){
                //ResetAnim(0);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", false);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", true);
                mm.PopUpPoint.Remove(mm.PopUpPoint[0]);
                //ResetAnim(0);
            }else if(mm.PopUpPoint.Count==1){
                // ResetAnim(0);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", false);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", true);
                mm.PopUpPoint.TrimExcess();
                mm.PopUpPoint.Clear();
                //ResetAnim(0);
            }
            //if(SceneManageNDisplay.PopUpPoint.Count>0)
            //ResetAnim(0);
    }
    void ResetAnim(int i){
        mm.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("Hover", false);
        mm.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("Off", false);
        // SceneManageNDisplay.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("SetLocation", false);
        // SceneManageNDisplay.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("PlugFlag", false);
    }
    public void HoverExitPt(GameObject panel){
        panel.SetActive(true);
        PUAnim.SetTrigger("Hover");
    }
    public void ExitExitPt(GameObject panel){
        panel.SetActive(false);
        PUAnim.SetTrigger("Off");
    }
}
