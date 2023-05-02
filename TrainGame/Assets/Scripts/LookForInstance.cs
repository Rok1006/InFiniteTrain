using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForInstance : MonoBehaviour
{
    public bool tutorial;
    public string lookingfor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tutorial == true )
        {
            if(Tutorial.instance.stepIndex == 2)
            {
                Tutorial.instance.arrow.target = this.gameObject.transform.GetChild(0);
            }
        }
    }
}
