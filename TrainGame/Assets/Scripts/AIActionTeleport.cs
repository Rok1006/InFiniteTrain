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
        Debug.Log("tp");
        var tpPoint = _brain.Target.GetComponent<PlayerInformation>().back;
        var target = _brain.Target;
        gameObject.transform.position = tpPoint.position;

    }

    // Update is called once per frame
 
}
