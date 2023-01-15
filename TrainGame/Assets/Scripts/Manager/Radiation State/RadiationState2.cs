using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationState2 : RadiationStateBase
{
    public override void EnterState(RadiationManager manager) {manager.enterRad2.Invoke();}
    public override void UpdateState(RadiationManager manager) {manager.updateRad2.Invoke();}
    public override void LeaveState(RadiationManager manager) {manager.leaveRad2.Invoke();}
}
