using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.EventSystems;

public class InventoryInputActionPlus : InventoryInputActions
{
    public InventoryDisplay _inventoryDisplay;

    protected override void DetectInput()
    {
        base.DetectInput();
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
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                ExecuteAction(binding);
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
}
