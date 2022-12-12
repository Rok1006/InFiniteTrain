using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapState : MonoBehaviour
{

    public static int gameState;
    public GameObject UI;
    public BoxCollider doorTrigger;
    public BoxCollider block;
    public GameObject text1;
    // Start is called before the first frame update
    void Start()
    {
        doorTrigger.enabled = false;
        block.enabled = true;
        text1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(gameState >= 1)
        {
            UI.GetComponent<TextMeshProUGUI>().text = "Door Unlocked";
            doorTrigger.enabled = true;
             block.enabled = false;
             text1.SetActive(true);
        }
        
    }
}
