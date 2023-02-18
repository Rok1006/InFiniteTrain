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
    [SerializeField] private float radiationDecrease;

    //references
    PlayerInformation playerInfo;

    //getters & setters
    public float RadiationDecrease {get=>radiationDecrease;set=>radiationDecrease=value;}

    public override bool Use(string playerID)
    {
        base.Use(playerID);
        Debug.Log("Eating Food");
        playerInfo = FindObjectOfType<PlayerInformation>();
        if (playerInfo == null)
            Debug.LogWarning("cant find playerInfomation");
        playerInfo.CurrentRadiationValue = Mathf.Max(playerInfo.CurrentRadiationValue-radiationDecrease, 0.0f);
        return true;
    }
}
