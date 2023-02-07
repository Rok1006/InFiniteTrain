using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapState : MonoBehaviour
{

    public static int gameState;
    
    [SerializeField] private LineRenderer TW_LRS1;
    [SerializeField] private LineRenderer TW_LRS2;
    [SerializeField] private LineRenderer TW_LRS3;
    [SerializeField] private LineRenderer OW_LRS1;
    [SerializeField] private LineRenderer OW_LRS2;
    [SerializeField] private LineRenderer OW_LRS3;
    //LineRenderer LR;
    [SerializeField] private List<GameObject> Point_TW1 = new List<GameObject>();
    [SerializeField] private List<GameObject> Point_TW2 = new List<GameObject>();
    [SerializeField] private List<GameObject> Point_TW3 = new List<GameObject>();

    [SerializeField] private List<GameObject> Point_OW1 = new List<GameObject>();
    [SerializeField] private List<GameObject> Point_OW2 = new List<GameObject>();
    [SerializeField] private List<GameObject> Point_OW3 = new List<GameObject>();
    // public GameObject UI;
    // public BoxCollider doorTrigger;
    // public BoxCollider block;
    // public GameObject text1;
    void Start()
    {
        //TW_LR = this.GetComponent<LineRenderer>();
        TW_LRS1.positionCount = Point_TW1.Count;
        TW_LRS2.positionCount = Point_TW2.Count;
        TW_LRS3.positionCount = Point_TW3.Count;

        OW_LRS1.positionCount = Point_OW1.Count;
        OW_LRS2.positionCount = Point_OW2.Count;
        OW_LRS3.positionCount = Point_OW3.Count;
        
        for (int i = 0; i< Point_TW1.Count; i++){
            TW_LRS1.SetPosition(i, new Vector3(Point_TW1[i].transform.position.x, Point_TW1[i].transform.position.y, Point_TW1[i].transform.position.z));
        }
        for (int i = 0; i< Point_TW2.Count; i++){
            TW_LRS2.SetPosition(i, new Vector3(Point_TW2[i].transform.position.x, Point_TW2[i].transform.position.y, Point_TW2[i].transform.position.z));
        }
        for (int i = 0; i< Point_TW3.Count; i++){
            TW_LRS3.SetPosition(i, new Vector3(Point_TW3[i].transform.position.x, Point_TW3[i].transform.position.y, Point_TW3[i].transform.position.z));
        }

        for (int i = 0; i< Point_OW1.Count; i++){
            OW_LRS1.SetPosition(i, new Vector3(Point_OW1[i].transform.position.x, Point_OW1[i].transform.position.y, Point_OW1[i].transform.position.z));
        }
        for (int i = 0; i< Point_OW2.Count; i++){
            OW_LRS2.SetPosition(i, new Vector3(Point_OW2[i].transform.position.x, Point_OW2[i].transform.position.y, Point_OW2[i].transform.position.z));
        }
        for (int i = 0; i< Point_OW3.Count; i++){
            OW_LRS3.SetPosition(i, new Vector3(Point_OW3[i].transform.position.x, Point_OW3[i].transform.position.y, Point_OW3[i].transform.position.z));
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
