using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.InventoryEngine;

[CreateAssetMenu(fileName = "MedicineItem", menuName = "InfiniteTrain/Inventory/MedicineItem", order = 3)]
[Serializable]
public class MedicineItem : InventoryItemPlus
{
    [SerializeField] private float radiationDecrease;

    //references
    PlayerInformation _playerInfo;
    PlayerManager _playerManager;

    //getters & setters
    public float RadiationDecrease {get=>radiationDecrease;set=>radiationDecrease=value;}

    public override bool Use(string playerID)
    {
        base.Use(playerID);

        //decreases radiation
        _playerInfo = FindObjectOfType<PlayerInformation>();
        if (_playerInfo == null) {
            Debug.LogWarning("cant find playerInfomation");
            return false;
        }
        _playerInfo.CurrentRadiationValue = Mathf.Max(_playerInfo.CurrentRadiationValue-radiationDecrease, 0.0f);
        return true;
    }
}
