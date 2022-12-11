using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapState : MonoBehaviour
{

    public static int gameState;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(gameState >= 1)
        {
            UI.GetComponent<TextMeshProUGUI>().text = "Door Unlocked";
        }
        
    }
}
