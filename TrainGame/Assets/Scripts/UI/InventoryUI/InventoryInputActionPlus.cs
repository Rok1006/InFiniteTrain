using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.EventSystems;
using MoreMountains.TopDownEngine;

public class InventoryInputActionPlus : InventoryInputActions
{
    public InventoryDisplay _inventoryDisplay;
    private bool isPerformingAction = false;
    private Animator _playerAnimator;
    private PlayerManager _playerManager;

    protected override void Start()
    {
        base.Start();
        _playerAnimator = FindObjectOfType<PlayerInformation>().PlayerAnimator;
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    protected override void DetectInput()
    {
        foreach (InventoryInputActionsBindings binding in InputBindings)
        {
            if (binding == null)
            {
                continue;
            }
            if (!binding.Active)
            {
                continue;
            }
            if (Input.GetKeyDown(binding.InputBinding) || Input.GetKeyDown(binding.AltInputBinding))
            {
                if (_inventoryDisplay.CurrentlySelectedInventorySlot() != null) {
                    if (!_inventoryDisplay.CurrentlySelectedInventorySlot().Equals( _inventoryDisplay.SlotContainer[binding.SlotIndex])) {
                        ExecuteAction(binding);
                    } else {
                        StartUsingItem(binding);
                    }
                }
            }

            //cancel work if player doesnt hold the key/mouse button
            if (Input.GetKeyUp(binding.InputBinding) || Input.GetKeyUp(binding.AltInputBinding) || Input.GetMouseButtonUp(0)) {
                if (isPerformingAction) {
                    StopAllCoroutines();
                    isPerformingAction = false;
                    _playerManager.ReleaseMovement();
                    _playerManager.IsUsingItem = false;

                    MechanismItem mech = _targetInventory.Content[binding.SlotIndex] as MechanismItem;
                    if (mech != null)
                        mech.indicator = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) {
                    return;
                }
                if (_inventoryDisplay.CurrentlySelectedInventorySlot() != null) {
                    
                    if (!_inventoryDisplay.CurrentlySelectedInventorySlot().Equals(_inventoryDisplay.SlotContainer[_inventoryDisplay.CurrentlySelectedInventorySlot().Index])) {
                        _inventoryDisplay.SlotContainer[_inventoryDisplay.CurrentlySelectedInventorySlot().Index].Select();
                    } else {
                        StartUsingItem(_inventoryDisplay.CurrentlySelectedInventorySlot().Index);
                    }
                }
            }
    }

    protected override void ExecuteAction(InventoryInputActionsBindings binding)
    {
        if (binding.SlotIndex > _targetInventory.Content.Length)
        {
            return;
        }
        if (_targetInventory.Content[binding.SlotIndex] == null)
        {
            return;
        }

        if (_inventoryDisplay.CurrentlySelectedInventorySlot() != null) {
            if (!_inventoryDisplay.CurrentlySelectedInventorySlot().Equals( _inventoryDisplay.SlotContainer[binding.SlotIndex])) {
                _inventoryDisplay.SlotContainer[binding.SlotIndex].Select();
            } else {
                switch (binding.Action)
                {
                    case Actions.Equip:
                        MMInventoryEvent.Trigger(MMInventoryEventType.EquipRequest, null, _targetInventory.name, _targetInventory.Content[binding.SlotIndex], 0, binding.SlotIndex, _targetInventory.PlayerID);
                        break;
                    case Actions.Use:
                        MMInventoryEvent.Trigger(MMInventoryEventType.UseRequest, null, _targetInventory.name, _targetInventory.Content[binding.SlotIndex], 0, binding.SlotIndex, _targetInventory.PlayerID);
                        break;
                    case Actions.Drop:
                        MMInventoryEvent.Trigger(MMInventoryEventType.Drop, null, _targetInventory.name, _targetInventory.Content[binding.SlotIndex], 0, binding.SlotIndex, _targetInventory.PlayerID);
                        break;
                    case Actions.Unequip:
                        MMInventoryEvent.Trigger(MMInventoryEventType.UnEquipRequest, null, _targetInventory.name, _targetInventory.Content[binding.SlotIndex], 0, binding.SlotIndex, _targetInventory.PlayerID);
                        break;
                    case Actions.Combine:
                        MMInventoryEvent.Trigger(MMInventoryEventType.CombineRequest, null, _targetInventory.name, _targetInventory.Content[binding.SlotIndex], 0, binding.SlotIndex, _targetInventory.PlayerID);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    IEnumerator waitToAct(float actionTime, InventoryInputActionsBindings binding) {
        InventoryItemPlus item = _targetInventory.Content[binding.SlotIndex] as InventoryItemPlus;
        if (item.hasMovementRestriction) {
            _playerManager.RestrictMovement();
        }
        isPerformingAction = true;
        _playerManager.TotalActionTime = item.actionTime;
        _playerManager.IsUsingItem = true;
        yield return new WaitForSeconds(actionTime);
        //release restricted movement
        _playerManager.ReleaseMovement();

        //finish action animation
        _playerAnimator.SetTrigger("ActionFinished");
        Debug.Log("action finished");

        ExecuteAction(binding);
        isPerformingAction = false;
        _playerManager.IsUsingItem = false;
    }

    IEnumerator waitToAct(float actionTime, int slotIndex) {
        InventoryItemPlus item = _targetInventory.Content[slotIndex] as InventoryItemPlus;
        if (item.hasMovementRestriction) {
            _playerManager.RestrictMovement();
        }
        isPerformingAction = true;
        _playerManager.TotalActionTime = item.actionTime;
        _playerManager.IsUsingItem = true;
        yield return new WaitForSeconds(actionTime);
        //release restricted movement
        _playerManager.ReleaseMovement();

        //finish action animation
        _playerAnimator.SetTrigger("ActionFinished");
        Debug.Log("action finished");

        MMInventoryEvent.Trigger(MMInventoryEventType.UseRequest, null, _targetInventory.name,
                                            _targetInventory.Content[_inventoryDisplay.CurrentlySelectedInventorySlot().Index], 0,
                                            _inventoryDisplay.CurrentlySelectedInventorySlot().Index, _targetInventory.PlayerID);
        isPerformingAction = false;
        _playerManager.IsUsingItem = false;
    }

    public void StartUsingItem(InventoryInputActionsBindings binding) {
        if (isPerformingAction)
            return;
        
        InventoryItemPlus item = (InventoryItemPlus) _targetInventory.Content[binding.SlotIndex];
        if (item != null) {
            //if item can be planted
            MechanismItem mech = item as MechanismItem;
            if (mech != null) {
                mech.PlantIndicator();
                Debug.Log("Planting");
            }

            StartCoroutine(waitToAct(item.actionTime, binding));
        }
    }

    public void StartUsingItem(int slotIndex) {
        if (isPerformingAction)
            return;
        
        InventoryItemPlus item = (InventoryItemPlus) _targetInventory.Content[slotIndex];
        if (item != null) {
            //if item can be planted
            MechanismItem mech = item as MechanismItem;
            if (mech != null) {
                mech.PlantIndicator();
                Debug.Log("Planting");
            }
                
            StartCoroutine(waitToAct(item.actionTime, slotIndex));
        }
    }
}
