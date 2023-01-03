using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPopUp : MonoBehaviour
{
    [SerializeField] private GameObject PopUpObj;
    Animator PUAnim;
    private SceneManageNDisplay SceneManageNDisplay;
    public bool clicked = false;
    //[HideInInspector]public List<GameObject> PopUpPoint = new List<GameObject>();

    void Start()
    {
        PUAnim = PopUpObj.GetComponent<Animator>();
        SceneManageNDisplay = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneManageNDisplay>();
        //INPAnim = InfoPop.GetComponent<Animator>();
    }
    void Update()
    {
        
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
        if(SceneManageNDisplay.PopUpPoint.Count>0)
            ResetPoint();
        if(!clicked){
            clicked = true;
            PUAnim.SetTrigger("SetLocation");
            SceneManageNDisplay.PopUpPoint.Add(this.gameObject);
            ResetAnim(0);
            //if(SceneManageNDisplay.PopUpPoint.Count>0)
                //ResetAnim(0);
            //SceneManageNDisplay.PopUpPoint[0].transform.GetChild(0).GetComponent<MapPopUp>().clicked = false;
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
