using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTriggerArea : MonoBehaviour
{
    private RadiationManager radiationManager;
    void Start()
    {
        radiationManager = FindObjectOfType<RadiationManager>();
        if (radiationManager == null)
            Debug.Log("cant find radiation manager");
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals("Player")) {
            Debug.Log("Player enter");
            radiationManager.IsRadiated = false;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag.Equals("Player")) {
            Debug.Log("Player Exit");
            radiationManager.IsRadiated = true;
        }
    }
}
