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
    //[HideInInspector]public List<GameObject> PopUpPoint = new List<GameObject>();

    void Start()
    {
        
        point = GetComponent<Point>();
        PUAnim = PopUpObj.GetComponent<Animator>();
        SceneManageNDisplay = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManageNDisplay>();
        mm = GameObject.FindGameObjectWithTag("Mehnager").GetComponent<MapManager>();
        // yeah man looks right
        //INPAnim = InfoPop.GetComponent<Animator>();
    }
    void Update()
    {
        icon.GetComponent<Image>().sprite = point.icon;
        text.GetComponent<TextMeshProUGUI>().text = point.text;
    }
    public void EnterPtIcon(){ //when player hover on green sq; on the Icon
        //if(SceneManageNDisplay.PopUpPoint.Count==0)
            PUAnim.SetTrigger("Hover");
    }
    public void ExitPtIcon(){ //when player hover on green sq; on the Icon
        //if(SceneManageNDisplay.PopUpPoint.Count==0)
            PUAnim.SetTrigger("Off");
    }
    public void ClickPtIcon(){ //Click the Icon, anim
        //Off Point
        // if(clicked){
        //     ResetPoint();
        //     clicked = false;
        // }
        if(MapManager.gameState == 0) {
           if( mm.AvailableToMove(this.gameObject) == true)
           {
                Debug.Log("df");
                if (SceneManageNDisplay.PopUpPoint.Count > 0)
                    ResetPoint();
                if (!clicked)
                {
                    clicked = true;
                    PUAnim.SetTrigger("SetLocation");
                    SceneManageNDisplay.PopUpPoint.Add(this.gameObject);
                    ResetAnim(0);
                    MapManager.gameState = 1;
                    //if(SceneManageNDisplay.PopUpPoint.Count>0)
                    //ResetAnim(0);
                    //SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<MapPopUp>().clicked = false;
                }
                MapManager.gameState = 1;
                GetComponent<Point>().MovePlayer();
                mm.UpdatePlayer();
           }
            
           


        }
      
        
        
    }
    public void ResetPoint(){ //reset the status of point
        //PUAnim.SetTrigger("PlugFlag");
        //if(SceneManageNDisplay.PopUpPoint.Count==1)
            SceneManageNDisplay.PopUpPoint[0].GetComponent<MapPopUp>().clicked = false;
            SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("PlugFlag");
            if(SceneManageNDisplay.PopUpPoint.Count==2){
                //ResetAnim(0);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", false);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", true);
                SceneManageNDisplay.PopUpPoint.Remove(SceneManageNDisplay.PopUpPoint[0]);
                //ResetAnim(0);
            }else if(SceneManageNDisplay.PopUpPoint.Count==1){
                // ResetAnim(0);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", false);
                // SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<Animator>().SetBool("Reset", true);
                SceneManageNDisplay.PopUpPoint.TrimExcess();
                SceneManageNDisplay.PopUpPoint.Clear();
                //ResetAnim(0);
            }
            //if(SceneManageNDisplay.PopUpPoint.Count>0)
            //ResetAnim(0);
    }
    void ResetAnim(int i){
        SceneManageNDisplay.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("Hover", false);
        SceneManageNDisplay.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("Off", false);
        // SceneManageNDisplay.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("SetLocation", false);
        // SceneManageNDisplay.PopUpPoint[i].transform.GetChild(0).GetComponent<Animator>().SetBool("PlugFlag", false);
    }
}
