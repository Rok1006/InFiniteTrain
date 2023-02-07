using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapState : MonoBehaviour
{

    public static int gameState;
    [SerializeField] private LineRenderer OW_LR;
    [SerializeField] private LineRenderer TW_LR;
    //LineRenderer LR;
    [SerializeField] private List<GameObject> Point = new List<GameObject>();
    // public GameObject UI;
    // public BoxCollider doorTrigger;
    // public BoxCollider block;
    // public GameObject text1;
    void Start()
    {
        //TW_LR = this.GetComponent<LineRenderer>();
        TW_LR.positionCount = Point.Count;
        for (int i = 0; i<= Point.Count; i++){
            TW_LR.SetPosition(i, new Vector3(Point[i].transform.position.x, Point[i].transform.position.y, Point[i].transform.position.z));
        }
        // doorTrigger.enabled = false;
        // block.enabled = true;
        // text1.SetActive(false);
    }
    void Update()
    {
        // for (int i = 0; i<= Point.Count; i++){
        //     TW_LR.SetPosition(i, new Vector3(Point[i].transform.position.x, Point[i].transform.position.y, Point[i].transform.position.z));
        // }

        // TW_LR.SetPosition(0, new Vector3(Point[0].transform.position.x, Point[0].transform.position.y, Point[0].transform.position.z));
        // TW_LR.SetPosition(1, new Vector3(Point[1].transform.position.x, Point[1].transform.position.y, Point[1].transform.position.z));
        // TW_LR.SetPosition(2, new Vector3(Point[2].transform.position.x, Point[2].transform.position.y, Point[2].transform.position.z));
    //    if(gameState >= 1)
    //     {
    //         UI.GetComponent<TextMeshProUGUI>().text = "Door Unlocked";
    //         doorTrigger.enabled = true;
    //          block.enabled = false;
    //          text1.SetActive(true);
    //     }
        
    }
}
