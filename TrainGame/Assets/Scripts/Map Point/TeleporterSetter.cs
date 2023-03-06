using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

/*this class set teleporter's target room and exit teleporter, according to map point number player set*/
public class TeleporterSetter : MonoBehaviour
{
    [SerializeField] private List<TargetTeleporters> targetTeleporters;
    private Teleporter myTeleporter;
    void Start()
    {
        myTeleporter = GetComponentInChildren<Teleporter>();
        foreach (TargetTeleporters targetTeleporter in targetTeleporters) {
            if (targetTeleporter.PointID == Info.Instance.pointID) {
                myTeleporter.Destination = targetTeleporter.Target;
                myTeleporter.TargetRoom = targetTeleporter.Target.GetComponentInParent<Room>();
            }
        }
    }
}

[System.Serializable]
public struct TargetTeleporters {
    public int PointID;
    public Teleporter Target; 
}
