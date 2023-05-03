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
        Debug.Log(targetTeleporters.Count + " is the count of teleporters");
        foreach (TargetTeleporters targetTeleporter in targetTeleporters) {
            // Debug.Log("point id for target teleporter is :" + targetTeleporter.PointID +
            //             "\npoint id for info is :" + GameManager.Instance.GetComponent<Info>().pointID + "\nand they are " + (targetTeleporter.PointID == Info.Instance.pointID)
            //             +"\ninfo name is :" + Info.Instance.name);
            if (targetTeleporter.PointID == GameManager.Instance.GetComponent<Info>().pointID) {
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
