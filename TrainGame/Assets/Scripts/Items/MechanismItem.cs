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
    PlayerManager _playerManager;
    public Projectile projectile;
    [ReadOnly] public GameObject indicator;

    [SerializeField] private float radiationDecrease;
    [SerializeField] private float indicatorExistingTime = 1.0f;

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
            if (isThrowable) {
                indicator = _playerManager.CreateThrowDestination();
            } else if (isPlantable) {
                indicator = _playerManager.CreatePlantDestination();
            }
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

    public override bool Pick(string playerID)
    {
        Debug.Log("picked " + ItemName);
        return base.Pick(playerID);
    }
}
