using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

public class AIActionDebugLog : AIAction
{
    public override void PerformAction()
    {
        Debug.Log("performing action");        
    }

    void Start()
    {
        
    }
}
