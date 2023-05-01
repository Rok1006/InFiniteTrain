using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.InventoryEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "MechanismItem", menuName = "InfiniteTrain/Inventory/MechanismItem", order = 4)]
[Serializable]
public class MechanismItem : InventoryItemPlus
{
    //references
    PlayerInformation _playerInfo;

    [SerializeField] private float radiationDecrease;

    //getters & setters
    public float RadiationDecrease {get=>radiationDecrease;set=>radiationDecrease=value;}
    
    public override bool Use(string playerID)
    {
        base.Use(playerID);

        //decreases radiation
        _playerInfo = FindObjectOfType<PlayerInformation>();
        if (_playerInfo == null)
            Debug.LogWarning("cant find playerInfomation");
        _playerInfo.CurrentRadiationValue = Mathf.Max(_playerInfo.CurrentRadiationValue-radiationDecrease, 0.0f);

        return true;
    }
}
