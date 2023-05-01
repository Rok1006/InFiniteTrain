using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using System;

[CreateAssetMenu(fileName = "ThrowItem", menuName = "InfiniteTrain/Inventory/ThrowItem", order = 5)]
[Serializable]
public class ThrowItem : InventoryItemPlus
{
    //references
    PlayerInformation _playerInfo;
    PlayerManager _playerManager;
    public Projectile projectile;
    public GameObject indicator;

    public override bool Use(string playerID)
    {
        base.Use(playerID);

        //throw items
        if (isThrowable || isPlantable) {
            _playerInfo = FindObjectOfType<PlayerInformation>();
            if (_playerInfo == null)
                Debug.LogWarning("cant find playerInfomation");

            Projectile project = Instantiate(projectile, _playerInfo.transform.position, Quaternion.identity);
            if (indicator != null) {
                project.destination = indicator;
                project.timeToTake = 1.5f;
                Debug.Log("Using mechanism item");
            } else
                Debug.Log("cant find indicator");
            

            //throw animation
            _playerInfo.PlayerAnimator.SetTrigger("Throw");
        }
        return true;
    }

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
            if (isThrowable) {
                indicator = _playerManager.CreateThrowDestination();
            } else if (isPlantable) {
                indicator = _playerManager.CreatePlantDestination();
            }
            return true;
        }

        return false;
    }
}
