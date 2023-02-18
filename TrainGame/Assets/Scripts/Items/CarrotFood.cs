using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;

public class CarrotFood : FoodItem
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
}
