using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class CarrotFood : FoodItem, ICombinable
{
    CombineManager combineManager;

    void Start() {
        combineManager = FindObjectOfType<CombineManager>();
    }

    public override bool Use(string playerID)
    {
        Debug.Log("using carrot");
        return base.Use(playerID);
    }

    public override bool Combine(string playerID)
    {
        Debug.Log("combining");
        return base.Combine(playerID);
    }

    public bool Combine() {
        if (combineManager.ItemA == null) {
            combineManager.ItemA = this;
            return true;
        } else if (combineManager.ItemB == null) {
            combineManager.ItemB = this;
            return true;
        }
        return false;
    }
}
