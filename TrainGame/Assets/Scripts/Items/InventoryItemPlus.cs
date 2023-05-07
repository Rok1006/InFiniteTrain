using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using MoreMountains.Feedbacks;

public class InventoryItemPlus : InventoryItem
{
    public float actionTime;
    public bool hasMovementRestriction = true;
    public bool isPlantable = false, isThrowable = false;
}
