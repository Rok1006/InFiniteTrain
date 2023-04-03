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

    [SerializeField] private float radiationDecrease;

    //getters & setters
    public float RadiationDecrease {get=>radiationDecrease;set=>radiationDecrease=value;}

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

        //decreases radiation
        _playerInfo = FindObjectOfType<PlayerInformation>();
        if (_playerInfo == null)
            Debug.LogWarning("cant find playerInfomation");
        _playerInfo.CurrentRadiationValue = Mathf.Max(_playerInfo.CurrentRadiationValue-radiationDecrease, 0.0f);

        //throw items
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
