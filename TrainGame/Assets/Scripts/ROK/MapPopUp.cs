using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MapPopUp : MonoBehaviour
{
    [SerializeField]private Info InfoSC;
    [SerializeField]private SceneManageNDisplay SceneManageNDisplay;

    [SerializeField] private GameObject PopUpObj; //anim obj
    Animator PUAnim;
    public bool clicked = false;
    public bool blocked = false;   //every pt after selected point is blocked
    public Point point;
    public GameObject icon;
    public GameObject text;
    private MapManager mm;
    [SerializeField] GameObject RequirementPanel;

    [SerializeField, BoxGroup("UI")] public Image HeadIcon;

    void Awake() {
        PUAnim = PopUpObj.GetComponent<Animator>();
    }
    void Start()
    {
        point = GetComponent<Point>();
        SceneManageNDisplay = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManageNDisplay>();
        mm = GameObject.FindGameObjectWithTag("Mehnager").GetComponent<MapManager>();
        InfoSC = GameObject.Find("GameManager").GetComponent<Info>();
        //PUAnim.SetTrigger("SetLocation");
    }
    
    void Update()
    {
        icon.GetComponent<Image>().sprite = point.icon;
        text.GetComponent<TextMeshProUGUI>().text = point.text;

        if(Input.GetKeyDown(KeyCode.Space)&&this.clicked){ //make it appear after close and open again
            
        }
    }
    public void ReapperaFlagPt(){
        if(this.clicked&&this.GetComponent<Point>().id==InfoSC.CurrentSelectedPt){
            Debug.Log("bruh");
            this.PUAnim.SetTrigger("SetLocation"); //this one not going why?
            //SceneManageNDisplay.PopUpPoint.Add(this.gameObject);
            ResetAnim(0);
        }
    }
    public void EnterPtIcon(){ //when player hover on green sq; on the Icon
        if(!blocked){
            PUAnim.SetTrigger("Hover");
        }
    }
    public void ExitPtIcon(){ //when player hover on green sq; on the Icon
        if(!blocked){
            PUAnim.SetTrigger("Off");
        }
    }
    public void ForceChange() 
    {
        clicked = true;
        if(clicked&&this.GetComponent<Point>().id==InfoSC.CurrentSelectedPt){
            this.PUAnim.SetTrigger("SetLocation");
            mm.PopUpPoint.Add(this.gameObject);
            ResetAnim(0);
        }
    }
    public void ClickPtIcon(){ //Click the Icon, set the destination; after this player go pull the lever
        //if(MapManager.gameState == 0) {
        if(!blocked){
            if (mm.PopUpPoint.Count > 0){
                ResetPoint();
            }
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
                //SceneManageNDisplay.fuelCost = this.gameObject.GetComponent<Point>().fuelAmtNeeded;
                SceneManageNDisplay.WarningGuideCall(4);
                mm.GetTotalFuelNeeded(this.GetComponent<Point>().id);
                GetComponent<Point>().MovePlayer(); //new
                GetComponent<Point>().isPlayer = true; //new
                if(this.GetComponent<Point>().id == InfoSC.ConfirmedSelectedPt){
                    SceneManageNDisplay.WarningGuideCall(5);
                }
                InfoSC.radAmt = this.GetComponent<Point>()._radAmt;
                //InfoSC.CurrentSelectedPtObj = this.PopUpObj;
            }
            mm.UpdatePlayer(); //new
            //mm.UpdateMapPointState();
            //MapManager.gameState = 1;  //turns related
        }
    }
    public void ClickedTurnPt(){ //assign this on TURN pt, need flip train
        //wtever happen agter located turn pt
        //reopen the all the turn pts
        //there is sth in turn pt
    }
    public void ClickOnEXITPt(){  //assign this on EXIT pt
        
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
    public void ResetAnim(int i){
        //Debug.Log("brugh this shit");
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

//  //if(SceneManageNDisplay.PopUpPoint.Count>0)
                    //ResetAnim(0);
                    //SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<MapPopUp>().clicked = false;
