using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.InventoryEngine;
using NaughtyAttributes;
using MoreMountains.TopDownEngine;


[CreateAssetMenu(fileName = "FoodItem", menuName = "InfiniteTrain/Inventory/FoodItem", order = 2)]
[Serializable]
public class FoodItem : InventoryItem
{
    [BoxGroup("Food")]
    public int FoodCount;
    [SerializeField, BoxGroup("Food")] private float radiationDecrease;
    [SerializeField, BoxGroup("Food")] private float movementBoost;
    [SerializeField, BoxGroup("Food")] private float lastingTime, currentTime;
    [SerializeField, ReadOnly] private float originalMovementSpeed;

    [SerializeField, BoxGroup("Randomly Generation")] private int score;

    //references
    PlayerInformation _playerInfo;
    CharacterMovement _characterMovement;
    PlayerManager _playerManager;

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

        //increase movement speed
        _playerManager = FindObjectOfType<PlayerManager>();
        if (_playerManager == null)
            Debug.LogWarning("cant find playerManager");
        
        _characterMovement = _playerInfo.GetComponent<CharacterMovement>();
        if (_characterMovement == null)
            Debug.LogWarning("cant find CharacterMovement");
        else
            originalMovementSpeed = _characterMovement.WalkSpeed;
        
        _playerManager.StartCoroutine(_playerManager.MovementBoost(lastingTime, movementBoost, originalMovementSpeed));
        return true;
    }
}
