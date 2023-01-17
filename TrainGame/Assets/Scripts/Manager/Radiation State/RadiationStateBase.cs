using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RadiationStateBase
{
    public virtual void EnterState(RadiationManager manager) {}
    public virtual void UpdateState(RadiationManager manager) {}
    public virtual void LeaveState(RadiationManager manager) {}
}
