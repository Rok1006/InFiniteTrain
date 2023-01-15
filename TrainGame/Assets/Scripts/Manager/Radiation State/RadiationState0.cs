using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationState0 : RadiationStateBase
{
    public override void EnterState(RadiationManager manager) {manager.enterRad0.Invoke();}
    public override void UpdateState(RadiationManager manager) {manager.updateRad0.Invoke();}
    public override void LeaveState(RadiationManager manager) {manager.leaveRad0.Invoke();}
}
