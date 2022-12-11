using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public class AIActionTeleport : AIAction
{
    public override void PerformAction()
    {
        Teleport();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Teleport()
    {
        // Debug.Log("tp");
        if (_brain == null) Debug.Log("_brain is null");
        if (_brain.Target == null) Debug.Log("_brain.Target is null");
        if (_brain.Target.GetComponent<PlayerInformation>() == null) Debug.Log("_brain.information is null");
        if (_brain.Target.GetComponent<PlayerInformation>().back == null) Debug.Log("_brain.back is null");
        var tpPoint = _brain.Target.GetComponent<PlayerInformation>().back;
        var target = _brain.Target;
        gameObject.transform.position = tpPoint.position;

    }

    // Update is called once per frame
 
}
