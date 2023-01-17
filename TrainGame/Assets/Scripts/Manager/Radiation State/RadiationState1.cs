using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationState1 : RadiationStateBase
{
    public override void EnterState(RadiationManager manager) {manager.enterRad1.Invoke();}
    public override void UpdateState(RadiationManager manager) {manager.updateRad1.Invoke();}
    public override void LeaveState(RadiationManager manager) {manager.leaveRad1.Invoke();}
}
