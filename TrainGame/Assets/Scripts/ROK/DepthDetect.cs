using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class DepthDetect : MonoBehaviour
{
    [SerializeField] private GameObject Main; //the main player object
    [SerializeField] private GameObject FrontMC;
    [SerializeField] private GameObject BackMC;
    //public List<GameObject> thing = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col) { //Refine this later
        var obj = col.gameObject; //the encountered obj or character
        if(obj.transform.position.z>Main.transform.position.z){ //behind
            FrontMC.GetComponent<MeshRenderer>().sortingOrder = 1;
            BackMC.GetComponent<MeshRenderer>().sortingOrder = 1;
        }else if(obj.transform.position.z<Main.transform.position.z){ //infront
            FrontMC.GetComponent<MeshRenderer>().sortingOrder = -1;
            BackMC.GetComponent<MeshRenderer>().sortingOrder = -1;
        }
    }
}
