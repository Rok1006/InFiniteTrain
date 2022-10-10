using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.InventoryEngine;


[CreateAssetMenu(fileName = "FoodItem", menuName = "InfiniteTrain/Inventory/FoodItem", order = 2)]
[Serializable]
public class FoodItem : InventoryItem
{
    [Header("Food")]
    public int FoodCount;

    public override bool Use(string playerID)
    {
        base.Use(playerID);
        Debug.Log("Eating Food");
        return true;
    }

    public override bool Drop(string playerID)
    {
        FoodCart foodCart = FindObjectOfType<FoodCart>();
        if (foodCart != null) {
            foodCart.Foods.Add(this);
        }
        base.Drop(playerID);
        return true;
    }
}
