using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapState : MonoBehaviour
{

    public static int gameState;
    LineRenderer LR;
    [SerializeField] private List<GameObject> Point = new List<GameObject>();
    // public GameObject UI;
    // public BoxCollider doorTrigger;
    // public BoxCollider block;
    // public GameObject text1;
    void Start()
    {
        LR = this.GetComponent<LineRenderer>();
        LR.positionCount = 2;
        // doorTrigger.enabled = false;
        // block.enabled = true;
        // text1.SetActive(false);
    }
    void Update()
    {
        LR.SetPosition(0, new Vector3(Point[0].transform.position.x, Point[0].transform.position.y, Point[0].transform.position.z));
        LR.SetPosition(1, new Vector3(Point[1].transform.position.x, Point[1].transform.position.y, Point[1].transform.position.z));
    //    if(gameState >= 1)
    //     {
    //         UI.GetComponent<TextMeshProUGUI>().text = "Door Unlocked";
    //         doorTrigger.enabled = true;
    //          block.enabled = false;
    //          text1.SetActive(true);
    //     }
        
    }
}
