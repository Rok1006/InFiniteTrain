using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.EventSystems;

public class InventoryInputActionPlus : InventoryInputActions
{
    public InventoryDisplay _inventoryDisplay;
    private bool isPerformingAction = false;
    [SerializeField] private Animator _playerAnimator;

    protected override void Start()
    {
        base.Start();
        _playerAnimator = FindObjectOfType<PlayerInformation>().PlayerAnimator;
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
                if (!_inventoryDisplay.CurrentlySelectedInventorySlot().Equals( _inventoryDisplay.SlotContainer[binding.SlotIndex])) {
                    ExecuteAction(binding);
                } else {
                    if (isPerformingAction)
                        return;
                    
                    InventoryItemPlus item = (InventoryItemPlus) _targetInventory.Content[binding.SlotIndex];
                    if (item != null) {
                        StartCoroutine(waitToAct(item.actionTime, binding));
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                if (isPerformingAction)
                    return;
                
                InventoryItemPlus item = (InventoryItemPlus) _targetInventory.Content[binding.SlotIndex];
                if (item != null) {
                    StartCoroutine(waitToAct(item.actionTime, binding));
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
        isPerformingAction = true;
        yield return new WaitForSeconds(actionTime);

        //finish action animation
        _playerAnimator.SetTrigger("ActionFinished");

        ExecuteAction(binding);
        isPerformingAction = false;
    }
}
