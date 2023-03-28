using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.InventoryEngine;

[CreateAssetMenu(fileName = "MechanismItem", menuName = "InfiniteTrain/Inventory/MechanismItem", order = 4)]
[Serializable]
public class MechanismItem : InventoryItemPlus
{
    //references
    PlayerInformation _playerInfo;
    PlayerManager _playerManager;
    public Projectile projectile;
    public GameObject indicator;

    public bool PlantIndicator() {
        //decreases radiation
        _playerInfo = FindObjectOfType<PlayerInformation>();
        if (_playerInfo == null)
            Debug.LogWarning("cant find playerInfomation");

        //increase movement speed
        _playerManager = FindObjectOfType<PlayerManager>();
        if (_playerManager == null)
            Debug.LogWarning("cant find playerManager");

        if (indicator == null) {
            indicator = _playerManager.CreateDestination();
            return true;
        }

        return false;
    }
    
    public override bool Use(string playerID)
    {
        base.Use(playerID);

        _playerInfo = FindObjectOfType<PlayerInformation>();
        if (_playerInfo == null)
            Debug.LogWarning("cant find playerInfomation");

        Projectile project = Instantiate(projectile, _playerInfo.transform.position, Quaternion.identity);
        if (indicator != null) {
            project.destination = indicator;
            project.timeToTake = 1.5f;
        } else
            Debug.Log("cant find indicator");
        // //decreases radiation
        // _playerInfo = FindObjectOfType<PlayerInformation>();
        // if (_playerInfo == null)
        //     Debug.LogWarning("cant find playerInfomation");

        // //increase movement speed
        // _playerManager = FindObjectOfType<PlayerManager>();
        // if (_playerManager == null)
        //     Debug.LogWarning("cant find playerManager");

        // Debug.Log("using " + name);

        // _playerManager.CreateDestination();

        return true;
    }
}
